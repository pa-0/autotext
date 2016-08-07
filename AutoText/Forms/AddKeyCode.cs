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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public partial class AddKeyCode : Form
	{
		public AddKeyCode()
		{
			InitializeComponent();
			comboBoxAction.Width = 172;
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxAction.SelectedItem.ToString() == "Press")
			{
				numericUpDownQuantity.Show();
				labelTimes.Show();
				comboBoxAction.Width = 119;
			}
			else
			{
				numericUpDownQuantity.Hide();
				labelTimes.Hide();
				comboBoxAction.Width = 172;
			}
		}

		private void AddKeyCode_Load(object sender, EventArgs e)
		{
			List<string> keys = ConfigHelper.GetKeycodesConfiguration().Keycodes.Select(p => string.Join(" | ", p.Names.Select(g => g.Value))).ToList();
			keys.RemoveAt(0);
			comboBoxKey.Items.AddRange(keys.ToArray());
			comboBoxKey.SelectedIndex = 0;
			comboBoxAction.SelectedIndex = 0;
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			string action = "";

			switch (comboBoxAction.SelectedItem.ToString())
			{
				case "Down":
					action = "+";
					break;
				case "Up":
					action = "-";
					break;
				case "On":
					action = "on";
					break;
				case "Off":
					action = "off";
					break;
				case "Toggle":
					action = "toggle";
					break;
				case "Press":
					action = numericUpDownQuantity.Value.ToString();
					break;
			}


			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			string selStr = string.Format("{{k:{0} {1}}}", comboBoxKey.SelectedItem.ToString().Split('|').First().Trim(), action);
			tb.Text = tb.Text.Insert(tb.SelectionStart, selStr);
			tb.SelectionStart = selStart + selStr.Length;
		}
	}
}
