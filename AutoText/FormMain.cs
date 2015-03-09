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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public partial class FormMain : Form
	{
		private List<AutotextRule> _rules;
		private Dictionary<string, int> _macrosChars;
		private AutotextMatcher _matcher;
		private string _textBoxText;
		private KeyLogger _keylogger = new KeyLogger();

		public FormMain()
		{
			InitializeComponent();

			_macrosChars = ConfigHelper.GetMacrosCharacters(@"MacrosCharacters.xml");
			_rules = ConfigHelper.GetAutotextRules("AutotextRules.xml");
			_matcher = new AutotextMatcher(_keylogger, _rules);
			_matcher.MatchFound += _matcher_MatchFound;

			_keylogger.KeyCaptured += _testKeylogger_KeyCaptured;
			_keylogger.StartCapture();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			/*
			Dictionary<string, int> keysEmbeddedValues = new Dictionary<string, int>();
			Dictionary<string, int> keysOtherValues = new Dictionary<string, int>();

			string[] names = Enum.GetNames(typeof(Keys));
			int[] values = (int[])Enum.GetValues(typeof(Keys));

			for (int i = 0; i < names.Length; i++)
			{
				keysEmbeddedValues.Add(names[i], values[i]);
			}

			string[] names1 = Enum.GetNames(typeof(Keys));
			ushort[] values1 = (ushort[])Enum.GetValues(typeof(Keys));

			for (int i = 0; i < names1.Length; i++)
			{
				keysOtherValues.Add(names1[i], values1[i]);
			}

			string otherStr = string.Join("\r\n", keysOtherValues.Select(p => p.Value + " " + p.Key));

			*/

			/*
			try
			{
				List<string> keysNames = Enum.GetNames(typeof(Keys)).ToList();
				List<int> keysValues = ((int[])Enum.GetValues(typeof(Keys))).ToList();

				int engKeybLayout = 67699721;
				//int ebgLayout =	WinAPI.GetKeyboardLayout(WinAPI.GetWindowThreadProcessId(WinAPI.GetForegroundWindow(), IntPtr.Zero));


				string res = "\"Index\";\"Name\";\"Value\";\"Char\";\"Unicode str\";\"Is control\";\"Is digit\";\"Is letter\";\"Is number\";\"Is punct\";\"Is separator\";\"Is whitespace\";\"Is symbol\";\"Match Regex\"\r\n";
				string nonPrintableKeys = "";

				for (int i = 0; i < keysNames.Count; i++)
				{
					string unicodeStr = TextHelper.GetCharsFromKeys(keysValues[i], false, false, engKeybLayout);

					if (!Regex.IsMatch(unicodeStr, @"[\p{L}\p{M}\p{N}\p{P}\p{S}]{1}"))
					{
						nonPrintableKeys += keysValues[i] + "\t" + keysNames[i] + "\r\n";
					}

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
				//File.WriteAllText(@"d:\Downloads\all keys.csv", res,Encoding.UTF8);

			}
			catch (Exception ex)
			{
				{ }
			}
			*/
		}

		void _testKeylogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			textBox1.Invoke(new Action(() =>
			{
				textBox1.Text += (string.IsNullOrEmpty(e.CapturedCharacter) ? "\"\"" : e.CapturedCharacter) + "\r\n" + string.Join(" | ", e.CapturedKeys) + "\r\n\r\n";
				textBox1.Select(textBox1.Text.Length, 0);
				textBox1.ScrollToCaret();

			}));
		}

		void _matcher_MatchFound(object sender, AutotextMatchEventArgs e)
		{
			//SendKeys.SendWait(e.MatchedRule.Phrase);
			//InputSimulator.SimulateTextEntry(e.MatchedRule.Phrase);

			InputSimulator.SimulateKeyDown(Keys.Menu);
			InputSimulator.SimulateKeyPress(Keys.A);
			//InputSimulator.SimulateKeyUp('A');
			InputSimulator.SimulateKeyUp(Keys.Menu);


														
			/*
			for (int i = 0; i < e.MatchedRule.Phrase.Length; i++)
			{
				SendKeys.SendWait(e.MatchedRule.Phrase[i].ToString());
				Thread.Sleep(200);
			}
			*/


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
