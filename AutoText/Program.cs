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

namespace AutoText
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			/*
			List<string> my = File.ReadAllText(@"c:\Documents and Settings\k304148\Desktop\Downloads\CU pages.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
			List<string> alexey = File.ReadAllText(@"c:\Documents and Settings\k304148\Desktop\Downloads\Alexey links.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
			List<string> vpids = File.ReadAllText(@"c:\Documents and Settings\k304148\Desktop\Downloads\vpids.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();

			List<string> res = my.Where(p => !vpids.Contains(p)).ToList();
			res.Clear();
			res.AddRange(alexey.Where(p => !vpids.Contains(p)).Distinct().ToList());

			string resStr = string.Join("\r\n", res);
			*/
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());
		}
	}
}
