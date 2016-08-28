/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

AutoText is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using AutoText.Forms;
using AutoText.Helpers;

namespace AutoText
{
	static class Program
	{
//		static bool catchUnhandledErrors = false;
		static bool catchUnhandledErrors = true;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			if (catchUnhandledErrors)
			{
				// Add the event handler for handling UI thread exceptions to the event.
				Application.ThreadException += Application_ThreadException;

				// Set the unhandled exception mode to force all Windows Forms errors
				// to go through our handler.
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

				// Add the event handler for handling non-UI thread exceptions to the event. 
				AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HandleUnhandledException(e, null);
		}

		static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			HandleUnhandledException(null, e);
		}

		private static void HandleUnhandledException(UnhandledExceptionEventArgs e1, ThreadExceptionEventArgs e2)
		{
			if (e1 != null && e1.IsTerminating)
			{
				ErrorMessageForm errorMessageForm = new ErrorMessageForm(null, (Exception)e1.ExceptionObject, e1.IsTerminating);
				errorMessageForm.ShowDialog();
			}
			else
			{
				if (e1 != null)
				{
					ErrorMessageForm errorMessageForm = new ErrorMessageForm(null, (Exception)e1.ExceptionObject, false);
					errorMessageForm.ShowDialog();
				}
				else if (e2 != null)
				{
					ErrorMessageForm errorMessageForm = new ErrorMessageForm(null, e2.Exception, false);
					errorMessageForm.ShowDialog();
				}
			}
		}
	}
}
