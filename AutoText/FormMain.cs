using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using WindowsInput;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;
using System.Xml.Linq;
using NCalc;

namespace AutoText
{
	public partial class FormMain : Form
	{
		
		private List<AutotextRuleConfig> _rules;
		private AutotextMatcher _matcher;
		private KeyLogger _keylogger = new KeyLogger();


		public FormMain()
		{
			InitializeComponent();

			_rules = ConfigHelper.GetAutotextRules();
			_matcher = new AutotextMatcher(_rules);
			_matcher.MatchFound += _matcher_MatchFound;
			_keylogger.KeyCaptured += _testKeylogger_KeyCaptured;
			_keylogger.StartCapture();
		}


		private void FormMain_Load(object sender, EventArgs e)
		{
		}

		void _testKeylogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			textBox1.Invoke(new Action(() =>
			{
				textBox1.Text += (String.IsNullOrEmpty(e.CapturedCharacter) ? "\"\"" : e.CapturedCharacter) + "\r\n" + String.Join(" | ", e.CapturedKeys) + "\r\n\r\n";
				textBox1.Select(textBox1.Text.Length, 0);
				textBox1.ScrollToCaret();
			}));

			Keys[] notAllowedSymbols =
			{
				Keys.Up,
				Keys.Right,
				Keys.Left,
				Keys.Down,
				Keys.MButton,
				Keys.LButton,
				Keys.RButton,
				Keys.Home,
				Keys.End,
				Keys.PageDown,
				Keys.Delete,
				Keys.PageUp
			};

			if (e.CapturedKeys.Any(capturedKey => notAllowedSymbols.Contains(capturedKey)))
			{
				_matcher.ClearBuffer();
				return;
			}


			if (e.CapturedKeys[0] == Keys.Back )
			{
				_matcher.EraseLastBufferSymbol();
			}
			else
			{
				_matcher.CaptureSymbol(e);
			}


			textBoxBuffer.Invoke(new Action(() =>
			{
				textBoxBuffer.Text = _matcher.BufferContents;
			}));

		}

		void _matcher_MatchFound(object sender, AutotextMatchEventArgs e)
		{
			_keylogger.PauseCapture();
			Thread.Sleep(20);
			AutotextRuleExecution.ProcessRule(new MatchParameters(e.MatchedRule,e.Trigger));
			_keylogger.ResumeCapture();
			_matcher.ClearBuffer();
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			textBoxBufferContents.Focus();
		}
	}

}
