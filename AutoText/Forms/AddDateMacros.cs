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
using AutoText.Helpers;
using AutoText.Helpers.Extensions;

namespace AutoText
{
	public partial class AddDateMacros : Form
	{
		public AddDateMacros()
		{
			InitializeComponent();
		}

		private void AddDateMacros_Load(object sender, EventArgs e)
		{
			labelExampleDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string macros = string.Format("{{d [{0}]}}", textBoxFormat.Text.EscapeSpecialExpressionChars());
			((FormMain)Owner).InserTextToPhraseEditTextBox(macros);
		}

		private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			HelpOnDateFormat helpOnDateFormatForm = new HelpOnDateFormat();
			helpOnDateFormatForm.CenterTo(this);
			helpOnDateFormatForm.Show(this);
		}

		private void AddDateMacros_Shown(object sender, EventArgs e)
		{
			textBoxFormat.DeselectAll();
		}
	}
}
