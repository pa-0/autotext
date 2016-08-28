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
	public partial class ErrorMessageForm : Form
	{
		public ErrorMessageForm(string customMessage, Exception exceptionOccured, bool isErrorCrytical)
		{
			InitializeComponent();
			labelErrorMessage.Text = exceptionOccured.Message;
			textBoxExceptionDetails.Text = exceptionOccured.ToString();
			labelErrorCrytical.Visible = isErrorCrytical;

			if (isErrorCrytical)
			{
				buttonAction.Text = "Exit";
			}
			else
			{
				buttonAction.Text = "Ok";
			}
		}

		private void buttonAction_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
