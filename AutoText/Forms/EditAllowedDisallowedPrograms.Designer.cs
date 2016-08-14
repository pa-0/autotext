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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.ColumnProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnWindowTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.comboBoxProgramsList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxConditionsList = new System.Windows.Forms.ComboBox();
			this.textBoxWindowTitle = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// radioButtonAllow
			// 
			this.radioButtonAllow.AutoSize = true;
			this.radioButtonAllow.Location = new System.Drawing.Point(23, 12);
			this.radioButtonAllow.Name = "radioButtonAllow";
			this.radioButtonAllow.Size = new System.Drawing.Size(173, 17);
			this.radioButtonAllow.TabIndex = 0;
			this.radioButtonAllow.TabStop = true;
			this.radioButtonAllow.Text = "Allow only in following programs";
			this.radioButtonAllow.UseVisualStyleBackColor = true;
			// 
			// radioButtonDisallow
			// 
			this.radioButtonDisallow.AutoSize = true;
			this.radioButtonDisallow.Location = new System.Drawing.Point(202, 12);
			this.radioButtonDisallow.Name = "radioButtonDisallow";
			this.radioButtonDisallow.Size = new System.Drawing.Size(187, 17);
			this.radioButtonDisallow.TabIndex = 1;
			this.radioButtonDisallow.TabStop = true;
			this.radioButtonDisallow.Text = "Disallow only in following programs";
			this.radioButtonDisallow.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnProgram,
            this.ColumnCondition,
            this.ColumnWindowTitle});
			this.dataGridView1.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView1.Location = new System.Drawing.Point(23, 35);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(605, 221);
			this.dataGridView1.TabIndex = 2;
			// 
			// ColumnProgram
			// 
			this.ColumnProgram.HeaderText = "Program";
			this.ColumnProgram.Name = "ColumnProgram";
			this.ColumnProgram.ReadOnly = true;
			this.ColumnProgram.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnProgram.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnProgram.Width = 200;
			// 
			// ColumnCondition
			// 
			this.ColumnCondition.HeaderText = "Condition";
			this.ColumnCondition.Name = "ColumnCondition";
			this.ColumnCondition.ReadOnly = true;
			this.ColumnCondition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnCondition.Width = 200;
			// 
			// ColumnWindowTitle
			// 
			this.ColumnWindowTitle.HeaderText = "Title";
			this.ColumnWindowTitle.Name = "ColumnWindowTitle";
			this.ColumnWindowTitle.ReadOnly = true;
			this.ColumnWindowTitle.Width = 200;
			// 
			// comboBoxProgramsList
			// 
			this.comboBoxProgramsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProgramsList.FormattingEnabled = true;
			this.comboBoxProgramsList.Location = new System.Drawing.Point(71, 262);
			this.comboBoxProgramsList.Name = "comboBoxProgramsList";
			this.comboBoxProgramsList.Size = new System.Drawing.Size(191, 21);
			this.comboBoxProgramsList.TabIndex = 3;
			this.comboBoxProgramsList.SelectedIndexChanged += new System.EventHandler(this.comboBoxProgramsList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 265);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Program";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(268, 265);
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
			this.comboBoxConditionsList.Location = new System.Drawing.Point(300, 262);
			this.comboBoxConditionsList.Name = "comboBoxConditionsList";
			this.comboBoxConditionsList.Size = new System.Drawing.Size(184, 21);
			this.comboBoxConditionsList.TabIndex = 6;
			this.comboBoxConditionsList.SelectedIndexChanged += new System.EventHandler(this.comboBoxConditionsList_SelectedIndexChanged);
			// 
			// textBoxWindowTitle
			// 
			this.textBoxWindowTitle.Location = new System.Drawing.Point(490, 262);
			this.textBoxWindowTitle.Name = "textBoxWindowTitle";
			this.textBoxWindowTitle.Size = new System.Drawing.Size(138, 20);
			this.textBoxWindowTitle.TabIndex = 7;
			// 
			// EditAllowedDisallowedPrograms
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 547);
			this.Controls.Add(this.textBoxWindowTitle);
			this.Controls.Add(this.comboBoxConditionsList);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxProgramsList);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.radioButtonDisallow);
			this.Controls.Add(this.radioButtonAllow);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "EditAllowedDisallowedPrograms";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Allowed/Disallowed Programs";
			this.Load += new System.EventHandler(this.EditAllowedDisallowedPrograms_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radioButtonAllow;
		private System.Windows.Forms.RadioButton radioButtonDisallow;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProgram;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCondition;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWindowTitle;
		private System.Windows.Forms.ComboBox comboBoxProgramsList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxConditionsList;
		private System.Windows.Forms.TextBox textBoxWindowTitle;
	}
}