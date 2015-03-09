using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public partial class FormMain : Form
	{
		private List<AutotextRule> _rules;
		private AutotextMatcher _matcher;
		private string _textBoxText;
		KeyLogger _testKeylogger = new KeyLogger();

		public FormMain()
		{
			InitializeComponent();
		}

		public static Keys ConvertCharToVirtualKey(char ch)
		{
			short vkey = VkKeyScanW(ch);
			Keys retval = (Keys)(vkey & 0xff);
			int modifiers = vkey >> 8;
			if ((modifiers & 1) != 0) retval |= Keys.Shift;
			if ((modifiers & 2) != 0) retval |= Keys.Control;
			if ((modifiers & 4) != 0) retval |= Keys.Alt;
			return retval;
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern short VkKeyScanW(char ch);

		private void FormMain_Load(object sender, EventArgs e)
		{

			{ }

			/*
			try
			{
				List<string> keysNames = Enum.GetNames(typeof(Keys)).ToList();
				List<int> keysValues = ((int[])Enum.GetValues(typeof(Keys))).ToList();

				int engKeybLayout = 67699721;
				//int ebgLayout =	WinAPI.GetKeyboardLayout(WinAPI.GetWindowThreadProcessId(WinAPI.GetForegroundWindow(), IntPtr.Zero));


				string res = "\"Index\";\"Name\";\"Value\";\"Char\";\"Unicode str\";\"Is control\";\"Is digit\";\"Is letter\";\"Is number\";\"Is punct\";\"Is separator\";\"Is whitespace\";\"Is symbol\";\"Match Regex\"\r\n";

				for (int i = 0; i < keysNames.Count; i++)
				{
					string unicodeStr = TextHelper.GetCharsFromKeys(keysValues[i], false, false, engKeybLayout);
					char charTOSave = 'E';

					if (keysValues[i] <= short.MaxValue && keysValues[i] >= short.MinValue)
					{
						charTOSave = Convert.ToChar(keysValues[i]);
					}

					bool isControl = Char.IsControl(charTOSave);
					bool isDigit = Char.IsDigit(charTOSave);
					bool isLetter = Char.IsLetter(charTOSave);
					bool isNumber = Char.IsNumber(charTOSave);
					bool isPunctuation = Char.IsPunctuation(charTOSave);
					bool isSeparator = Char.IsSeparator(charTOSave);
					bool isWhitespace = Char.IsWhiteSpace(charTOSave);
					bool isSymbol = Char.IsSymbol(charTOSave);
					bool matchRegex = Regex.IsMatch(unicodeStr, @"[\p{L}\p{M}\p{N}\p{P}\p{S}]{1}");//[\p{L}\p{M}\p{N}\p{P}\p{S}]{1}

					res += string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{12}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{13}\"\r\n", 
						i, 
						keysNames[i], 
						keysValues[i], 
						charTOSave, 
						isControl,
						isDigit,
						isLetter,
						isNumber,
						isPunctuation,
						isSeparator,
						isWhitespace,
						isSymbol,
						unicodeStr,
						matchRegex);
				}

				File.WriteAllText(@"d:\Downloads\all keys.csv", res,Encoding.UTF8);

				{ }
			}
			catch (Exception ex)
			{
				{ }
			}
			*/
			/*
			_testKeylogger.KeyCaptured += _testKeylogger_KeyCaptured;
			_testKeylogger.StartCapture();
			return;
			 */
			try
			{
				_rules = ConfigHelper.GetAutotextRules("AutotextRules.xml");
				_matcher = new AutotextMatcher(new KeyLogger(), _rules);
				_matcher.MatchFound += _matcher_MatchFound;
			}
			catch (Exception ex)
			{
				{ }
				throw;
			}
		}

		void _testKeylogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			textBox1.Invoke(new Action(() =>
			{
				textBox1.Text += (string.IsNullOrEmpty(e.CapturedCharacter) ? "\"\"" : e.CapturedCharacter) + "\r\n" + string.Join(" | ",e.CapturedKeys) + "\r\n\r\n";
				textBox1.Select(textBox1.Text.Length,0);
				textBox1.ScrollToCaret();

			}));
		}

		void _matcher_MatchFound(object sender, AutotextMatchEventArgs e)
		{
			SendKeys.SendWait(e.MatchedRule.Phrase);

			/*
			_textBoxText += "Match found\r\n";
			textBox1.SetPropertyThreadSafe(() => textBox1.Text, _textBoxText);
			 */
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}
	}
}
