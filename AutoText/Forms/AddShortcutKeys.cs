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
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public partial class AddShortcutKeys : Form
	{
		public AddShortcutKeys()
		{
			InitializeComponent();
		}

		private void buttonRemove_Click(object sender, EventArgs e)
		{
			if (listBoxKeysToPress.SelectedItems.Count == 0)
			{
				MessageBox.Show(this, "Please select items first", "Warning");
			}
			else
			{
				listBoxKeysToPress.Items.RemoveAt(listBoxKeysToPress.SelectedIndices[0]);
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			listBoxKeysToPress.Items.Add(comboBoxKeys.SelectedItem.ToString());
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			List<string> modifiers = new List<string>(6);

			if (checkBoxAlt.Checked)
			{
				modifiers.Add("Alt");
			}

			if (checkBoxControl.Checked)
			{
				modifiers.Add("ControlKey");
			}

			if (checkBoxShift.Checked)
			{
				modifiers.Add("ShiftKey");
			}

			if (checkBoxWin.Checked)
			{
				modifiers.Add("LWin");
			}

			if (checkBoxMenu.Checked)
			{
				modifiers.Add("Menu");
			}

			string macros = "";

			foreach (string modifier in modifiers)
			{
				macros += string.Format("{{k [{0}] [+]}}", modifier);
			}

			foreach (string item in listBoxKeysToPress.Items)
			{
				macros += string.Format("{{k [{0}]}}", item.Split('|').First().Trim());
			}

			foreach (string modifier in modifiers)
			{
				macros += string.Format("{{k [{0}] [-]}}", modifier);
			}

			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			tb.Text = tb.Text.Insert(tb.SelectionStart, macros);
			tb.SelectionStart = selStart + macros.Length;
		}

		private void AddShortcutKeys_Load(object sender, EventArgs e)
		{
			List<string> keys = ConfigHelper.GetKeycodesConfiguration().Keycodes.Select(p => string.Join(" | ", p.Names.Select(g => g.Value))).ToList();
			keys.RemoveAt(0);
			comboBoxKeys.Items.AddRange(keys.ToArray());
			comboBoxKeys.SelectedIndex = 0;

		}
	}
}
