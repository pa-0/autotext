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
		private bool _fileSelected = false;
		private AutotextRuleSpecificPrograms _programs;
		private BindingList<AutotextRuleSpecificProgram> _programsBindingList;


		public EditAllowedDisallowedPrograms(AutotextRuleSpecificPrograms programsToEdit)
		{
			InitializeComponent();
			_programs = programsToEdit;
			_programsBindingList = new BindingList<AutotextRuleSpecificProgram>(_programs.Programs);
			dataGridViewPrograms.AutoGenerateColumns = false;
			dataGridViewPrograms.DataSource = _programsBindingList;

			radioButtonAllow.Checked = programsToEdit.ProgramsListType == SpecificProgramsListtype.Whitelist;
			radioButtonDisallow.Checked = programsToEdit.ProgramsListType == SpecificProgramsListtype.Blacklist;
		}

		private void EditAllowedDisallowedPrograms_Load(object sender, EventArgs e)
		{
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
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			List<int> selIndeces = dataGridViewPrograms.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();

			if (selIndeces.Any())
			{
				_programsBindingList.RemoveAt(selIndeces.First());
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{

		}

		private void dataGridViewPrograms_SelectionChanged(object sender, EventArgs e)
		{
			List<int> selIndeces = dataGridViewPrograms.SelectedRows.Cast<DataGridViewRow>().Select(p => p.Index).ToList();

			if (selIndeces.Any())
			{

			}

			//AutotextRuleSpecificProgram program = _
		}
	}

	public class ProgramEntry
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
