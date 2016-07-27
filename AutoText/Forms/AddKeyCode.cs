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
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxAction.SelectedItem.ToString() == "Press number of times")
			{
				numericUpDownQuantity.Show();
			}
			else
			{
				numericUpDownQuantity.Hide();
			}
		}

		private void AddKeyCode_Load(object sender, EventArgs e)
		{
			List<string> keys =
				ConfigHelper.GetKeycodesConfiguration().Keycodes.SelectMany(p => p.Names.Select(j => j.Value)).ToList();
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
				case "Press number of times":
					action = numericUpDownQuantity.Value.ToString();
					break;
			}


			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			string selStr = string.Format("{{k:{0} {1}}}", comboBoxKey.SelectedItem.ToString(), action);
			tb.Text = tb.Text.Insert(tb.SelectionStart, selStr);
			tb.SelectionStart = selStart + selStr.Length;
		}
	}
}
