using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoText.Engine;

namespace AutoText.Forms
{
	public partial class DebugTools : Form
	{
		KeyLogger _keyLogger;

		public DebugTools(KeyLogger keyLogger)
		{
			InitializeComponent();
			_keyLogger = keyLogger;
			_keyLogger.KeyCaptured += _keyLogger_KeyCaptured;
		}

		void _keyLogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			try
			{
				if (textBoxKeyCaptured.IsHandleCreated)
				{
					textBoxKeyCaptured.Invoke(new Action(() =>
					{
						textBoxKeyCaptured.Text += e.CapturedCharacter + " " + String.Join(" | ", e.CapturedKeys) + "\r\n\r\n";
						textBoxKeyCaptured.Select(textBoxKeyCaptured.Text.Length, 0);
						textBoxKeyCaptured.ScrollToCaret();
					}));
				}
			}
			//Ignore if object is disposed
			catch (ObjectDisposedException ex){}
		}

		private void textBoxKeyCaptured_TextChanged(object sender, EventArgs e)
		{
			if (textBoxKeyCaptured.Lines.Count() >= 100)
			{
				string[] lines = textBoxKeyCaptured.Lines;
				textBoxKeyCaptured.Lines = lines.Skip(1).ToArray();
			}
		}
	}
}
