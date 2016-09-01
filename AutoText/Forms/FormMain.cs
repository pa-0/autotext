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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoText.Engine;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;
using AutoText.Model.Configuration;
using AutoText.Utility;
using KellermanSoftware.CompareNetObjects;

namespace AutoText.Forms
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


		public FormMain()
		{
			InitializeComponent();
			Sender.StartSender();
			Sender.DataSent += Sender_DataSent;

			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = @"d:\Downloads\";
			watcher.Filter = "AutoText.log";
			watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
			watcher.Changed += watcher_Changed;	
			watcher.EnableRaisingEvents = true;
		}

		void watcher_Changed(object sender, FileSystemEventArgs e)
		{
			MessageBox.Show("asdasd");
		}

		void Sender_DataSent(object sender, EventArgs e)
		{
			_keylogger.ResumeCapture();
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

#if DEBUG
				Debug.WriteLine(ex.Message);
				throw;
#endif
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
				if (!_matcher.TestForMatch(e))
				{
					_matcher.ClearBuffer();
					return;
				}
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
					return foregroundWindowTitle.EndsWith(title);
					break;
				case TitleCondition.Contains:
					return foregroundWindowTitle.Contains(title);
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

			try
			{
				AutotextRuleExecution.ProcessRule(new AutotextRuleMatchParameters(e.MatchedRule, e.Trigger));
			}
			catch (Exception ex)
			{
				notifyIcon.BalloonTipTitle = "Failed to execute phrase";
				notifyIcon.BalloonTipText = ex.Message;
				notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
				notifyIcon.ShowBalloonTip(3000);
				_keylogger.ResumeCapture();
			}

			_matcher.ClearBuffer();
		}

		private void AddTriggerControls(string triggerChar, string triggerKey, string triggerString, bool? triggerIsKeySensitive)
		{
			_numberOfTriggers++;

			Panel triggerPanel = new Panel();
			triggerPanel.Size = new Size(520, 27);
			triggerPanel.Location = new Point(6, 19 + _shift);
			//			triggerPanel.BorderStyle = BorderStyle.FixedSingle;
			triggerPanel.Name = "triggertsPanel" + _numberOfTriggers;

			ComboBox comboBoxTriggerType = new ComboBox();
			TextBox textBoxTriggerText = new TextBox();
			ComboBox comboBoxTriggerKey = new ComboBox();
			CheckBox charTriggerIsCaseSensitive = new CheckBox();
			Button buttonAddTrigger = new Button();
			Button buttonRemoveTrigger = new Button();


			// 
			// comboBoxTriggerType
			// 
			comboBoxTriggerType.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxTriggerType.FormattingEnabled = true;
			comboBoxTriggerType.Items.AddRange(new object[] { "Key", "One of chars", "String" });
			comboBoxTriggerType.Location = new Point(1, 2);
			comboBoxTriggerType.Name = "comboBoxTriggerType";
			comboBoxTriggerType.Size = new Size(100, 21);
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
			else if (triggerString != null)
			{
				comboBoxTriggerType.SelectedIndex = 2;
			}
			else
			{
				comboBoxTriggerType.SelectedIndex = 0;
			}

			comboBoxTriggerType.SelectedIndexChanged += comboBoxTriggerType_SelectedIndexChanged;
			// 
			// textBoxTriggerText
			// 
			textBoxTriggerText.Location = new Point(triggerPanel.Width - 410, 2);
			textBoxTriggerText.MaxLength = 1000;
			textBoxTriggerText.Name = "textBoxTriggerText";
			textBoxTriggerText.Size = new Size(250, 20);
			textBoxTriggerText.TabIndex = 16;
			textBoxTriggerText.Visible = triggerChar != null || triggerString != null;
			textBoxTriggerText.Font = new Font(textBoxTriggerText.Font, FontStyle.Regular);

			if (!string.IsNullOrEmpty(triggerChar))
			{
				textBoxTriggerText.Text = triggerChar;
			}
			else if (!string.IsNullOrEmpty(triggerString))
			{
				textBoxTriggerText.Text = triggerString;
			}

			// 
			// comboBoxTriggerKey
			// 
			comboBoxTriggerKey.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxTriggerKey.FormattingEnabled = true;
			comboBoxTriggerKey.Location = new Point(triggerPanel.Width - 410, 2);
			comboBoxTriggerKey.Name = "comboBoxTriggerKey";
			comboBoxTriggerKey.Size = new Size(135, 21);
			comboBoxTriggerKey.TabIndex = 17;
			comboBoxTriggerKey.Visible = triggerChar == null && triggerString == null;
			comboBoxTriggerKey.Font = new Font(comboBoxTriggerKey.Font, FontStyle.Regular);
			ConfigHelper.GetDisplayKeysTriggerListVisible().ForEach(p => comboBoxTriggerKey.Items.Add(p));
			comboBoxTriggerKey.SelectedIndex = 0;

			if (!string.IsNullOrEmpty(triggerKey))
			{
				foreach (var item in comboBoxTriggerKey.Items)
				{
					if (item.ToString() == ConfigHelper.GetDisplayKeyByNativeKey(triggerKey))
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
			charTriggerIsCaseSensitive.Location = new Point(triggerPanel.Width - 150, 5);
			charTriggerIsCaseSensitive.Name = "checkBoxTriggerCaseSensitive";
			charTriggerIsCaseSensitive.Size = new Size(94, 17);
			charTriggerIsCaseSensitive.TabIndex = 18;
			charTriggerIsCaseSensitive.Text = "Case sensitive";
			charTriggerIsCaseSensitive.UseVisualStyleBackColor = true;
			charTriggerIsCaseSensitive.Visible = triggerIsKeySensitive != null;
			charTriggerIsCaseSensitive.Checked = triggerIsKeySensitive == null ? false : (bool)triggerIsKeySensitive;
			charTriggerIsCaseSensitive.Font = new Font(charTriggerIsCaseSensitive.Font, FontStyle.Regular);

			// 
			// buttonAddTriggerButton
			// 
			buttonAddTrigger.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			buttonAddTrigger.Location = new Point(triggerPanel.Width - 55, 2);
			buttonAddTrigger.Name = "buttonAddTriggerButton";
			buttonAddTrigger.Size = new Size(24, 23);
			buttonAddTrigger.TabIndex = 19;
			buttonAddTrigger.Text = "+";
			buttonAddTrigger.UseVisualStyleBackColor = true;
			buttonAddTrigger.Click += buttonAddTriggerButton_Click;

			// 
			// buttonDelTriggerButton
			// 
			buttonRemoveTrigger.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			buttonRemoveTrigger.Location = new Point(triggerPanel.Width - 30, 2);
			buttonRemoveTrigger.Name = "buttonDelTriggerButton";
			buttonRemoveTrigger.Size = new Size(24, 23);
			buttonRemoveTrigger.TabIndex = 19;
			buttonRemoveTrigger.Text = "-";
			buttonRemoveTrigger.UseVisualStyleBackColor = true;
			buttonRemoveTrigger.Click += buttonRemoveTrigger_Click;

			triggerPanel.Controls.Add(comboBoxTriggerType);
			triggerPanel.Controls.Add(textBoxTriggerText);
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
			else
			{
				foreach (Panel panel in availPanels)
				{
					foreach (Control control in panel.Controls)
					{
						if (control is Button && control.Name.StartsWith("buttonDelTriggerButton"))
						{
							control.Enabled = true;
						}
					}
				}
			}
		}

		private void comboBoxTriggerType_SelectedIndexChanged(object sender, EventArgs e)
		{
			Panel panel = (Panel)((Control)sender).Parent;

			string selRes =
				((ComboBox)panel.Controls.Find("comboBoxTriggerType", false).First()).SelectedItem.ToString();

			if (selRes == "Key")
			{
				((TextBox)panel.Controls.Find("textBoxTriggerText", false).First()).Hide();
				((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false).First()).Hide();
				((ComboBox)panel.Controls.Find("comboBoxTriggerKey", false).First()).Show();

			}
			else if (selRes == "One of chars")
			{
				((TextBox)panel.Controls.Find("textBoxTriggerText", false).First()).Show();
				((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false).First()).Show();
				((ComboBox)panel.Controls.Find("comboBoxTriggerKey", false).First()).Hide();
			}
			else if (selRes == "String")
			{
				((TextBox)panel.Controls.Find("textBoxTriggerText", false).First()).Show();
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
			AddTriggerControls(null, null, null, null);

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
			else if (availPanels.Count > 0 && availPanels.Count < 7)
			{
				foreach (Panel panel in availPanels)
				{
					foreach (Control control in panel.Controls)
					{
						if (control is Button && control.Name.StartsWith("buttonDelTriggerButton"))
						{
							control.Enabled = true;
						}
					}
				}
			}
		}

		private void buttonAddPhrase_Click(object sender, EventArgs e)
		{
			if (dataGridViewPhrases.RowCount > 0 && IsCurrentPhraseDirty())
			{
				DialogResult dl = MessageBox.Show(this, "Currently selected phrase has unsaved changes. Save changes?", "AutoText",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				switch (dl)
				{
					case DialogResult.Cancel:
						return;
						break;
					case DialogResult.Yes:
						if (!SavePhrase(_curSelectedPhraseIndex))
						{
							return;
						}
						break;
					case DialogResult.No:
						break;
				}
			}

			IEnumerable<string> availPhraseAbbreviations = _rules.Select(p => p.Abbreviation.AbbreviationText);
			IEnumerable<string> matchedToDefNameAbbr =
				availPhraseAbbreviations.Where(p => Regex.IsMatch(p, Constants.CommonConstants.NewPhraseDefaultAutotextRegex));

			string nextNewPhraseAutotext;

			if (!matchedToDefNameAbbr.Any())
			{
				nextNewPhraseAutotext = string.Format(Constants.CommonConstants.NewPhraseDefaultAutotext, "");
			}
			//if we have autotext template with no numbers
			else if (matchedToDefNameAbbr.Count() == 1 && matchedToDefNameAbbr.First() == string.Format(Constants.CommonConstants.NewPhraseDefaultAutotext, ""))
			{
				nextNewPhraseAutotext = string.Format(Constants.CommonConstants.NewPhraseDefaultAutotext, "1");
			}
			else
			{
				int maxNum =
					//Where we have some number
					matchedToDefNameAbbr.Where(g => !string.IsNullOrEmpty(Regex.Match(g, Constants.CommonConstants.NewPhraseDefaultAutotextRegex).Groups[1].Value)).
					//Get that number max value
					Select(p => int.Parse(Regex.Match(p, Constants.CommonConstants.NewPhraseDefaultAutotextRegex).Groups[1].Value)).Max();
				nextNewPhraseAutotext = string.Format(Constants.CommonConstants.NewPhraseDefaultAutotext, maxNum + 1);
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
				MessageBox.Show(this, "Phrase autotext can't be empty", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}

			if (_rules.Where((p, i) => i != phraseIndex).Any(p => string.Equals(p.Abbreviation.AbbreviationText, textBoxAutotext.Text, StringComparison.CurrentCultureIgnoreCase)))
			{
				MessageBox.Show(this, "Phrase with specified autotext is already exists", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}

			AutotextRuleConfiguration ruleToSave = GetCurrentPhrase();

			if (ruleToSave.Triggers.Any(p => string.IsNullOrEmpty(p.Value)))
			{
				MessageBox.Show(this, "Trigger text field can't be empty", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
				MessageBox.Show(this, "Please select item first", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			else
			{
				int selIndex = selRowsIndeces.First();
				SavePhrase(selIndex);
			}
		}

		private void RemoveSelectedPhrase()
		{
			if (_rules.Any())
			{
				if (MessageBox.Show(this, "Are you sure that you want to delete selected phrase?", "AutoText", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
			RemoveSelectedPhrase();
			dataGridViewPhrases.Focus();
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
			dataGridViewPhrases.Focus();

			if (dataGridViewPhrases.RowCount == 0)
			{
				SetPhraseEditingAreaEnabledState(false);
			}
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				//notifyIcon.Visible = true;
				Hide();
			}
			else
			{
				//notifyIcon.Visible = false;
			}
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				Show();
				WindowState = FormWindowState.Normal;
			}
			else
			{
				WindowState = FormWindowState.Minimized;
				Hide();
			}
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
			InserTextToPhraseEditTextBox(textToPaste);
		}

		public void InserTextToPhraseEditTextBox(string textToInsert)
		{
			int selectionStart = textBoxPhraseContent.SelectionStart;
			string textBoxText = textBoxPhraseContent.Text;

			if (textBoxPhraseContent.SelectionLength != 0)
			{
				textBoxText = textBoxText.Remove(textBoxPhraseContent.SelectionStart, textBoxPhraseContent.SelectionLength);
			}

			textBoxText = textBoxText.Insert(selectionStart, textToInsert);
			textBoxPhraseContent.Text = textBoxText;
			textBoxPhraseContent.SelectionStart = selectionStart + textToInsert.Length;
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


		private void keyComboMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddShortcutKeys form = new AddShortcutKeys();
			form.Activated += childForm_Activated;
			form.Deactivate += childForm_Deactivate;
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
			addPauseMacrosForm.Activated += childForm_Activated;
			addPauseMacrosForm.Deactivate += childForm_Deactivate;
			addPauseMacrosForm.CenterTo(this);
			addPauseMacrosForm.Show(this);
		}

		void childForm_Deactivate(object sender, EventArgs e)
		{
			_keylogger.StartCapture();
		}

		void childForm_Activated(object sender, EventArgs e)
		{
			_keylogger.StopCapture();
		}


		private void dateAndTimeMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddDateMacros addDateMacrosForm = new AddDateMacros();
			addDateMacrosForm.Activated += childForm_Activated;
			addDateMacrosForm.Deactivate += childForm_Deactivate;
			addDateMacrosForm.CenterTo(this);
			addDateMacrosForm.Show(this);
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
			else if (e.KeyCode == Keys.Escape)
			{
				WindowState = FormWindowState.Minimized;
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
				string triggerText = ((TextBox)panel.Controls.Find("textBoxTriggerText", false)[0]).Text;
				string triggerKey = ((ComboBox)panel.Controls.Find("comboBoxTriggerKey", false)[0]).SelectedItem.ToString();
				bool triggerCaseSen = ((CheckBox)panel.Controls.Find("checkBoxTriggerCaseSensitive", false)[0]).Checked;

				if (triggerType == "One of chars")
				{
					currentPhrase.Triggers.Add(new AutotextRuleTrigger()
					{
						CaseSensitive = triggerCaseSen,
						Value = triggerText,
						TriggerType = AutotextRuleTriggerType.OneOfChars
					});
				}
				else if (triggerType == "Key")
				{
					currentPhrase.Triggers.Add(new AutotextRuleTrigger()
					{
						CaseSensitive = triggerCaseSen,
						Value = ConfigHelper.GetNativeKeyByDisplayKey(triggerKey).First(),
						TriggerType = AutotextRuleTriggerType.Key
					});
				}
				else if (triggerType == "String")
				{
					currentPhrase.Triggers.Add(new AutotextRuleTrigger()
					{
						CaseSensitive = triggerCaseSen,
						Value = triggerText,
						TriggerType = AutotextRuleTriggerType.String
					});
				}
				else
				{
					throw new InvalidOperationException("Trigger type is not recognized");
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
						e.Cancel = !SavePhrase(_curSelectedPhraseIndex);
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

				SetPhraseEditingAreaEnabledState(true);

				foreach (AutotextRuleTrigger trigger in config.Triggers)
				{
					if (trigger.TriggerType == AutotextRuleTriggerType.Key)
					{
						AddTriggerControls(null, trigger.Value, null, null);
					}
					else if (trigger.TriggerType == AutotextRuleTriggerType.OneOfChars)
					{
						AddTriggerControls(trigger.Value, null, null, trigger.CaseSensitive);
					}
					else if (trigger.TriggerType == AutotextRuleTriggerType.String)
					{
						AddTriggerControls(null, null, trigger.Value, trigger.CaseSensitive);
					}
					else
					{
						throw new InvalidOperationException("Can't recognize trigger type");
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

				if (groupBoxTriggers.Controls.Cast<Panel>().Count() == 1)
				{
					groupBoxTriggers.Controls[0].Controls[5].Enabled = false;
				}
			}
			else
			{
				groupBoxTriggers.Controls.Clear();
				SetPhraseEditingAreaEnabledState(false);
			}
		}

		private void SetPhraseEditingAreaEnabledState(bool isEnabled)
		{
			if (!isEnabled)
			{
				textBoxDescription.Text = "";
				textBoxPhraseContent.Text = "";
				textBoxAutotext.Text = "";
				checkBoxSubstitute.Checked = false;
				checkBoxAutotextCaseSensetive.Checked = false;
			}

			buttonSavePhrase.Enabled =
			comboBoxProcessMacros.Enabled =
			textBoxDescription.Enabled =
			textBoxAutotext.Enabled =
			checkBoxSubstitute.Enabled =
			checkBoxAutotextCaseSensetive.Enabled =
			textBoxPhraseContent.Enabled =
			buttonAllowedDisallowedPrograms.Enabled = isEnabled;
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
				DialogResult dl = MessageBox.Show(this, "Currently selected phrase has unsaved changes. Save changes?", "AutoText",
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
			addRandomStringMacros.Activated += childForm_Activated;
			addRandomStringMacros.Deactivate += childForm_Deactivate;
			addRandomStringMacros.CenterTo(this);
			addRandomStringMacros.Show(this);
		}

		private void randomNumberMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRandomNumberMacros addRandomNumberMacros = new AddRandomNumberMacros();
			addRandomNumberMacros.Activated += childForm_Activated;
			addRandomNumberMacros.Deactivate += childForm_Deactivate;
			addRandomNumberMacros.CenterTo(this);
			addRandomNumberMacros.Show(this);
		}

		private void insertFileContentsMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddInsertFileContentsMacros addInsertFileContentsMacros = new AddInsertFileContentsMacros();
			addInsertFileContentsMacros.Activated += childForm_Activated;
			addInsertFileContentsMacros.Deactivate += childForm_Deactivate;
			addInsertFileContentsMacros.CenterTo(this);
			addInsertFileContentsMacros.Show(this);
		}

		private void insertEnvironmentVariableValueMacrosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddEnvironmentVariableMacros addEnvironmentVariableMacros = new AddEnvironmentVariableMacros();
			addEnvironmentVariableMacros.Activated += childForm_Activated;
			addEnvironmentVariableMacros.Deactivate += childForm_Deactivate;
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
				RemoveSelectedPhrase();
			}
		}

		private void globalAllowedDisallowedProgramsListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditAllowedDisallowedPrograms allowedDisallowedPrograms = new EditAllowedDisallowedPrograms(ConfigHelper.GetCommonConfiguration().SpecificPrograms, ProgramsConfigSource.Global);
			allowedDisallowedPrograms.ShowDialog(this);
		}
	}
}
