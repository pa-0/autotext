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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoText.Helpers.Extensions;

namespace AutoText.Forms
{
	public partial class AddInsertFileContentsMacros : Form
	{
		public AddInsertFileContentsMacros()
		{
			InitializeComponent();
		}

		private void buttonChoseFile_Click(object sender, EventArgs e)
		{
			openFileDialogFile.ShowDialog(this);
		}

		private void openFileDialogFile_FileOk(object sender, CancelEventArgs e)
		{
			textBoxPathToFile.Text = openFileDialogFile.FileName;
		}

		private void AddInsertFileContentsMacros_Load(object sender, EventArgs e)
		{
			comboBoxEncodings.Items.Add("System default");
			comboBoxEncodings.Items.AddRange(Encoding.GetEncodings().Select(p => p.Name).ToArray());
			comboBoxEncodings.SelectedIndex = 0;
		}

		private void buttonAddMacros_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxPathToFile.Text))
			{
				MessageBox.Show(this, "Please enter path to file", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}

			string encoding = comboBoxEncodings.SelectedItem.ToString();

			if (encoding == "System default")
			{
				encoding = "";
			}
			else
			{
				encoding = "[" + encoding + "]";
			}

			string macros = string.Format("{{f [{0}] {1}}}", 
				textBoxPathToFile.Text.EscapeSpecialExpressionChars(),
				encoding);

			((FormMain)Owner).InserTextToPhraseEditTextBox(macros);
		}

		private void AddInsertFileContentsMacros_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
