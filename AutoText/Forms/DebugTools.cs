/*This file is part of AutoText.

Copyright © 2022 Alexander Litvinov

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoText.Engine;

namespace AutoText.Forms
{
	public partial class DebugTools : Form
	{
		KeyLogger _keyLogger;

		public DebugTools(KeyLogger keyLogger)
		{
			InitializeComponent();
			_keyLogger = keyLogger;
			_keyLogger.KeyCaptured += _keyLogger_KeyCaptured;
		}

		void _keyLogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			try
			{
				if (textBoxKeyCaptured.IsHandleCreated)
				{
					textBoxKeyCaptured.Invoke(new Action(() =>
					{
						textBoxKeyCaptured.Text += e.CapturedCharacter + " " + String.Join(" | ", e.CapturedKeys) + "\r\n\r\n";
						textBoxKeyCaptured.Select(textBoxKeyCaptured.Text.Length, 0);
						textBoxKeyCaptured.ScrollToCaret();
					}));
				}
			}
			//Ignore if object is disposed
			catch (ObjectDisposedException ex){}
		}

		private void textBoxKeyCaptured_TextChanged(object sender, EventArgs e)
		{
			if (textBoxKeyCaptured.Lines.Count() >= 100)
			{
				string[] lines = textBoxKeyCaptured.Lines;
				textBoxKeyCaptured.Lines = lines.Skip(1).ToArray();
			}
		}

		private void checkBoxStayOnTop_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = checkBoxStayOnTop.Checked;
		}

		private void DebugTools_Load(object sender, EventArgs e)
		{
			TopMost = true;
		}
	}
}
