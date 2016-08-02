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
				macros += string.Format("{{k:{0} +}}", modifier);
			}

			foreach (string item in listBoxKeysToPress.Items)
			{
				macros += string.Format("{{{0}}}", item.Split('|').First().Trim());
			}

			foreach (string modifier in modifiers)
			{
				macros += string.Format("{{k:{0} -}}", modifier);
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
