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
			string randomSourceString = "";

			if (checkBoxLowercaseLetters.Checked)
			{
				randomSourceString += Constants.Common.LowercaseLetters;
			}

			if (checkBoxUppercaseLetters.Checked)
			{
				randomSourceString += Constants.Common.UppercaseLetters;
			}

			if (checkBoxDigits.Checked)
			{
				randomSourceString += Constants.Common.Digits;
			}

			if (checkBoxSpecialCharacters.Checked)
			{
				randomSourceString += Constants.Common.SpecialChars;
			}

			if (checkBoxContainFollowingChars.Checked)
			{
				randomSourceString += textBoxCharsToContain.Text;
			}
		}

		private void checkBoxContainFollowingChars_CheckedChanged(object sender, EventArgs e)
		{
			textBoxCharsToContain.Enabled = checkBoxContainFollowingChars.Checked;
		}
	}
}
