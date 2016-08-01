﻿using System;
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
using AutoText.Engine;
using AutoText.Forms;
using AutoText.Helpers.Extensions;
using AutoText.Model.Configuration;
using KellermanSoftware.CompareNetObjects;


namespace AutoText
{
	public partial class FormMain : Form
	{

		private List<AutotextRuleConfig> _rules;
		private AutotextMatcher _matcher;
		private KeyLogger _keylogger = new KeyLogger();
		int _shift;
		int _numberOfTriggers;
		private int _curSelectedPhraseIndex = -1;

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

		private void LoadPhrasesToListView()
		{
			dataGridViewPhrases.Rows.Clear();

			foreach (AutotextRuleConfig ruleConfig in _rules)
			{
				dataGridViewPhrases.Rows.Add(ruleConfig.Abbreviation.AbbreviationText, ruleConfig.Description);
			}
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadPhrasesToListView();
			comboBoxProcessMacros.SelectedIndex = 0;
			contextMenuStripPhraseContentEdit.Opening += contextMenuStripPhraseContentEdit_Opening;
		}

		public void LoadRules()
		{
			_rules = ConfigHelper.GetAutotextRules();
		}

		void _testKeylogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
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
				_matcher.EraseLastBufferedSymbol();
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
			AutotextRuleExecution.ProcessRule(new AutotextRuleMatchParameters(e.MatchedRule, e.Trigger));
			_keylogger.ResumeCapture();
			_matcher.ClearBuffer();
		}

		private void AddTriggerControls(string triggerChar, Keys? triggerKey, bool? charIsKeySensitive)
		{
			_numberOfTriggers++;

			Panel triggerPanel = new Panel();
			triggerPanel.Size = new Size(298, 27);
			triggerPanel.Location = new Point(6, 19 + _shift);
			//			triggerPanel.BorderStyle = BorderStyle.FixedSingle;
			triggerPanel.Name = "triggertsPanel" + _numberOfTriggers;

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
			comboBoxTriggerType.Font = new Font(comboBoxTriggerType.Font, FontStyle.Regular);
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
			textBoxTriggerChar.Font = new Font(textBoxTriggerChar.Font,FontStyle.Regular);
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
			comboBoxTriggerKey.Font = new Font(comboBoxTriggerKey.Font, FontStyle.Regular);
			ConfigHelper.GetKeycodesConfiguration().Keycodes.ForEach(p => comboBoxTriggerKey.Items.Add(string.Join(" | ", p.Names.Select( g => g.Value))));
			comboBoxTriggerKey.Items.RemoveAt(0);
			comboBoxTriggerKey.SelectedIndex = 0;

			if (triggerKey != null)
			{
				foreach (var item in comboBoxTriggerKey.Items)
				{
					if (triggerKey != null && item.ToString().Split('|').Select(p => p.Trim()).ToList().Contains(triggerKey.ToString()))
					{
						comboBoxTriggerKey.SelectedItem = item;
						break;
					}
				}
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
			charTriggerIsCaseSensitive.Font = new Font(charTriggerIsCaseSensitive.Font, FontStyle.Regular);
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
			_shift += 30;

		}

		void buttonRemoveTrigger_Click(object sender, EventArgs e)
		{
			Panel pToRemove = groupBoxTriggers.Controls.Cast<Panel>().Single(p => p.Controls.Contains((Control)sender));
			groupBoxTriggers.Controls.Remove(pToRemove);

			_shift = 19;
			List<Panel> availPanels = groupBoxTriggers.Controls.Cast<Panel>().ToList();

			foreach (Panel panel in availPanels)
			{
				panel.Location = new Point(panel.Location.X, 0 + _shift);
				_shift += 30;
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

			_shift = 19;
			List<Panel> availPanels = groupBoxTriggers.Controls.Cast<Panel>().ToList();

			foreach (Panel panel in availPanels)
			{
				panel.Location = new Point(panel.Location.X, 0 + _shift);
				_shift += 30;
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
			IEnumerable<string> availPhraseAbbreviations = _rules.Select(p => p.Abbreviation.AbbreviationText);
			IEnumerable<string> matchedToDefNameAbbr =
				availPhraseAbbreviations.Where(p => Regex.IsMatch(p, Constants.Common.NewPhraseDefaultAutotextRegex));

			string nextNewPhraseAutotext;

			if (!matchedToDefNameAbbr.Any())
			{
				nextNewPhraseAutotext = string.Format(Constants.Common.NewPhraseDefaultAutotext, "");
			}
			//if we have autotext template with no numbers
			else if (matchedToDefNameAbbr.Count() == 1 && matchedToDefNameAbbr.First() == string.Format(Constants.Common.NewPhraseDefaultAutotext, ""))
			{
				nextNewPhraseAutotext = string.Format(Constants.Common.NewPhraseDefaultAutotext, "1");
			}
			else
			{
				int maxNum =
					//Where we have some number
					matchedToDefNameAbbr.Where(g => !string.IsNullOrEmpty(Regex.Match(g, Constants.Common.NewPhraseDefaultAutotextRegex).Groups[1].Value)).
					//Get that number max value
					Select(p => int.Parse(Regex.Match(p, Constants.Common.NewPhraseDefaultAutotextRegex).Groups[1].Value)).Max();
				nextNewPhraseAutotext = string.Format(Constants.Common.NewPhraseDefaultAutotext, maxNum + 1);
			}


			{ }
			AddNewPhrase(nextNewPhraseAutotext);
			dataGridViewPhrases.SelectedRows.Clear();
//			listViewPhrases.SelectedIndices.Clear();
//			listViewPhrases.SelectedIndices.Add(listViewPhrases.Items.Count - 1);
		}

		private void SavePhrase(int phraseIndex)
		{
			if (_rules.Where((p, i) => i != phraseIndex).Any(p => p.Abbreviation.AbbreviationText == textBoxAutotext.Text))
			{
				MessageBox.Show(this, "Phrase with specified autotext is already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}

			XElement ruleToSave = GetCurrentPhrase();
			XDocument config = XDocument.Parse(File.ReadAllText(Constants.Common.AutotextRulesConfigFileFullPath), LoadOptions.PreserveWhitespace);
			XElement ruleToRewrite = config.Descendants("rule").ElementAt(phraseIndex);
			ruleToRewrite.RemoveAll();
			ruleToRewrite.Add(ruleToSave.XPathSelectElements("/*"));

			File.Delete(Constants.Common.AutotextRulesConfigFileFullPath);

			using (FileStream fs = File.OpenWrite(Constants.Common.AutotextRulesConfigFileFullPath))
			{
				config.Save(fs);
			}

			LoadRules();
			LoadPhrasesToListView();
			_matcher.Rules = ConfigHelper.GetAutotextRules();
		}

		private void AddNewPhrase(string abbreviationText)
		{
			XElement triggerItem = new XElement("triggers");
			triggerItem.Add(new XElement("item", new XAttribute("caseSensitive", false), new XElement("value", new XCData("{Tab}"))));

			XElement ruleToSave = new XElement("rule", new XElement("abbreviation", new XAttribute("caseSensitive", false), new XElement("value", new XCData(abbreviationText))), new XElement("removeAbbr", true), new XElement("phrase", new XCData("<phrase content>")), new XElement("phraseCompiled", new XCData("<phrase content>")), new XElement("macros", new XAttribute("mode", "Execute")), new XElement("description", new XCData("<phrase description>")), triggerItem);

			XDocument config = XDocument.Parse(File.ReadAllText(Constants.Common.AutotextRulesConfigFileFullPath), LoadOptions.PreserveWhitespace);
			config.Descendants("rules").First().Add(ruleToSave);

			File.Delete(Constants.Common.AutotextRulesConfigFileFullPath);

			using (FileStream fs = File.OpenWrite(Constants.Common.AutotextRulesConfigFileFullPath))
			{
				config.Save(fs);
			}

			LoadRules();
			LoadPhrasesToListView();
			_matcher.Rules = ConfigHelper.GetAutotextRules();
		}

		private void buttonSavePhrase_Click(object sender, EventArgs e)
		{
			List<int> selRowsIndeces = GetDataGridViewSelectedRows();

			if (selRowsIndeces.Count == 0)
			{
				MessageBox.Show(this, "Please select item first", "Attention");
			}
			else
			{
				int selIndex = selRowsIndeces.First();
				SavePhrase(selIndex);
				dataGridViewPhrases.SelectedRows.Clear();
//				listViewPhrases.SelectedIndices.Clear();
//				listViewPhrases.SelectedIndices.Add(selIndex);
			}
		}

		private void buttonRemovePhrase_Click(object sender, EventArgs e)
		{

/*
			if (listViewPhrases.SelectedItems.Count == 0)
			{
				MessageBox.Show(this, "Please select item first", "Attention");
			}
			else
			{
				if (MessageBox.Show(this, "Are you sure that you want to delete selected phrase?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					_rules = ConfigHelper.GetAutotextRules();

					ListViewItem lvi = listViewPhrases.Items[_curSelectedPhraseIndex];
					listViewPhrases.Items.Remove(lvi);
					listViewPhrases.SelectedIndices.Clear();

					if (listViewPhrases.Items.Count > 0)
					{
						if (_curSelectedPhraseIndex <= (listViewPhrases.Items.Count - 1))
						{
							listViewPhrases.SelectedIndices.Add(_curSelectedPhraseIndex);
						}
						else
						{
							listViewPhrases.SelectedIndices.Add(listViewPhrases.Items.Count - 1);
						}
					}

					_rules.RemoveAt(_curSelectedPhraseIndex);

					XDocument xd = XDocument.Parse(File.ReadAllText(Constants.Common.AutotextRulesConfigFileFullPath));
					XElement elemToDel = xd.Descendants("rule").ElementAt(_curSelectedPhraseIndex);
					elemToDel.Remove();

					File.Delete(Constants.Common.AutotextRulesConfigFileFullPath);

					using (FileStream fs = File.OpenWrite(Constants.Common.AutotextRulesConfigFileFullPath))
					{
						xd.Save(fs);
					}

					LoadRules();
					_matcher.Rules = ConfigHelper.GetAutotextRules();
				}
			}
*/
		}

		private void FormMain_Activated(object sender, EventArgs e)
		{
//			_keylogger.PauseCapture();
		}

		private void FormMain_Deactivate(object sender, EventArgs e)
		{
//			_keylogger.ResumeCapture();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
/*
			if (listViewPhrases.Items.Count > 0)
			{
				listViewPhrases.SelectedIndices.Add(0);
				_curSelectedPhraseIndex = 0;
			}
*/
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == this.WindowState)
			{
				notifyIcon.Visible = true;
				this.Hide();
			}

			else if (FormWindowState.Normal == this.WindowState)
			{
				notifyIcon.Visible = false;
			}
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBoxPhraseContent.Text))
			{
				Clipboard.SetText(textBoxPhraseContent.Text);
			}
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string textToPaste = Clipboard.GetText();
			int selectionStart = textBoxPhraseContent.SelectionStart;
			string textBoxText = textBoxPhraseContent.Text;

			if (textBoxPhraseContent.SelectionLength != 0)
			{
				textBoxText = textBoxText.Remove(textBoxPhraseContent.SelectionStart, textBoxPhraseContent.SelectionLength);
			}

			textBoxText = textBoxText.Insert(selectionStart, textToPaste);
			textBoxPhraseContent.Text = textBoxText;
			textBoxPhraseContent.SelectionStart = selectionStart + textToPaste.Length;
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (textBoxPhraseContent.SelectionLength == 0)
			{
				return;
			}

			int selStart = textBoxPhraseContent.SelectionStart;

			Clipboard.SetText(textBoxPhraseContent.Text.Substring(textBoxPhraseContent.SelectionStart, textBoxPhraseContent.SelectionLength));
			textBoxPhraseContent.Text = textBoxPhraseContent.Text.Remove(textBoxPhraseContent.SelectionStart, textBoxPhraseContent.SelectionLength);
			textBoxPhraseContent.SelectionStart = selStart;
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textBoxPhraseContent.SelectionStart = 0;
			textBoxPhraseContent.SelectionLength = textBoxPhraseContent.TextLength;
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (textBoxPhraseContent.SelectionLength == 0)
			{
				return;
			}

			int selStart = textBoxPhraseContent.SelectionStart;
			textBoxPhraseContent.Text = textBoxPhraseContent.Text.Remove(textBoxPhraseContent.SelectionStart, textBoxPhraseContent.SelectionLength);
			textBoxPhraseContent.SelectionStart = selStart;
		}

		void contextMenuStripPhraseContentEdit_Opening(object sender, CancelEventArgs e)
		{
			if (!textBoxPhraseContent.Focused)
			{
				textBoxPhraseContent.Focus();
			}

			contextMenuStripPhraseContentEdit.Items["copyToolStripMenuItem"].Enabled = textBoxPhraseContent.SelectionLength != 0;
			contextMenuStripPhraseContentEdit.Items["pasteToolStripMenuItem"].Enabled = Clipboard.ContainsText();
			contextMenuStripPhraseContentEdit.Items["cutToolStripMenuItem"].Enabled = textBoxPhraseContent.SelectionLength != 0;
			contextMenuStripPhraseContentEdit.Items["undoToolStripMenuItem"].Enabled = textBoxPhraseContent.CanUndo;
			contextMenuStripPhraseContentEdit.Items["deleteToolStripMenuItem"].Enabled = textBoxPhraseContent.SelectionLength != 0;
			contextMenuStripPhraseContentEdit.Items["selectAllToolStripMenuItem"].Enabled = textBoxPhraseContent.TextLength > 0;
		}

		private void keyActionMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddKeyCode formKeyCodes = new AddKeyCode();
			formKeyCodes.CenterTo(this);
			formKeyCodes.Show(this);
		}

		private void keyComboMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddShortcutKeys form = new AddShortcutKeys();
			form.CenterTo(this);
			form.Show(this);
		}

		private void textBoxPhraseContent_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && (e.KeyCode == Keys.A))
			{
				if (sender != null)
					((TextBox) sender).SelectAll();
				e.Handled = true;
			}
		}

		private void pauseMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddPauseMacros addPauseMacrosForm = new AddPauseMacros();
			addPauseMacrosForm.CenterTo(this);
			addPauseMacrosForm.Show(this);
		}

		private void dateAndTimeMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddDateMacros addDateMacrosForm = new AddDateMacros();
			addDateMacrosForm.CenterTo(this);
			addDateMacrosForm.Show(this);
		}

		private void debugWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DebugTools debugToolsWindow = new DebugTools(_keylogger);
			debugToolsWindow.CenterTo(this);
			debugToolsWindow.Show();
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			if (textBoxPhraseContent.CanUndo)
			{
				textBoxPhraseContent.Undo();
			}
		}

		private void textBoxPhraseContent_TextChanged(object sender, EventArgs e)
		{
		}

		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
/*
			if (e.Control && (e.KeyCode == Keys.S))
			{
				if (listViewPhrases.SelectedItems.Count == 0)
				{
					MessageBox.Show(this, "Please select item first", "Attention");
				}
				else
				{
					int selIndex = listViewPhrases.SelectedIndices[0];
					SavePhrase(selIndex);
					listViewPhrases.SelectedIndices.Clear();
					listViewPhrases.SelectedIndices.Add(selIndex);
				}

				e.Handled = true;
			}
*/
		}

		private XElement GetCurrentPhrase()
		{
			List<Panel> triggersPanels = groupBoxTriggers.Controls.Cast<Panel>().ToList();

			XElement triggerItem = new XElement("triggers");

			foreach (Panel panel in triggersPanels)
			{
				string triggerType = ((ComboBox) panel.Controls.Find("comboBoxTriggerType", false)[0]).SelectedItem.ToString();
				string triggerChar = ((TextBox) panel.Controls.Find("textBoxTriggerChar", false)[0]).Text;
				string triggerKey = ((ComboBox) panel.Controls.Find("comboBoxTriggerKey", false)[0]).SelectedItem.ToString().Split('|').Select(p => p.Trim()).First();
				bool triggerCaseSen = ((CheckBox) panel.Controls.Find("checkBoxTriggerCaseSensitive", false)[0]).Checked;

				if (triggerType == "Character")
				{
					triggerItem.Add(new XElement("item", new XAttribute("caseSensitive", triggerCaseSen), new XElement("value", new XCData(triggerChar))));
				}
				else if (triggerType == "Key")
				{
					triggerItem.Add(new XElement("item", new XAttribute("caseSensitive", triggerCaseSen), new XElement("value", new XCData("{" + triggerKey + "}"))));
				}
			}

			XElement resRule = new XElement("rule", new XElement("abbreviation", new XAttribute("caseSensitive", checkBoxAutotextCaseSensetive.Checked), new XElement("value", new XCData(textBoxAutotext.Text))), new XElement("removeAbbr", checkBoxSubstitute.Checked ? "true" : "false"), new XElement("phrase", new XCData(textBoxPhraseContent.Text)), new XElement("phraseCompiled", new XCData(textBoxPhraseContent.Text)), new XElement("macros", new XAttribute("mode", comboBoxProcessMacros.SelectedItem.ToString())), new XElement("description", new XCData(textBoxDescription.Text)), triggerItem);

			return resRule;
		}

		private bool IsCurrentPhraseDirty()
		{
			AutotextRuleConfig ruleToRewrite = _rules[_curSelectedPhraseIndex];

			XElement curRuleState = GetCurrentPhrase();
			XDocument doc = new XDocument(new XElement("autotextRules", new XElement("rules", curRuleState)));

			string xmlDocStr = doc.ToXmlString();
			AutotextRulesRoot root = ConfigHelper.DeserailizeXmlFromString<AutotextRulesRoot>(xmlDocStr);

			CompareLogic basicComparison = new CompareLogic();

			if (!basicComparison.Compare(root.AutotextRulesList.First(), ruleToRewrite).AreEqual)
			{
				return true;
			}

			return false;
		}

		private List<int> GetDataGridViewSelectedRows()
		{
			return dataGridViewPhrases.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();
		}

		private void dataGridViewPhrases_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
		{
			List<int> selIndeces = dataGridViewPhrases.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();
			
		}

		private void dataGridViewPhrases_SelectionChanged(object sender, EventArgs e)
		{
			List<int> selIndeces = dataGridViewPhrases.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();

			if (selIndeces.Count > 0)
			{
				_curSelectedPhraseIndex = selIndeces.First();

				_shift = 0;
				groupBoxTriggers.Controls.Clear();

				AutotextRuleConfig config = _rules[selIndeces.First()];

				textBoxDescription.Text = config.Description;
				textBoxPhraseContent.Text = config.Phrase;
				textBoxAutotext.Text = config.Abbreviation.AbbreviationText;
				checkBoxSubstitute.Checked = config.RemoveAbbr;
				checkBoxAutotextCaseSensetive.Checked = config.Abbreviation.CaseSensitive;

				comboBoxProcessMacros.Enabled = true;
				textBoxDescription.Enabled = true;
				textBoxAutotext.Enabled = true;
				checkBoxSubstitute.Enabled = true;
				checkBoxAutotextCaseSensetive.Enabled = true;
				textBoxPhraseContent.Enabled = true;


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

				groupBoxTriggers.Controls[0].Controls[5].Enabled = false;

			}
			else
			{
				if (IsCurrentPhraseDirty())
				{
					DialogResult dl = MessageBox.Show(this, "Currently selected phrase has unsaved changes. Save changes?", "Confirmation",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Question);

					switch (dl)
					{
						case DialogResult.Cancel:
							break;
						case DialogResult.Yes:
							break;
						case DialogResult.No:
							break;
					}
				}

				groupBoxTriggers.Controls.Clear();

				textBoxDescription.Text = "";
				textBoxPhraseContent.Text = "";
				textBoxAutotext.Text = "";
				checkBoxSubstitute.Checked = false;
				checkBoxAutotextCaseSensetive.Checked = false;

				comboBoxProcessMacros.Enabled = false;
				textBoxDescription.Enabled = false;
				textBoxAutotext.Enabled = false;
				checkBoxSubstitute.Enabled = false;
				checkBoxAutotextCaseSensetive.Enabled = false;
				textBoxPhraseContent.Enabled = false;
			}
		}
	}
}
