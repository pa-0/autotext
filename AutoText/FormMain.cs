using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using System.Xml.XPath;
using AutoText.Helpers.Extensions;
using NCalc;

namespace AutoText
{
	public partial class FormMain : Form
	{

		private List<AutotextRuleConfig> _rules;
		private AutotextMatcher _matcher;
		private KeyLogger _keylogger = new KeyLogger();
		int shift = 0;
		int numberOfTriggers = 0;
		private int _curEditItemIndex = -1;

		public TextBox PhraseTextBox
		{
			get { return textBoxPhraseContent; }
		}


		public FormMain()
		{
			InitializeComponent();

			LoadRules();
			_matcher = new AutotextMatcher(_rules);
			_matcher.MatchFound += _matcher_MatchFound;
			_keylogger.KeyCaptured += _testKeylogger_KeyCaptured;
			_keylogger.StartCapture();

		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			foreach (AutotextRuleConfig ruleConfig in _rules)
			{
				listViewPhrases.Items.Add(new ListViewItem(ruleConfig.Abbreviation.AbbreviationText, ruleConfig.Description));
			}

			comboBoxProcessMacros.SelectedIndex = 0;
		}

		public void LoadRules()
		{
			_rules = ConfigHelper.GetAutotextRules();
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

			if (e.CapturedKeys.Any(capturedKey => notAllowedSymbols.Count(p => p.ToString() == capturedKey) > 0))
			{
				_matcher.ClearBuffer();
				return;
			}


			if (e.CapturedKeys[0] == "Back")
			{
				_matcher.EraseLastBufferSymbol();
			}
			else
			{
				_matcher.CaptureSymbol(e);
			}
		}

		void _matcher_MatchFound(object sender, AutotextMatchEventArgs e)
		{
			_keylogger.PauseCapture();
			Thread.Sleep(20);
			AutotextRuleExecution.ProcessRule(new MatchParameters(e.MatchedRule, e.Trigger));
			_keylogger.ResumeCapture();
			_matcher.ClearBuffer();
		}

		private void AddTriggerControls(string triggerChar, Keys? triggerKey, bool? charIsKeySensitive)
		{
			numberOfTriggers++;

			Panel triggerPanel = new Panel();
			triggerPanel.Size = new Size(298, 27);
			triggerPanel.Location = new Point(6, 19 + shift);
			//			triggerPanel.BorderStyle = BorderStyle.FixedSingle;
			triggerPanel.Name = "triggertsPanel" + numberOfTriggers;

			ComboBox comboBoxTriggerType = new ComboBox();
			TextBox textBoxTriggerChar = new TextBox();
			ComboBox comboBoxTriggerKey = new ComboBox();
			CheckBox charTriggerIsCaseSensitive = new CheckBox();
			Button buttonAddTrigger = new Button();
			Button buttonRemoveTrigger = new Button();


			// 
			// comboBoxTriggerType
			// 
			comboBoxTriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTriggerType.FormattingEnabled = true;
			comboBoxTriggerType.Items.AddRange(new object[] { "Key", "Character" });
			comboBoxTriggerType.Location = new System.Drawing.Point(1, 2);
			comboBoxTriggerType.Name = "comboBoxTriggerType";
			comboBoxTriggerType.Size = new System.Drawing.Size(80, 21);
			comboBoxTriggerType.TabIndex = 15;
			if (triggerChar != null)
			{
				comboBoxTriggerType.SelectedIndex = 1;
			}
			else if (triggerKey != null)
			{
				comboBoxTriggerType.SelectedIndex = 0;
			}
			else
			{
				comboBoxTriggerType.SelectedIndex = 0;
			}

			comboBoxTriggerType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// textBoxTriggerChar
			// 
			textBoxTriggerChar.Location = new System.Drawing.Point(98, 2);
			textBoxTriggerChar.MaxLength = 1;
			textBoxTriggerChar.Name = "textBoxTriggerChar";
			textBoxTriggerChar.Size = new System.Drawing.Size(29, 20);
			textBoxTriggerChar.TabIndex = 16;
			textBoxTriggerChar.Visible = triggerChar != null;
			textBoxTriggerChar.Text = triggerChar == null ? "" : triggerChar;
			// 
			// comboBoxTriggerKey
			// 
			comboBoxTriggerKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTriggerKey.FormattingEnabled = true;
			comboBoxTriggerKey.Location = new System.Drawing.Point(98, 2);
			comboBoxTriggerKey.Name = "comboBoxTriggerKey";
			comboBoxTriggerKey.Size = new System.Drawing.Size(135, 21);
			comboBoxTriggerKey.TabIndex = 17;
			comboBoxTriggerKey.Visible = triggerChar == null;
			comboBoxTriggerKey.Items.AddRange(ConfigHelper.GetKeycodesConfiguration().Keycodes.Select(p => p.Names.First().Value).ToArray());
			comboBoxTriggerKey.Items.RemoveAt(0);
			comboBoxTriggerKey.SelectedIndex = 0;

			if (triggerKey != null)
			{
				foreach (var item in comboBoxTriggerKey.Items)
				{
					if (triggerKey != null && item.ToString() == triggerKey.ToString())
					{
						comboBoxTriggerKey.SelectedItem = item;
						break;
					}
				}

				comboBoxTriggerKey.SelectedItem = triggerKey;

			}
			else
			{
				foreach (var item in comboBoxTriggerKey.Items)
				{
					if (item.ToString() == "Tab")
					{
						comboBoxTriggerKey.SelectedItem = item;
						break;
					}
				}
			}

			// 
			// checkBoxTriggerCaseSensitive
			// 
			charTriggerIsCaseSensitive.AutoSize = true;
			charTriggerIsCaseSensitive.Location = new System.Drawing.Point(132, 5);
			charTriggerIsCaseSensitive.Name = "checkBoxTriggerCaseSensitive";
			charTriggerIsCaseSensitive.Size = new System.Drawing.Size(94, 17);
			charTriggerIsCaseSensitive.TabIndex = 18;
			charTriggerIsCaseSensitive.Text = "Case sensitive";
			charTriggerIsCaseSensitive.UseVisualStyleBackColor = true;
			charTriggerIsCaseSensitive.Visible = charIsKeySensitive != null;
			charTriggerIsCaseSensitive.Checked = charIsKeySensitive == null ? false : (bool)charIsKeySensitive;
			// 
			// buttonAddTriggerButton
			// 
			buttonAddTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			buttonAddTrigger.Location = new System.Drawing.Point(242, 2);
			buttonAddTrigger.Name = "buttonAddTriggerButton";
			buttonAddTrigger.Size = new System.Drawing.Size(24, 23);
			buttonAddTrigger.TabIndex = 19;
			buttonAddTrigger.Text = "+";
			buttonAddTrigger.UseVisualStyleBackColor = true;
			buttonAddTrigger.Click += new System.EventHandler(this.buttonAddTriggerButton_Click);

			buttonRemoveTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			buttonRemoveTrigger.Location = new System.Drawing.Point(272, 2);
			buttonRemoveTrigger.Name = "buttonDelTriggerButton";
			buttonRemoveTrigger.Size = new System.Drawing.Size(24, 23);
			buttonRemoveTrigger.TabIndex = 19;
			buttonRemoveTrigger.Text = "-";
			buttonRemoveTrigger.UseVisualStyleBackColor = true;
			buttonRemoveTrigger.Click += buttonRemoveTrigger_Click;

			triggerPanel.Controls.Add(comboBoxTriggerType);
			triggerPanel.Controls.Add(textBoxTriggerChar);
			triggerPanel.Controls.Add(comboBoxTriggerKey);
			triggerPanel.Controls.Add(charTriggerIsCaseSensitive);
			triggerPanel.Controls.Add(buttonAddTrigger);
			triggerPanel.Controls.Add(buttonRemoveTrigger);
			groupBoxTriggers.Controls.Add(triggerPanel);
			shift += 30;

		}

		void buttonRemoveTrigger_Click(object sender, EventArgs e)
		{
			Panel pToRemove = groupBoxTriggers.Controls.Cast<Panel>().Where(p => p.Controls.Contains((Control)sender)).Single();
			groupBoxTriggers.Controls.Remove(pToRemove);

			shift = 19;
			List<Panel> availPanels = groupBoxTriggers.Controls.Cast<Panel>().ToList();

			foreach (Panel panel in availPanels)
			{
				panel.Location = new Point(panel.Location.X, 0 + shift);
				shift += 30;
			}

			if (availPanels.Count < 7)
			{
				foreach (Panel panel in availPanels)
				{
					foreach (Control control in panel.Controls)
					{
						if (control is Button && control.Name.StartsWith("buttonAddTriggerButton"))
						{
							control.Enabled = true;

						}
					}

				}
			}

			if (availPanels.Count == 1)
			{
				foreach (Panel panel in availPanels)
				{
					foreach (Control control in panel.Controls)
					{
						if (control is Button && control.Name.StartsWith("buttonDelTriggerButton"))
						{
							control.Enabled = false;

						}
					}

				}
			}

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

			Panel panel = (Panel)((Control)sender).Parent;

			string selRes =
				((ComboBox)panel.Controls.Find("comboBoxTriggerType" , false).First()).SelectedItem.ToString();

			if (selRes == "Key")
			{
				((TextBox)panel.Controls.Find("textBoxTriggerChar" , false).First()).Hide();
				((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false).First()).Hide();
				((ComboBox)panel.Controls.Find("comboBoxTriggerKey" , false).First()).Show();

			}
			else if (selRes == "Character")
			{
				((TextBox)panel.Controls.Find("textBoxTriggerChar" , false).First()).Show();
				((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive" , false).First()).Show();
				((ComboBox)panel.Controls.Find("comboBoxTriggerKey" , false).First()).Hide();

			}
			else
			{
				new InvalidOperationException();
			}
		}

		private void buttonAddTriggerButton_Click(object sender, EventArgs e)
		{
			AddTriggerControls(null, null, null);

			shift = 19;
			List<Panel> availPanels = groupBoxTriggers.Controls.Cast<Panel>().ToList();

			foreach (Panel panel in availPanels)
			{
				panel.Location = new Point(panel.Location.X, 0 + shift);
				shift += 30;
			}

			if (availPanels.Count == 7)
			{
				foreach (Panel panel in availPanels)
				{
					foreach (Control control in panel.Controls)
					{
						if (control is Button && control.Name.StartsWith("buttonAddTriggerButton"))
						{
							control.Enabled = false;

						}
					}

				}
			}

		}

		private void buttonAddPhrase_Click(object sender, EventArgs e)
		{
			listViewPhrases.Items.Add(new ListViewItem("<autotext>"));
			_curEditItemIndex = listViewPhrases.Items.Count - 1;

			textBoxDescription.Text = "";
			textBoxAutotext.Text = "";
			textBoxPhraseContent.Text = "";
			checkBoxSubstitute.Checked = false;
			checkBoxAutotextCaseSensetive.Checked = false;
			groupBoxTriggers.Controls.Clear();
			textBoxDescription.Focus();
			shift = 0;
			groupBoxTriggers.Controls.Clear();
			AddTriggerControls(null, null, null);
			((Control)groupBoxTriggers.Controls[0].Controls[5]).Enabled = false;

		}

		private void listViewPhrases_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView lv = (ListView)sender;

			if (lv.SelectedIndices.Count > 0)
			{
				shift = 0;
				groupBoxTriggers.Controls.Clear();

				AutotextRuleConfig config = _rules[lv.SelectedIndices[0]];

				textBoxDescription.Text = config.Description;
				textBoxPhraseContent.Text = config.Phrase;
				textBoxAutotext.Text = config.Abbreviation.AbbreviationText;
				checkBoxSubstitute.Checked = config.RemoveAbbr;
				checkBoxAutotextCaseSensetive.Checked = config.Abbreviation.CaseSensitive;
				bool shortcutFound = false;

				KeycodesConfiguration kcConfig = ConfigHelper.GetKeycodesConfiguration();

				List<string> kKodes = kcConfig.Keycodes.SelectMany(p => p.Names.Select(j => j.Value)).ToList();

				foreach (AutotextRuleTrigger trigger in config.Triggers)
				{
					if (kKodes.Contains( trigger.Value.Trim('{','}') ))
					{
						AddTriggerControls(null, (Keys)Enum.Parse(typeof(Keys), trigger.Value.Trim('{', '}')), trigger.CaseSensitive);
					}
					else
					{
						AddTriggerControls(trigger.Value, null, trigger.CaseSensitive);
					}
				}

				foreach (object item in comboBoxProcessMacros.Items)
				{
					if (item.ToString() == config.Macros.Mode.ToString())
					{
						comboBoxProcessMacros.SelectedItem = item;
						break;
					}
				}
				
			}

			((Control)groupBoxTriggers.Controls[0].Controls[5]).Enabled = false;


		}

		private void buttonSavePhrase_Click(object sender, EventArgs e)
		{
			XDocument config = XDocument.Parse(File.ReadAllText("AutotextRules.xml"), LoadOptions.PreserveWhitespace);
			List<Panel> triggersPanels = groupBoxTriggers.Controls.Cast<Panel>().ToList();

			XElement triggerItem = new XElement("triggers");

			foreach (Panel panel in triggersPanels)
			{
				string triggerType = ((ComboBox)panel.Controls.Find("comboBoxTriggerType", false)[0]).SelectedItem.ToString();
				string triggerChar = ((TextBox)panel.Controls.Find("textBoxTriggerChar", false)[0]).Text;
				string triggerKey = ((ComboBox)panel.Controls.Find("comboBoxTriggerKey", false)[0]).SelectedItem.ToString();
				bool triggerCaseSen = ((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false)[0]).Checked;

				if (triggerType == "Character")
				{
					triggerItem.Add(new XElement("item", new XAttribute("caseSensitive", triggerCaseSen),
							new XElement("value", new XCData(triggerChar))));
				}
				else if (triggerType == "Key")
				{
					triggerItem.Add(new XElement("item", new XAttribute("caseSensitive", triggerCaseSen),
							new XElement("value", new XCData("{" + triggerKey + "}"))));
				}
			}

			XElement newrule = new XElement("rule",
				new XElement("abbreviation",
					new XAttribute("caseSensitive",checkBoxAutotextCaseSensetive.Checked),
					new XElement("value", new XCData(textBoxAutotext.Text))),
				new XElement("removeAbbr", checkBoxSubstitute.Checked ? "true" : "false"),
				new XElement("phrase", new XCData(textBoxPhraseContent.Text)),
				new XElement("phraseCompiled", new XCData(textBoxPhraseContent.Text)),
				new XElement("macros", new XAttribute("mode", comboBoxProcessMacros.SelectedItem.ToString())),
				new XElement("description", new XCData(textBoxDescription.Text)),
				triggerItem);

			XElement rule =
				config.XPathSelectElement(string.Format("//rule/abbreviation/value[text()='{0}']/../..", textBoxAutotext.Text));

			if (rule != null)
			{
				rule.RemoveAll();
				rule.Add(newrule.XPathSelectElements("/*"));

			}
			else
			{
				config.Descendants("rules").First().Add(newrule);
			}

			File.Delete("AutotextRules.xml");

			using (FileStream fs = File.OpenWrite("AutotextRules.xml"))
			{
				config.Save(fs);
			}

			listViewPhrases.Items.Clear();

			_rules = ConfigHelper.GetAutotextRules();

			foreach (AutotextRuleConfig ruleConfig in _rules)
			{
				listViewPhrases.Items.Add(new ListViewItem(ruleConfig.Abbreviation.AbbreviationText, ruleConfig.Description));
			}

			LoadRules();
			_matcher.Rules = _rules;




		}

		private void listViewPhrases_Enter(object sender, EventArgs e)
		{
			List<ListViewItem> itemToRemove = new List<ListViewItem>(5);

			foreach (ListViewItem listViewItem in listViewPhrases.Items)
			{
				if (listViewItem.SubItems[0].Text == "<autotext>")
				{
					itemToRemove.Add(listViewItem);
				}
			}

			itemToRemove.ForEach(p => listViewPhrases.Items.Remove(p));

		}

		private void buttonRemovePhrase_Click(object sender, EventArgs e)
		{
			_rules = ConfigHelper.GetAutotextRules();

			if (listViewPhrases.SelectedItems.Count == 0)
			{
				MessageBox.Show(this, "Please select item first", "Warning");

			}
			else
			{

				ListViewItem lvi = listViewPhrases.SelectedItems[0];
				listViewPhrases.Items.Remove(lvi);

				_rules.Remove(_rules.Single(p => p.Abbreviation.AbbreviationText == lvi.SubItems[0].Text));
				listViewPhrases.Items.Clear();

				XDocument xd = XDocument.Parse(File.ReadAllText("AutotextRules.xml"));
				XElement elemTODel=  xd.XPathSelectElement(string.Format("//rule/abbreviation/value[text()='{0}']/../..", lvi.SubItems[0].Text));
				elemTODel.Remove();

				File.Delete("AutotextRules.xml");

				

				using (FileStream fs = File.OpenWrite("AutotextRules.xml"))
				{
					xd.Save(fs);
				}

				foreach (AutotextRuleConfig ruleConfig in _rules)
				{
					listViewPhrases.Items.Add(new ListViewItem(ruleConfig.Abbreviation.AbbreviationText, ruleConfig.Description));
				}
			}
		}

		private void buttonAddKeyCodeEpression_Click(object sender, EventArgs e)
		{
			AddKeyCode formKeyCodes = new AddKeyCode();
			formKeyCodes.Show(this);
		}

		private void FormMain_Activated(object sender, EventArgs e)
		{
			_keylogger.PauseCapture();
		}

		private void FormMain_Deactivate(object sender, EventArgs e)
		{
			_keylogger.ResumeCapture();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AddShortcutKeys form = new AddShortcutKeys();
			form.Show(this);
		}
	}

}
