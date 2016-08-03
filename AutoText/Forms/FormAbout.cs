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
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();
			labelVersion.Text = string.Format(labelVersion.Text, Constants.Common.ApplicationVersion);
			labelAuthor.Text = string.Format(labelAuthor.Text, Constants.Common.ApplicationAuthor);
		}

		private void FormAbout_Load(object sender, EventArgs e)
		{

		}
	}
}
