namespace AutoText.Forms
{
	partial class EditAllowedDisallowedPrograms
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAllowedDisallowedPrograms));
			this.radioButtonAllow = new System.Windows.Forms.RadioButton();
			this.radioButtonDisallow = new System.Windows.Forms.RadioButton();
			this.dataGridViewPrograms = new System.Windows.Forms.DataGridView();
			this.ColumnProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnWindowTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.comboBoxProgramsList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxConditionsList = new System.Windows.Forms.ComboBox();
			this.textBoxWindowTitle = new System.Windows.Forms.TextBox();
			this.openFileDialogSelectProgram = new System.Windows.Forms.OpenFileDialog();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.checkBoxPerProgramRestrictions = new System.Windows.Forms.CheckBox();
			this.panelControls = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrograms)).BeginInit();
			this.panelControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// radioButtonAllow
			// 
			this.radioButtonAllow.AutoSize = true;
			this.radioButtonAllow.Location = new System.Drawing.Point(3, 3);
			this.radioButtonAllow.Name = "radioButtonAllow";
			this.radioButtonAllow.Size = new System.Drawing.Size(173, 17);
			this.radioButtonAllow.TabIndex = 0;
			this.radioButtonAllow.TabStop = true;
			this.radioButtonAllow.Text = "Allow only in following programs";
			this.radioButtonAllow.UseVisualStyleBackColor = true;
			this.radioButtonAllow.CheckedChanged += new System.EventHandler(this.radioButtonAllowDisallow_CheckedChanged);
			// 
			// radioButtonDisallow
			// 
			this.radioButtonDisallow.AutoSize = true;
			this.radioButtonDisallow.Location = new System.Drawing.Point(182, 3);
			this.radioButtonDisallow.Name = "radioButtonDisallow";
			this.radioButtonDisallow.Size = new System.Drawing.Size(187, 17);
			this.radioButtonDisallow.TabIndex = 1;
			this.radioButtonDisallow.TabStop = true;
			this.radioButtonDisallow.Text = "Disallow only in following programs";
			this.radioButtonDisallow.UseVisualStyleBackColor = true;
			this.radioButtonDisallow.CheckedChanged += new System.EventHandler(this.radioButtonAllowDisallow_CheckedChanged);
			// 
			// dataGridViewPrograms
			// 
			this.dataGridViewPrograms.AllowUserToAddRows = false;
			this.dataGridViewPrograms.AllowUserToDeleteRows = false;
			this.dataGridViewPrograms.AllowUserToResizeRows = false;
			this.dataGridViewPrograms.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridViewPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPrograms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnProgram,
            this.ColumnCondition,
            this.ColumnWindowTitle});
			this.dataGridViewPrograms.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridViewPrograms.Location = new System.Drawing.Point(3, 26);
			this.dataGridViewPrograms.MultiSelect = false;
			this.dataGridViewPrograms.Name = "dataGridViewPrograms";
			this.dataGridViewPrograms.ReadOnly = true;
			this.dataGridViewPrograms.RowHeadersVisible = false;
			this.dataGridViewPrograms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewPrograms.Size = new System.Drawing.Size(605, 221);
			this.dataGridViewPrograms.TabIndex = 2;
			this.dataGridViewPrograms.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewPrograms_RowValidating);
			this.dataGridViewPrograms.SelectionChanged += new System.EventHandler(this.dataGridViewPrograms_SelectionChanged);
			// 
			// ColumnProgram
			// 
			this.ColumnProgram.DataPropertyName = "ProgramIdFormatted";
			this.ColumnProgram.HeaderText = "Program";
			this.ColumnProgram.Name = "ColumnProgram";
			this.ColumnProgram.ReadOnly = true;
			this.ColumnProgram.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnProgram.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnProgram.Width = 200;
			// 
			// ColumnCondition
			// 
			this.ColumnCondition.DataPropertyName = "TitelMatchConditionFormatted";
			this.ColumnCondition.HeaderText = "Condition";
			this.ColumnCondition.Name = "ColumnCondition";
			this.ColumnCondition.ReadOnly = true;
			this.ColumnCondition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnCondition.Width = 200;
			// 
			// ColumnWindowTitle
			// 
			this.ColumnWindowTitle.DataPropertyName = "TitleText";
			this.ColumnWindowTitle.HeaderText = "Title";
			this.ColumnWindowTitle.Name = "ColumnWindowTitle";
			this.ColumnWindowTitle.ReadOnly = true;
			this.ColumnWindowTitle.Width = 200;
			// 
			// comboBoxProgramsList
			// 
			this.comboBoxProgramsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProgramsList.FormattingEnabled = true;
			this.comboBoxProgramsList.Location = new System.Drawing.Point(51, 253);
			this.comboBoxProgramsList.Name = "comboBoxProgramsList";
			this.comboBoxProgramsList.Size = new System.Drawing.Size(557, 21);
			this.comboBoxProgramsList.TabIndex = 3;
			this.comboBoxProgramsList.SelectedIndexChanged += new System.EventHandler(this.comboBoxProgramsList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-1, 256);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Program";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(305, 276);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "with";
			// 
			// comboBoxConditionsList
			// 
			this.comboBoxConditionsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxConditionsList.FormattingEnabled = true;
			this.comboBoxConditionsList.Items.AddRange(new object[] {
            "any window title",
            "window title that exactly matches",
            "window title that starts with",
            "window title that ends with",
            "window title that contain"});
			this.comboBoxConditionsList.Location = new System.Drawing.Point(51, 292);
			this.comboBoxConditionsList.Name = "comboBoxConditionsList";
			this.comboBoxConditionsList.Size = new System.Drawing.Size(213, 21);
			this.comboBoxConditionsList.TabIndex = 6;
			this.comboBoxConditionsList.SelectedIndexChanged += new System.EventHandler(this.comboBoxConditionsList_SelectedIndexChanged);
			// 
			// textBoxWindowTitle
			// 
			this.textBoxWindowTitle.Location = new System.Drawing.Point(270, 292);
			this.textBoxWindowTitle.Name = "textBoxWindowTitle";
			this.textBoxWindowTitle.Size = new System.Drawing.Size(338, 20);
			this.textBoxWindowTitle.TabIndex = 7;
			// 
			// openFileDialogSelectProgram
			// 
			this.openFileDialogSelectProgram.CheckFileExists = false;
			this.openFileDialogSelectProgram.CheckPathExists = false;
			this.openFileDialogSelectProgram.DefaultExt = "exe";
			this.openFileDialogSelectProgram.Filter = "Executable files|*.exe";
			this.openFileDialogSelectProgram.SupportMultiDottedExtensions = true;
			this.openFileDialogSelectProgram.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogSelectProgram_FileOk);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(326, 318);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(90, 23);
			this.buttonAdd.TabIndex = 8;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(422, 318);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(90, 23);
			this.buttonDelete.TabIndex = 9;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(518, 318);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(90, 23);
			this.buttonSave.TabIndex = 10;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// checkBoxPerProgramRestrictions
			// 
			this.checkBoxPerProgramRestrictions.AutoSize = true;
			this.checkBoxPerProgramRestrictions.Location = new System.Drawing.Point(12, 12);
			this.checkBoxPerProgramRestrictions.Name = "checkBoxPerProgramRestrictions";
			this.checkBoxPerProgramRestrictions.Size = new System.Drawing.Size(171, 17);
			this.checkBoxPerProgramRestrictions.TabIndex = 11;
			this.checkBoxPerProgramRestrictions.Text = "Enable per program restrictions";
			this.checkBoxPerProgramRestrictions.UseVisualStyleBackColor = true;
			this.checkBoxPerProgramRestrictions.CheckedChanged += new System.EventHandler(this.checkBoxPerProgramRestrictions_CheckedChanged);
			// 
			// panelControls
			// 
			this.panelControls.Controls.Add(this.radioButtonAllow);
			this.panelControls.Controls.Add(this.radioButtonDisallow);
			this.panelControls.Controls.Add(this.buttonSave);
			this.panelControls.Controls.Add(this.dataGridViewPrograms);
			this.panelControls.Controls.Add(this.buttonDelete);
			this.panelControls.Controls.Add(this.comboBoxProgramsList);
			this.panelControls.Controls.Add(this.buttonAdd);
			this.panelControls.Controls.Add(this.label1);
			this.panelControls.Controls.Add(this.textBoxWindowTitle);
			this.panelControls.Controls.Add(this.label2);
			this.panelControls.Controls.Add(this.comboBoxConditionsList);
			this.panelControls.Location = new System.Drawing.Point(12, 35);
			this.panelControls.Name = "panelControls";
			this.panelControls.Size = new System.Drawing.Size(613, 346);
			this.panelControls.TabIndex = 12;
			// 
			// EditAllowedDisallowedPrograms
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 386);
			this.Controls.Add(this.panelControls);
			this.Controls.Add(this.checkBoxPerProgramRestrictions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "EditAllowedDisallowedPrograms";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Allowed/Disallowed Programs";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditAllowedDisallowedPrograms_FormClosing);
			this.Load += new System.EventHandler(this.EditAllowedDisallowedPrograms_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditAllowedDisallowedPrograms_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrograms)).EndInit();
			this.panelControls.ResumeLayout(false);
			this.panelControls.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radioButtonAllow;
		private System.Windows.Forms.RadioButton radioButtonDisallow;
		private System.Windows.Forms.DataGridView dataGridViewPrograms;
		private System.Windows.Forms.ComboBox comboBoxProgramsList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxConditionsList;
		private System.Windows.Forms.TextBox textBoxWindowTitle;
		private System.Windows.Forms.OpenFileDialog openFileDialogSelectProgram;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProgram;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCondition;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWindowTitle;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.CheckBox checkBoxPerProgramRestrictions;
		private System.Windows.Forms.Panel panelControls;
	}
}