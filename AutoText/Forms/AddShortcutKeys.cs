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
using System.Linq;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;

namespace AutoText.Forms
{
	public partial class AddShortcutKeys : Form
	{
		public AddShortcutKeys()
		{
			InitializeComponent();
		}

		private void buttonRemove_Click(object sender, EventArgs e)
		{
			if (listBoxKeysList.SelectedItems.Count == 0)
			{
				MessageBox.Show(this, "Please select items first", "Warning");
			}
			else
			{
				listBoxKeysList.Items.RemoveAt(listBoxKeysList.SelectedIndices[0]);
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			string listBoxItem = "Key - " + comboBoxKeys.SelectedItem.ToString();

			if (comboBoxKeyAction.SelectedItem.ToString() == "Press")
			{
				listBoxItem += " - Press - " + numericUpDownKeyPressCount.Value;
			}
			else
			{
				listBoxItem += " - " + comboBoxKeyAction.SelectedItem;
			}

			listBoxKeysList.Items.Add( listBoxItem );
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			List<string> modifiers = new List<string>(6);
			List<CheckBox> modifierCheckBoxes = groupBoxModifierKeys.Controls.OfType<CheckBox>().ToList();

			modifierCheckBoxes.ForEach(p =>
			{
				if (p.Checked)
				{
					modifiers.Add(ConfigHelper.GetNativeKeyByDisplayKey(p.Text).First());
				}
			});


			string macros = "";

			foreach (string modifier in modifiers)
			{
				macros += string.Format("{{k [{0}] [+]}}", modifier);
			}

			foreach (string item in listBoxKeysList.Items)
			{
				if (item.StartsWith("Key - "))
				{
					string[] split = item.Split(new[] { " - " }, StringSplitOptions.None);

					string key = ConfigHelper.GetNativeKeyByDisplayKey(split[1]).First();

					if (split[2] == "Press")
					{
						macros += string.Format("{{k [{0}] [{1}]}}", key, split[3]);
					}
					else if (split[2] == "Up")
					{
						macros += string.Format("{{k [{0}] [{1}]}}", key, "-");
					}
					else if (split[2] == "Down")
					{
						macros += string.Format("{{k [{0}] [{1}]}}", key, "+");
					}
					else if (split[2] == "On")
					{
						macros += string.Format("{{k [{0}] [{1}]}}", key, "on");
					}
					else if (split[2] == "Off")
					{
						macros += string.Format("{{k [{0}] [{1}]}}", key, "off");
					}
					else
					{
						throw new InvalidOperationException("Can't recognize key action");
					}
				}
				else if (item.StartsWith("Char - "))
				{
					string[] split = item.Split(new[] { " - " }, StringSplitOptions.None);

					if (split[2] == "Press")
					{
						macros += string.Format("{{c [{0}] [{1}]}}", split[1], split[3]);
					}
					else if (split[2] == "Up")
					{
						macros += string.Format("{{c [{0}] [{1}]}}", split[1], "-");
					}
					else if (split[2] == "Down")
					{
						macros += string.Format("{{c [{0}] [{1}]}}", split[1], "+");
					}
					else
					{
						throw new InvalidOperationException("Can't recognize char action");
					}
				}
			}

			foreach (string modifier in modifiers)
			{
				macros += string.Format("{{k [{0}] [-]}}", modifier);
			}

			((FormMain)Owner).InserTextToPhraseEditTextBox(macros);
		}

		private void AddShortcutKeys_Load(object sender, EventArgs e)
		{
			List<string> keys = ConfigHelper.GetAllDisplayKeys();
			comboBoxKeys.Items.AddRange(keys.ToArray());
			comboBoxKeys.SelectedIndex = 0;
			comboBoxKeyAction.SelectedIndex = 0;
			comboBoxCharAction.SelectedIndex = 0;
		}

		private void AddShortcutKeys_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void comboBoxKeyAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxKeyAction.SelectedItem.ToString() != "Press")
			{
				numericUpDownKeyPressCount.Enabled = false;
				labelKeyPressCount.Enabled = false;
			}
			else
			{
				numericUpDownKeyPressCount.Enabled = true;
				labelKeyPressCount.Enabled = true;
			}
		}

		private void comboBoxCharAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxCharAction.SelectedItem.ToString() != "Press")
			{
				numericUpDownCharPressCount.Enabled = false;
				labelCharPressCount.Enabled = false;
			}
			else
			{
				numericUpDownCharPressCount.Enabled = true;
				labelCharPressCount.Enabled = true;
			}
		}

		private void comboBoxKeys_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool canOn = ConfigHelper.GetKeycodesConfiguration()
				.Keycodes.Single(p => p.Names.Any(k => k.Value == comboBoxKeys.SelectedItem.ToString())).CanOn;

			if (canOn)
			{
				comboBoxKeyAction.Items.Clear();
				comboBoxKeyAction.Items.Add("Press");
				comboBoxKeyAction.Items.Add("Down");
				comboBoxKeyAction.Items.Add("Up");
				comboBoxKeyAction.Items.Add("On");
				comboBoxKeyAction.Items.Add("Off");
			}
			else
			{
				comboBoxKeyAction.Items.Clear();
				comboBoxKeyAction.Items.Add("Press");
				comboBoxKeyAction.Items.Add("Down");
				comboBoxKeyAction.Items.Add("Up");
			}

			comboBoxKeyAction.SelectedIndex = 0;
		}

		private void buttonCharAdd_Click(object sender, EventArgs e)
		{
			string listBoxItem = "Char - " + textBoxChar.Text;

			if (string.IsNullOrEmpty(textBoxChar.Text))
			{
				MessageBox.Show(this, "Please enter char to add", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (comboBoxCharAction.SelectedItem.ToString() == "Press")
			{
				listBoxItem += " - Press - " + numericUpDownCharPressCount.Value;
			}
			else
			{
				listBoxItem += " - " + comboBoxCharAction.SelectedItem;
			}

			listBoxKeysList.Items.Add(listBoxItem);
		}
	}
}
