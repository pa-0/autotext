using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
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

			/*
			string[] strTest = {"^","!","+","+","+","^","^" };
			string[] dist = strTest.Distinct().ToArray();
			string regEsc = Regex.Escape("^+");
			
			KeycodesConfiguration kkConfig = ConfigHelper.GetKeycodesConfiguration();
			List<int> similar = kkConfig.Keycodes.GroupBy(p => p.Value).Where(p => p.Count() > 1).Select(p => p.Key).ToList();

			List<KeycodeConfig> keycodes = kkConfig.Keycodes.Where(p => similar.Contains(p.Value )).ToList();

			string values = string.Join("\r\n", similar);
			string names = string.Join("\r\n", keycodes.Select(p => p.Value.ToString() + " " + p.Name));
			*/

			/*
			string[] testNames = Enum.GetNames(typeof(Keys));
			int[] testValues = (int[])Enum.GetValues(typeof(Keys));

			List<KeycodeTest> kkTest = new List<KeycodeTest>();

			for (int i = 0; i < testNames.Length; i++)
			{
				kkTest.Add(new KeycodeTest() { Name = testNames[i], Value = (int)Enum.Parse(typeof(Keys), testNames[i]) });
			}

			List<IGrouping<int, KeycodeTest>> groups = kkTest.GroupBy(p => p.Value).ToList();

			XDocument xmlDoc = new XDocument();
			XElement root = new XElement("configuration");
			xmlDoc.Add(root);
			XElement keycode;

			for (int i = 0; i < groups.Count; i++)
			{
				IGrouping<int, KeycodeTest> group = groups[i];
				keycode = new XElement("keycode");
				keycode.Add(new XAttribute("value", group.Key));
				keycode.Add(new XAttribute("toggleable", "false"));
				keycode.Add(new XAttribute("canOn", "false"));
				keycode.Add(new XAttribute("canOff", "false"));

				XElement namesCollection = new XElement("names");
				keycode.Add(namesCollection);

				foreach (KeycodeTest item in group)
				{
					XElement nemeElem = new XElement("name");
					nemeElem.Add(new XAttribute("value",item.Name));
					namesCollection.Add(nemeElem);
				}

				root.Add(keycode);
			}

			string resXml = xmlDoc.ToString();
			*/


			_rules = ConfigHelper.GetAutotextRules();
			_matcher = new AutotextMatcher(_keylogger, _rules);
			_matcher.MatchFound += _matcher_MatchFound;
			_matcher.BufferContentsChanged += _matcher_BufferContentsChanged;
			_matcher.CursorpositionChanged += _matcher_CursorpositionChanged;
			_keylogger.KeyCaptured += _testKeylogger_KeyCaptured;
			_keylogger.StartCapture();
		}

		void _matcher_CursorpositionChanged(object sender, CursorPositionChangedEventArgs e)
		{
			string cursorVisLeft = _matcher.BufferContents.Substring(0, e.NewCursorPosition);
			string cursorVisRight = _matcher.BufferContents.Substring(e.NewCursorPosition);
			string res = cursorVisLeft + "|" + cursorVisRight;
			textBoxBufferContents.Invoke(new Action(() => textBoxBufferContents.Text = res));
		}

		void _matcher_BufferContentsChanged(object sender, BufferContentsChangedEventArgs e)
		{
			string cursorVisLeft = e.NewValue.Substring(0, e.CursorPosition);
			string cursorVisRight = e.NewValue.Substring(e.CursorPosition);
			string res = cursorVisLeft + "|" + cursorVisRight;

			textBoxBufferContents.Invoke(new Action(() => textBoxBufferContents.Text = res));
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
			AutotextRuleExecution.ProcessRule(e.MatchedRule);
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}
	}

}
