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
using System.Windows.Forms;
using AutoText.Helpers.Extensions;

namespace AutoText.Forms
{
	public partial class AddRandomStringMacros : Form
	{
		public AddRandomStringMacros()
		{
			InitializeComponent();
		}

		private void buttonAddMacros_Click(object sender, EventArgs e)
		{
			string param = "palette[";
			bool hasPalette = false;
			bool hasUserChars = false;

			if (checkBoxLowercaseLetters.Checked)
			{
				param += "l";
				hasPalette = true;
			}

			if (checkBoxUppercaseLetters.Checked)
			{
				param += "L";
				hasPalette = true;
			}

			if (checkBoxDigits.Checked)
			{
				param += "d";
				hasPalette = true;
			}

			if (checkBoxSpecialCharacters.Checked)
			{
				param += "s";
				hasPalette = true;
			}

			param += "]";

			string userChars = "";

			if (checkBoxContainFollowingChars.Checked && !string.IsNullOrEmpty(textBoxCharsToContain.Text))
			{
				userChars += "chars[" + textBoxCharsToContain.Text.EscapeSpecialExpressionChars() + "]";
				hasUserChars = true;
			}

			if (!hasPalette && !hasUserChars)
			{
				MessageBox.Show(this, "No character sets selected", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}


			string count = numericUpDownMinStringLength.Value + "-" + numericUpDownMaxStringLenth.Value;

			string macros = string.Format("{{r {0} {1} count[{2}]}}", hasPalette ? param : "", hasUserChars ? userChars : "", count);

			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			tb.Text = tb.Text.Insert(tb.SelectionStart, macros);
			tb.SelectionStart = selStart + macros.Length;
		}

		private void checkBoxContainFollowingChars_CheckedChanged(object sender, EventArgs e)
		{
			textBoxCharsToContain.Enabled = checkBoxContainFollowingChars.Checked;
		}

		private void numericUpDownMinStringLength_ValueChanged(object sender, EventArgs e)
		{
			if (numericUpDownMinStringLength.Value > numericUpDownMaxStringLenth.Value)
			{
				numericUpDownMaxStringLenth.Value = numericUpDownMinStringLength.Value;
			}
		}

		private void numericUpDownMaxStringLenth_ValueChanged(object sender, EventArgs e)
		{
			if (numericUpDownMinStringLength.Value > numericUpDownMaxStringLenth.Value)
			{
				numericUpDownMinStringLength.Value = numericUpDownMaxStringLenth.Value;
			}
		}
	}
}
