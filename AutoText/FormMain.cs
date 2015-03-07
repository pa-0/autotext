using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public partial class FormMain : Form
	{
		KeyLogger _keylogger = new KeyLogger(30);
		List<string> _capturedString = new List<string>();

		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			_keylogger.KeyCaptured += _keylogger_KeyCaptured;
			_keylogger.CaptureStopped += _keylogger_CaptureStopped;
			_keylogger.StartCapture();
		}

		void _keylogger_CaptureStopped(object sender, EventArgs e)
		{
			this.SetPropertyThreadSafe(() => Text, "Capture stopped");
		}

		void _keylogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			_capturedString.Add(e.CapturedCharacter);

			textBox1.Invoke(new Action(() =>
			{
				textBox1.Text += e.CapturedCharacter;
			}));
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//_keylogger.StopCapture();

			ExeConfigurationFileMap configMap = new ExeConfigurationFileMap
			{
				ExeConfigFilename = Path.GetFullPath("AutotextRules.config")
			};
			Configuration cfg = ConfigurationManager.OpenMappedExeConfiguration(configMap,ConfigurationUserLevel.None);
			AutotextRulesSection section = (AutotextRulesSection)cfg.GetSection("autotextRules");

			if (section != null)
			{
				string str =  section.Rules[0].Trigger;

				/*
				System.Diagnostics.Debug.WriteLine(section.FolderItems[0].FolderType);
				System.Diagnostics.Debug.WriteLine(section.FolderItems[0].Path);
				section.FolderItems[0].Path = "C:\\Nanook";
				cfg.Save(); //устанавливает перенос на новую строку и производит проверку <exename>.vshost.exe.config файла в вашей отладочной папке.
				*/
			}
			
		}
	}
}
