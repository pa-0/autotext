using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
			string macros = string.Format("{{e [{0}]}}", comboBoxEnvVraNames.Text.Trim('%'));

			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			tb.Text = tb.Text.Insert(tb.SelectionStart, macros);
			tb.SelectionStart = selStart + macros.Length;
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
			labelExample.Text = Environment.GetEnvironmentVariable(comboBoxEnvVraNames.Text.Trim('%'));
		}
	}
}
