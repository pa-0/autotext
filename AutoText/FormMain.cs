using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoText
{
	public partial class FormMain : Form
	{
		Keylogger _keylogger = new Keylogger(20);

		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			_keylogger.KeyCaptured += _keylogger_KeyCaptured;
			_keylogger.StartCapture();
		}

		void _keylogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			textBox1.Invoke(new Action(() =>
			{
				if (!string.IsNullOrEmpty(e.CapturedCharacter))
				{
					textBox1.Text += e.CapturedCharacter;
				}
				else
				{
					textBox1.Text += e.CapturedKey;
				}
			}));
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}
	}
}
