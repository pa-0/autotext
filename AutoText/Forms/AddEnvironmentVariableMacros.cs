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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoText.Helpers.Extensions;

namespace AutoText.Forms
{
	public partial class AddEnvironmentVariableMacros : Form
	{
		public AddEnvironmentVariableMacros()
		{
			InitializeComponent();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			string macros = string.Format("{{e [{0}]}}", comboBoxEnvVraNames.Text.Trim('%').EscapeSpecialExpressionChars());
			((FormMain)Owner).InserTextToPhraseEditTextBox(macros);
		}

		private void AddEnvironmentVariableMacros_Load(object sender, EventArgs e)
		{
			IDictionary envVars = Environment.GetEnvironmentVariables();

			foreach (string key in envVars.Keys)
			{
				comboBoxEnvVraNames.Items.Add("%" + key + "%");
			}

			comboBoxEnvVraNames.SelectedIndex = 0;
		}

		private void comboBoxEnvVraNames_TextChanged(object sender, EventArgs e)
		{
			textBoxPreviev.Text = Environment.GetEnvironmentVariable(comboBoxEnvVraNames.Text.Trim('%'));
		}

		private void AddEnvironmentVariableMacros_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
