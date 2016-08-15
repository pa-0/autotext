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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AutoText.Model.Configuration;
using MoreLinq;

namespace AutoText.Forms
{
	public partial class EditAllowedDisallowedPrograms : Form
	{
		private bool _fileSelected;
		private AutotextRuleSpecificPrograms _programs;
		private BindingList<AutotextRuleSpecificProgram> _programsBindingList;
		private AutotextRuleConfiguration _autotextRule;
		private int _curSelDataGridViewRowIndex = -1;


		public EditAllowedDisallowedPrograms(AutotextRuleConfiguration autotextRule)
		{
			InitializeComponent();
			_autotextRule = autotextRule;

			if (_autotextRule.SpecificPrograms == null)
			{
				_autotextRule.SpecificPrograms = new AutotextRuleSpecificPrograms()
				{
					Programs =  new List<AutotextRuleSpecificProgram>()
				};
			}

			_programs = _autotextRule.SpecificPrograms;
			_programsBindingList = new BindingList<AutotextRuleSpecificProgram>(_programs.Programs);
			dataGridViewPrograms.AutoGenerateColumns = false;
			dataGridViewPrograms.DataSource = _programsBindingList;

			radioButtonAllow.Checked = _programs.ProgramsListType == SpecificProgramsListtype.Whitelist;
			radioButtonDisallow.Checked = _programs.ProgramsListType == SpecificProgramsListtype.Blacklist;

			comboBoxConditionsList.SelectedIndex = 0;

			comboBoxProgramsList.Items.Add(new ProgramEntry()
			{
				ProgramDescription = "Select file...",
				ProgramModuleName = ""
			});

			Process[] processlist = Process.GetProcesses();
			processlist = processlist.Where(p => ((int)p.MainWindowHandle) != 0).ToArray();

			ProgramEntry[] programs = processlist.Select(p => new ProgramEntry()
			{
				ProgramModuleName = Path.GetFileName(p.MainModule.FileVersionInfo.FileName),
				ProgramDescription = p.MainModule.FileVersionInfo.FileDescription
			}).DistinctBy(p => p.ProgramDescription).ToArray();

			comboBoxProgramsList.Items.AddRange(programs);
		}

		private void EditAllowedDisallowedPrograms_Load(object sender, EventArgs e)
		{
			
		}

		private void comboBoxConditionsList_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxWindowTitle.Visible = comboBoxConditionsList.SelectedIndex != 0;
		}

		private void comboBoxProgramsList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProgramsList.SelectedIndex == 0)
			{
				openFileDialogSelectProgram.ShowDialog(this);

				if (!_fileSelected)
				{
					comboBoxProgramsList.SelectedIndex = 1;
				}
				else
				{
					_fileSelected = false;
				}
			}
		}

		private void openFileDialogSelectProgram_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_fileSelected = true;

			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(openFileDialogSelectProgram.FileName);

			if (comboBoxProgramsList.Items.Cast<ProgramEntry>().Any(p => p.ProgramModuleName == Path.GetFileName(fvi.FileName)))
			{
				comboBoxProgramsList.SelectedItem = comboBoxProgramsList.Items.Cast<ProgramEntry>().Single(p => p.ProgramModuleName == Path.GetFileName(fvi.FileName));
			}
			else
			{
				comboBoxProgramsList.Items.Insert(1, new ProgramEntry()
				{
					ProgramDescription = fvi.FileDescription,
					ProgramModuleName = Path.GetFileName(fvi.FileName)
				});

				comboBoxProgramsList.SelectedIndex = 1;
			}
			{ }
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			if (comboBoxProgramsList.SelectedIndex == -1)
			{
				MessageBox.Show(this, "Please select program to add", "AutoText", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			TitleCondition titleMatchCondition;

			switch (comboBoxConditionsList.SelectedItem.ToString())
			{
				case "any window title":
					titleMatchCondition = TitleCondition.Any;
					break;
				case "window title that exactly matches":
					titleMatchCondition = TitleCondition.Exact;
					break;
				case "window title that starts with":
					titleMatchCondition = TitleCondition.StartsWith;
					break;
				case "window title that ends with":
					titleMatchCondition = TitleCondition.EndsWith;
					break;
				case "window title that contain":
					titleMatchCondition = TitleCondition.Contains;
					break;

				default:
					throw new InvalidOperationException("Can't resolve title match condition");
			}

			ProgramEntry selEntry = (ProgramEntry)comboBoxProgramsList.SelectedItem;

			AutotextRuleSpecificProgram newProgram = new AutotextRuleSpecificProgram()
			{
				ProgramDescription = selEntry.ProgramDescription,
				ProgramModuleName = selEntry.ProgramModuleName,
				TitelMatchCondition = titleMatchCondition,
				TitleText = textBoxWindowTitle.Text
			};

			_programsBindingList.Add(newProgram);
		}

		private void radioButtonAllowDisallow_CheckedChanged(object sender, EventArgs e)
		{
			_programs.ProgramsListType = radioButtonAllow.Checked ? SpecificProgramsListtype.Whitelist : SpecificProgramsListtype.Blacklist;

			if (Owner != null)
			{
				((FormMain)Owner).SaveConfiguration();
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (_curSelDataGridViewRowIndex != -1)
			{
				_programsBindingList.RemoveAt(_curSelDataGridViewRowIndex);

				if (_programsBindingList.Count > 0 && _curSelDataGridViewRowIndex == -1)
				{
					dataGridViewPrograms.Rows[dataGridViewPrograms.Rows.Count - 1].Selected = true;
				}

				((FormMain)Owner).SaveConfiguration();
			}
		}

		private void SaveProgramEntry()
		{
			List<int> selIndeces = dataGridViewPrograms.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();

			if (selIndeces.Any())
			{
				AutotextRuleSpecificProgram selectedProgram = _programsBindingList[selIndeces.First()];
				ProgramEntry programEntry = (ProgramEntry)comboBoxProgramsList.SelectedItem;
				selectedProgram.ProgramModuleName = programEntry.ProgramModuleName;
				selectedProgram.ProgramDescription = programEntry.ProgramDescription;
				selectedProgram.TitleText = textBoxWindowTitle.Text;

				TitleCondition titleCondition;

				switch (comboBoxConditionsList.SelectedItem.ToString())
				{
					case "any window title":
						titleCondition = TitleCondition.Any;
						selectedProgram.TitleText = "";
						break;
					case "window title that exactly matches":
						titleCondition = TitleCondition.Exact;
						break;
					case "window title that starts with":
						titleCondition = TitleCondition.StartsWith;
						break;
					case "window title that ends with":
						titleCondition = TitleCondition.EndsWith;
						break;
					case "window title that contain":
						titleCondition = TitleCondition.Contains;
						break;

					default:
						throw new InvalidOperationException("Can't resolve title match condition");
				}

				selectedProgram.TitelMatchCondition = titleCondition;
				dataGridViewPrograms.Refresh();
			}

			((FormMain)Owner).SaveConfiguration();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveProgramEntry();
		}

		private void dataGridViewPrograms_SelectionChanged(object sender, EventArgs e)
		{
			List<int> selIndeces = dataGridViewPrograms.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();

			if (selIndeces.Any())
			{
				_curSelDataGridViewRowIndex = selIndeces.First();
				AutotextRuleSpecificProgram selectedProgram = _programsBindingList[_curSelDataGridViewRowIndex];
				ProgramEntry programEntry = comboBoxProgramsList.Items.Cast<ProgramEntry>().SingleOrDefault(p => p.ProgramModuleName == selectedProgram.ProgramModuleName);

				if (programEntry != null)
				{
					comboBoxProgramsList.SelectedItem = programEntry;
				}
				else
				{
					comboBoxProgramsList.Items.Add(new ProgramEntry()
					{
						ProgramModuleName = selectedProgram.ProgramModuleName,
						ProgramDescription = selectedProgram.ProgramDescription
					});
				}

				string cond = comboBoxConditionsList.Items.Cast<string>().Single(p => p == selectedProgram.TitelMatchConditionFormatted.Substring(5));
				comboBoxConditionsList.SelectedItem = cond;
				textBoxWindowTitle.Text = selectedProgram.TitleText;
			}
			else
			{
				_curSelDataGridViewRowIndex = -1;
			}
		}

		private void dataGridViewPrograms_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
		{
			if (dataGridViewPrograms.RowCount > 0 && _curSelDataGridViewRowIndex != -1 && dataGridViewPrograms.ClientRectangle.Contains(PointToClient(Control.MousePosition)) && IsCurrentEntryDirty())
			{
				DialogResult dl = MessageBox.Show(this, "Currently selected program entry has unsaved changes. Save changes?", "Confirmation",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				switch (dl)
				{
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
					case DialogResult.Yes:
						SaveProgramEntry();
						break;
					case DialogResult.No:
						break;
				}
			}
		}

		private bool IsCurrentEntryDirty()
		{
			AutotextRuleSpecificProgram selectedProgram = _programsBindingList[_curSelDataGridViewRowIndex];
			ProgramEntry programEntry = (ProgramEntry) comboBoxProgramsList.SelectedItem;

			if (selectedProgram.ProgramModuleName != programEntry.ProgramModuleName ||
				textBoxWindowTitle.Text != selectedProgram.TitleText ||
				comboBoxConditionsList.SelectedItem.ToString() != selectedProgram.TitelMatchConditionFormatted.Substring(5))
			{

				return true;
			}

			return false;
		}

		private void EditAllowedDisallowedPrograms_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (dataGridViewPrograms.RowCount > 0 && _curSelDataGridViewRowIndex != -1  && IsCurrentEntryDirty())
			{
				DialogResult dl = MessageBox.Show(this, "Currently selected program entry has unsaved changes. Save changes?", "Confirmation",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				switch (dl)
				{
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
					case DialogResult.Yes:
						SaveProgramEntry();
						break;
					case DialogResult.No:
						break;
				}
			}
		}

		private class ProgramEntry
		{
			public string ProgramModuleName { get; set; }
			public string ProgramDescription { get; set; }

			public override string ToString()
			{
				if (ProgramDescription == "Select file...")
				{
					return ProgramDescription;
				}

				if (string.IsNullOrEmpty(ProgramDescription) || string.IsNullOrWhiteSpace(ProgramDescription))
				{
					return ProgramModuleName;
				}
				else
				{
					return ProgramDescription + " (" + ProgramModuleName + ")";
				}
			}
		}
	}


	
}
