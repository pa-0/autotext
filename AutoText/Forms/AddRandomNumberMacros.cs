using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoText.Forms
{
	public partial class AddRandomNumberMacros : Form
	{
		public AddRandomNumberMacros()
		{
			InitializeComponent();

			numericUpDownMinimum.Minimum = long.MinValue;
			numericUpDownMinimum.Maximum = long.MaxValue - 1;
			numericUpDownMaximum.Minimum = long.MinValue;
			numericUpDownMaximum.Maximum = long.MaxValue - 1;
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			string macros = string.Format("{{n [{0}]}}", numericUpDownMinimum.Value + "|" + numericUpDownMaximum.Value);

			TextBox tb = ((FormMain)Owner).PhraseTextBox;
			int selStart = tb.SelectionStart;
			tb.Text = tb.Text.Insert(tb.SelectionStart, macros);
			tb.SelectionStart = selStart + macros.Length;
		}

		private void numericUpDownMinimum_ValueChanged(object sender, EventArgs e)
		{
			if (numericUpDownMinimum.Value >= numericUpDownMaximum.Value)
			{
				numericUpDownMaximum.Value = numericUpDownMinimum.Value;
			}
		}

		private void numericUpDownMaximum_ValueChanged(object sender, EventArgs e)
		{
			if (numericUpDownMinimum.Value >= numericUpDownMaximum.Value)
			{
				numericUpDownMinimum.Value = numericUpDownMaximum.Value;
			}
		}
	}
}
