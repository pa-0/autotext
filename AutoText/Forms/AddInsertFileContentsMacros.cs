using System;
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
	public partial class AddInsertFileContentsMacros : Form
	{
		public AddInsertFileContentsMacros()
		{
			InitializeComponent();
		}

		private void buttonChoseFile_Click(object sender, EventArgs e)
		{
			openFileDialogFile.ShowDialog(this);
		}

		private void openFileDialogFile_FileOk(object sender, CancelEventArgs e)
		{
			textBoxPathToFile.Text = openFileDialogFile.FileName;
		}

		private void AddInsertFileContentsMacros_Load(object sender, EventArgs e)
		{
			comboBoxEncodings.Items.Add("System default");
			comboBoxEncodings.Items.AddRange(Encoding.GetEncodings().Select(p => p.Name).ToArray());
			comboBoxEncodings.SelectedIndex = 0;
		}

		private void buttonAddMacros_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxPathToFile.Text))
			{
				MessageBox.Show(this, "Please enter path to file", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}

			string encoding = comboBoxEncodings.SelectedItem.ToString();

			if (encoding == "System default")
			{
				encoding = "";
			}
			else
			{
				encoding = "[" + encoding + "]";
			}

			string macros = string.Format("{{f [{0}] {1}}}", 
				textBoxPathToFile.Text.EscapeSpecialExpressionChars(),
				encoding);

			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			tb.Text = tb.Text.Insert(tb.SelectionStart, macros);
			tb.SelectionStart = selStart + macros.Length;
		}
	}
}
