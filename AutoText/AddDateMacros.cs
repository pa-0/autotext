using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			string macros = string.Format("{{d:{0}}}", textBoxFormat.Text.ToString());
			tb.Text = tb.Text.Insert(tb.SelectionStart, macros);
			tb.SelectionStart = selStart + macros.Length;
		}

		private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			HelpOnDateFormat helpOnDateFormatForm = new HelpOnDateFormat();
			helpOnDateFormatForm.Show(this);
		}

		private void AddDateMacros_Shown(object sender, EventArgs e)
		{
			textBoxFormat.DeselectAll();
		}
	}
}
