/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

AutoText is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using AutoText.Engine;
using AutoText.Forms;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;
using AutoText.Model.Configuration;
using KellermanSoftware.CompareNetObjects;
using MoreLinq;


namespace AutoText
{
	public partial class FormMain : Form
	{

		private List<AutotextRuleConfiguration> _rules;
		private BindingList<AutotextRuleConfiguration> _rulesBindingList;
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
			Sender.StartSender();

			return;
			AutotextRulesRoot root = new AutotextRulesRoot()
			{
				AutotextRulesList = new List<AutotextRuleConfiguration>()
				{
					new AutotextRuleConfiguration()
					{
						Abbreviation = new AutotextRuleAbbreviation()
						{
							AbbreviationText = "asdasdasd",
							CaseSensitive = false
						},
						Description = "asdasdasdads",
						Phrase = "asdasdasdasd",
						RemoveAbbr = true,
						Macros = new AutotextRuleMacrosMode()
						{
							Mode = MacrosMode.Execute
						},
						Triggers = new List<AutotextRuleTrigger>()
						{
							{
								new AutotextRuleTrigger()
								{
									CaseSensitive = false,
									Value = "a",
									TriggerType = AutotextRuleTriggerType.Character
								}
							}
						},
						SpecificPrograms = new AutotextRuleSpecificPrograms()
						{
							ProgramsListType = SpecificProgramsListtype.Blacklist,
							Programs = new List<AutotextRuleSpecificProgram>()
							{
								{
									new AutotextRuleSpecificProgram()
									{
										TitelMatchCondition = TitleCondition.Exact,
										ProgramModuleName = "notepad.exe",
										ProgramDescription = "Notepad editor",
										TitleText = "Title text"

									}
								}
							}
						}
					}
				}
			};

			ConfigHelper.SaveAutotextRulesConfiguration(root.AutotextRulesList);
			{ }
		}

		private void LoadPhrasesToDataGridView()
		{
			try
			{
				dataGridViewPhrases.DataSource = _rulesBindingList;
			}
			catch (Exception ex)
			{
				//TODO catch that floating bug
			}
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

		private bool IsTitleMatched(TitleCondition titleCondition, string title, string foregroundWindowTitle)
		{
			switch (titleCondition)
			{
				case TitleCondition.Exact:
					return foregroundWindowTitle == title;
					break;
				case TitleCondition.StartsWith:
					return foregroundWindowTitle.StartsWith(title);
					break;
				case TitleCondition.EndsWith:
					return  foregroundWindowTitle.EndsWith(title);
					break;
				case TitleCondition.Contains:
					return  foregroundWindowTitle.Contains(title);
					break;
				case TitleCondition.Any:
					return true;
					break;
				default:
					throw new InvalidOperationException("Program match condition not recognized");
					break;
			}
		}

		void _matcher_MatchFound(object sender, AutotextMatchEventArgs e)
		{
			IntPtr hwnd = WinAPI.GetForegroundWindow();
			IntPtr pid;
			WinAPI.GetWindowThreadProcessId(hwnd, out pid);
			Process process = Process.GetProcessById((int)pid);
			string foregroundWindowTitle = GUIHelper.GetForegroundWindowTitle();


			#region Process phrase allow/deny programs list

			if (e.MatchedRule.SpecificPrograms != null &&
					e.MatchedRule.SpecificPrograms.ListEnabled &&
					e.MatchedRule.SpecificPrograms.Programs != null)
			{
				if (e.MatchedRule.SpecificPrograms.ProgramsListType == SpecificProgramsListtype.Whitelist &&
					e.MatchedRule.SpecificPrograms.Programs.Count == 0)
				{
					return;
				}

				List<AutotextRuleSpecificProgram> programs = e.MatchedRule.SpecificPrograms.Programs.Where(p => p.ProgramModuleName.ToLower() == Path.GetFileName(process.MainModule.FileName.ToLower())).ToList();
				bool titleMatched = false;

				foreach (AutotextRuleSpecificProgram program in programs)
				{
					titleMatched = IsTitleMatched(program.TitelMatchCondition, program.TitleText, foregroundWindowTitle);

					if (titleMatched)
					{
						break;
					}
				}

				if (e.MatchedRule.SpecificPrograms.ProgramsListType == SpecificProgramsListtype.Whitelist &&
					!titleMatched)
				{
					return;
				}

				if (e.MatchedRule.SpecificPrograms.ProgramsListType == SpecificProgramsListtype.Blacklist &&
				titleMatched)
				{
					return;
				}
			}

			#endregion

			#region Process global allow/deny programs list

			AutotextRuleSpecificPrograms globalList = ConfigHelper.GetCommonConfiguration().SpecificPrograms;

			if (globalList.ListEnabled)
			{
				if (globalList.ProgramsListType == SpecificProgramsListtype.Whitelist && globalList.Programs.Count == 0)
				{
					return;
				}

				List<AutotextRuleSpecificProgram> programs = globalList.Programs.Where(p => p.ProgramModuleName.ToLower() == Path.GetFileName(process.MainModule.FileName.ToLower())).ToList();
				bool titleMatched = false;

				foreach (AutotextRuleSpecificProgram program in programs)
				{
					titleMatched = IsTitleMatched(program.TitelMatchCondition, program.TitleText, foregroundWindowTitle);

					if (titleMatched)
					{
						break;
					}
				}

				if (globalList.ProgramsListType == SpecificProgramsListtype.Whitelist && !titleMatched)
				{
					return;
				}

				if (globalList.ProgramsListType == SpecificProgramsListtype.Blacklist && titleMatched)
				{
					return;
				}
			}

			#endregion

			_keylogger.PauseCapture();
			Thread.Sleep(20);
			AutotextRuleExecution.ProcessRule(new AutotextRuleMatchParameters(e.MatchedRule, e.Trigger));
			_keylogger.ResumeCapture();
			_matcher.ClearBuffer();
		}

		private void AddTriggerControls(string triggerChar, string triggerKey, bool? charIsKeySensitive)
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
			textBoxTriggerChar.Font = new Font(textBoxTriggerChar.Font, FontStyle.Regular);
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
			ConfigHelper.GetKeycodesConfiguration().Keycodes.Where(p => p.Names.Any(n => n.KeyRelation == KeyRelation.Sender)).ForEach(p => comboBoxTriggerKey.Items.Add(p.Names.Single(s => s.KeyRelation == KeyRelation.Sender).Value));
			comboBoxTriggerKey.Items.RemoveAt(0);
			comboBoxTriggerKey.SelectedIndex = 0;

			if (!string.IsNullOrEmpty(triggerKey))
			{
				foreach (var item in comboBoxTriggerKey.Items)
				{
					if (item.ToString() == triggerKey)
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
				((ComboBox)panel.Controls.Find("comboBoxTriggerType", false).First()).SelectedItem.ToString();

			if (selRes == "Key")
			{
				((TextBox)panel.Controls.Find("textBoxTriggerChar", false).First()).Hide();
				((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false).First()).Hide();
				((ComboBox)panel.Controls.Find("comboBoxTriggerKey", false).First()).Show();

			}
			else if (selRes == "Character")
			{
				((TextBox)panel.Controls.Find("textBoxTriggerChar", false).First()).Show();
				((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false).First()).Show();
				((ComboBox)panel.Controls.Find("comboBoxTriggerKey", false).First()).Hide();

			}
			else
			{
				throw new InvalidOperationException("Trigger type is not recognized");
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
			if (dataGridViewPhrases.RowCount > 0 && IsCurrentPhraseDirty())
			{
				DialogResult dl = MessageBox.Show(this, "Currently selected phrase has unsaved changes. Save changes?", "Confirmation",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				switch (dl)
				{
					case DialogResult.Cancel:
						return;
						break;
					case DialogResult.Yes:
						SavePhrase(_curSelectedPhraseIndex);
						break;
					case DialogResult.No:
						break;
				}
			}

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

			AddNewPhrase(nextNewPhraseAutotext);
			dataGridViewPhrases.Rows.Cast<DataGridViewRow>().ToList().ForEach(p => p.Selected = false);
			dataGridViewPhrases.Rows.Cast<DataGridViewRow>().Last().Selected = true;
			SaveConfiguration();
		}

		private bool SavePhrase(int phraseIndex)
		{
			if (string.IsNullOrEmpty(textBoxAutotext.Text))
			{
				MessageBox.Show(this, "Phrase autotext can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}

			if (_rules.Where((p, i) => i != phraseIndex).Any(p => p.Abbreviation.AbbreviationText == textBoxAutotext.Text))
			{
				MessageBox.Show(this, "Phrase with specified autotext is already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}

			AutotextRuleConfiguration ruleToSave = GetCurrentPhrase();

			if (ruleToSave.Triggers.Any(p => string.IsNullOrEmpty(p.Value)))
			{
				MessageBox.Show(this, "Character trigger field can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}

			_rulesBindingList[phraseIndex] = ruleToSave;
			dataGridViewPhrases.Refresh();
			SaveConfiguration();
			return true;
		}

		public void SaveConfiguration()
		{
			ConfigHelper.SaveAutotextRulesConfiguration(_rules);
		}

		private void AddNewPhrase(string abbreviationText)
		{
			AutotextRuleConfiguration newConfig = new AutotextRuleConfiguration();
			newConfig.Triggers = new List<AutotextRuleTrigger>();
			newConfig.Triggers.Add(new AutotextRuleTrigger()
			{
				CaseSensitive = false,
				Value = "Tab",
				TriggerType = AutotextRuleTriggerType.Key
			});

			newConfig.Abbreviation = new AutotextRuleAbbreviation()
			{
				AbbreviationText = abbreviationText,
				CaseSensitive = false,
			};

			newConfig.RemoveAbbr = true;
			newConfig.Phrase = "<phrase content>";
			newConfig.Macros = new AutotextRuleMacrosMode() { Mode = MacrosMode.Execute };
			newConfig.Description = "<phrase description>";
			newConfig.SpecificPrograms = new AutotextRuleSpecificPrograms()
			{
				Programs = new List<AutotextRuleSpecificProgram>()
			};

			_rulesBindingList.Add(newConfig);
		}

		private void buttonSavePhrase_Click(object sender, EventArgs e)
		{
			List<int> selRowsIndeces = GetDataGridViewSelectedRowIndeces();

			if (selRowsIndeces.Count == 0)
			{
				MessageBox.Show(this, "Please select item first", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			else
			{
				int selIndex = selRowsIndeces.First();
				SavePhrase(selIndex);
			}
		}

		private void RemovePhrase()
		{
			if (_rules.Any())
			{
				if (MessageBox.Show(this, "Are you sure that you want to delete selected phrase?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					_rulesBindingList.RemoveAt(_curSelectedPhraseIndex);

					int selIndex = -1;

					if (dataGridViewPhrases.Rows.Count > 0)
					{
						if (_curSelectedPhraseIndex <= (dataGridViewPhrases.Rows.Count - 1))
						{
							selIndex = _curSelectedPhraseIndex;
						}
						else
						{
							selIndex = dataGridViewPhrases.Rows.Count - 1;
						}
					}

					SaveConfiguration();

					if (selIndex != -1)
					{
						dataGridViewPhrases.Rows.Cast<DataGridViewRow>().ToList().ForEach(p => p.Selected = false);
						dataGridViewPhrases.Rows.Cast<DataGridViewRow>().ElementAt(selIndex).Selected = true;
					}
				}
			}
		}

		private void buttonRemovePhrase_Click(object sender, EventArgs e)
		{
			RemovePhrase();
		}

		private void FormMain_Activated(object sender, EventArgs e)
		{
			_keylogger.PauseCapture();
		}

		private void FormMain_Deactivate(object sender, EventArgs e)
		{
			_keylogger.ResumeCapture();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			if (!ConfigHelper.IsKeycodesConfigurationOk() ||
				!ConfigHelper.IsCommonConfigurationOk())
			{
				MessageBox.Show(this, "Failed to load program configuration files. Program will exit", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return;
			}

			dataGridViewPhrases.AutoGenerateColumns = false;
			_rules = ConfigHelper.GetAutotextRulesConfiguration();
			_rulesBindingList = new BindingList<AutotextRuleConfiguration>(_rules);
			_matcher = new AutotextMatcher(_rules);
			_matcher.MatchFound += _matcher_MatchFound;
			_keylogger.KeyCaptured += _testKeylogger_KeyCaptured;
			_keylogger.StartCapture();
			comboBoxProcessMacros.SelectedIndex = 0;
			contextMenuStripPhraseContentEdit.Opening += contextMenuStripPhraseContentEdit_Opening;

			LoadPhrasesToDataGridView();
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
					((TextBox)sender).SelectAll();
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
			if (e.Control && (e.KeyCode == Keys.S))
			{
				int selIndex = GetDataGridViewSelectedRowIndeces().First();
				SavePhrase(selIndex);
				e.Handled = true;
			}
		}

		private AutotextRuleConfiguration GetCurrentPhrase()
		{
			AutotextRuleConfiguration currentPhrase = new AutotextRuleConfiguration();
			currentPhrase.Triggers = new List<AutotextRuleTrigger>();

			currentPhrase.Abbreviation = new AutotextRuleAbbreviation()
			{
				AbbreviationText = textBoxAutotext.Text,
				CaseSensitive = checkBoxAutotextCaseSensetive.Checked,
			};

			currentPhrase.RemoveAbbr = checkBoxSubstitute.Checked;
			currentPhrase.Phrase = textBoxPhraseContent.Text;
			currentPhrase.Macros = new AutotextRuleMacrosMode() { Mode = (MacrosMode)Enum.Parse(typeof(MacrosMode), comboBoxProcessMacros.SelectedItem.ToString()) };
			currentPhrase.Description = textBoxDescription.Text;
			List<Panel> triggersPanels = groupBoxTriggers.Controls.Cast<Panel>().ToList();

			foreach (Panel panel in triggersPanels)
			{

				string triggerType = ((ComboBox)panel.Controls.Find("comboBoxTriggerType", false)[0]).SelectedItem.ToString();
				string triggerChar = ((TextBox)panel.Controls.Find("textBoxTriggerChar", false)[0]).Text;
				string triggerKey = ((ComboBox)panel.Controls.Find("comboBoxTriggerKey", false)[0]).SelectedItem.ToString();
				bool triggerCaseSen = ((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false)[0]).Checked;

				if (triggerType == "Character")
				{
					currentPhrase.Triggers.Add(new AutotextRuleTrigger()
					{
						CaseSensitive = triggerCaseSen,
						Value = triggerChar,
						TriggerType = AutotextRuleTriggerType.Character
					});
				}
				else if (triggerType == "Key")
				{
					currentPhrase.Triggers.Add(new AutotextRuleTrigger()
					{
						CaseSensitive = triggerCaseSen,
						Value = triggerKey,
						TriggerType = AutotextRuleTriggerType.Key
					});

				}
			}

			AutotextRuleSpecificPrograms copyOfSpecificPrograms = _rules[_curSelectedPhraseIndex].SpecificPrograms.Clone();
			currentPhrase.SpecificPrograms = copyOfSpecificPrograms;

			return currentPhrase;
		}

		private bool IsCurrentPhraseDirty()
		{
			if (_rules.Count > _curSelectedPhraseIndex)
			{
				AutotextRuleConfiguration ruleToRewrite = _rules[_curSelectedPhraseIndex];
				AutotextRuleConfiguration curRuleState = GetCurrentPhrase();
				CompareLogic basicComparison = new CompareLogic();
				ComparisonResult comparisonResult = basicComparison.Compare(curRuleState, ruleToRewrite);

				if (!comparisonResult.AreEqual)
				{
					return true;
				}
			}

			return false;
		}

		private List<int> GetDataGridViewSelectedRowIndeces()
		{
			return dataGridViewPhrases.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();
		}

		private void dataGridViewPhrases_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
		{
			if (dataGridViewPhrases.RowCount > 0 && GetDataGridViewSelectedRowIndeces().Any() && dataGridViewPhrases.ClientRectangle.Contains(PointToClient(Control.MousePosition)) && IsCurrentPhraseDirty())
			{
				DialogResult dl = MessageBox.Show(this, "Currently selected phrase has unsaved changes. Save changes?", "Confirmation",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				switch (dl)
				{
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
					case DialogResult.Yes:
						SavePhrase(_curSelectedPhraseIndex);
						break;
					case DialogResult.No:
						break;
				}
			}
		}

		private void dataGridViewPhrases_SelectionChanged(object sender, EventArgs e)
		{
			List<int> selIndeces = GetDataGridViewSelectedRowIndeces();

			if (selIndeces.Count > 0)
			{
				_curSelectedPhraseIndex = selIndeces.First();

				_shift = 0;
				groupBoxTriggers.Controls.Clear();

				AutotextRuleConfiguration config = _rules[selIndeces.First()];

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


				foreach (AutotextRuleTrigger trigger in config.Triggers)
				{
					if (trigger.TriggerType == AutotextRuleTriggerType.Key)
					{
						AddTriggerControls(null, trigger.Value, trigger.CaseSensitive);
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

		private void dataGridViewPhrases_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
			{
				DataGridViewColumn column = dataGridViewPhrases.Columns[e.ColumnIndex];

				if (column.DataPropertyName.Contains("."))
				{
					if (dataGridViewPhrases.Rows[e.RowIndex] != null)
					{
						object data = dataGridViewPhrases.Rows[e.RowIndex].DataBoundItem;

						string[] properties = column.DataPropertyName.Split('.');

						for (int i = 0; i < properties.Length && data != null; i++)
						{
							data = data.GetType().GetProperty(properties[i]).GetValue(data, null);
						}

						dataGridViewPhrases.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data;
					}
				}
			}
			catch (Exception ex)
			{
				//TODO Catch that floating bug
				Debug.WriteLine(ex.Message);
			}
		}

		private bool CheckSaveOnClose()
		{
			if (dataGridViewPhrases.RowCount > 0 && IsCurrentPhraseDirty())
			{
				DialogResult dl = MessageBox.Show(this, "Currently selected phrase has unsaved changes. Save changes?", "Confirmation",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				switch (dl)
				{
					case DialogResult.Cancel:
						return false;
						break;
					case DialogResult.Yes:
						return SavePhrase(_curSelectedPhraseIndex);
						break;
					case DialogResult.No:
						return true;
						break;
				}
			}

			return true;
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CheckSaveOnClose())
			{
				e.Cancel = true;
			}
			else
			{
				Sender.StopSender();
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormAbout formAbout = new FormAbout();
			formAbout.ShowDialog(this);
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
		}

		private void keyLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DebugTools debugToolsWindow = new DebugTools(_keylogger);
			debugToolsWindow.CenterTo(this);
			debugToolsWindow.Show();
		}

		private void randomStringMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRandomStringMacros addRandomStringMacros = new AddRandomStringMacros();
			addRandomStringMacros.CenterTo(this);
			addRandomStringMacros.Show(this);
		}

		private void randomNumberMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRandomNumberMacros addRandomNumberMacros = new AddRandomNumberMacros();
			addRandomNumberMacros.CenterTo(this);
			addRandomNumberMacros.Show(this);
		}

		private void insertFileContentsMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddInsertFileContentsMacros addInsertFileContentsMacros = new AddInsertFileContentsMacros();
			addInsertFileContentsMacros.CenterTo(this);
			addInsertFileContentsMacros.Show(this);
		}

		private void insertEnvironmentVariableValueMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddEnvironmentVariableMacros addEnvironmentVariableMacros = new AddEnvironmentVariableMacros();
			addEnvironmentVariableMacros.CenterTo(this);
			addEnvironmentVariableMacros.Show(this);
		}

		private void buttonAllowedDisallowedPrograms_Click(object sender, EventArgs e)
		{
			EditAllowedDisallowedPrograms allowedDisallowedPrograms = new EditAllowedDisallowedPrograms(_rules[_curSelectedPhraseIndex].SpecificPrograms, ProgramsConfigSource.Phrase);
			allowedDisallowedPrograms.ShowDialog(this);
		}

		private void dataGridViewPhrases_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				RemovePhrase();
			}
		}

		private void globalAllowedDisallowedProgramsListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditAllowedDisallowedPrograms allowedDisallowedPrograms = new EditAllowedDisallowedPrograms(ConfigHelper.GetCommonConfiguration().SpecificPrograms, ProgramsConfigSource.Global);
			allowedDisallowedPrograms.ShowDialog(this);
		}
	}
}
