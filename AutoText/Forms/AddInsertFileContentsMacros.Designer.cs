namespace AutoText.Forms
{
	partial class AddInsertFileContentsMacros
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddInsertFileContentsMacros));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonAddMacros = new System.Windows.Forms.Button();
			this.textBoxPathToFile = new System.Windows.Forms.TextBox();
			this.comboBoxEncodings = new System.Windows.Forms.ComboBox();
			this.buttonChoseFile = new System.Windows.Forms.Button();
			this.openFileDialogFile = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Path to file:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "File encoding:";
			// 
			// buttonAddMacros
			// 
			this.buttonAddMacros.Location = new System.Drawing.Point(145, 73);
			this.buttonAddMacros.Name = "buttonAddMacros";
			this.buttonAddMacros.Size = new System.Drawing.Size(91, 23);
			this.buttonAddMacros.TabIndex = 2;
			this.buttonAddMacros.Text = "Insert macros";
			this.buttonAddMacros.UseVisualStyleBackColor = true;
			this.buttonAddMacros.Click += new System.EventHandler(this.buttonAddMacros_Click);
			// 
			// textBoxPathToFile
			// 
			this.textBoxPathToFile.Location = new System.Drawing.Point(91, 12);
			this.textBoxPathToFile.Name = "textBoxPathToFile";
			this.textBoxPathToFile.Size = new System.Drawing.Size(209, 20);
			this.textBoxPathToFile.TabIndex = 3;
			// 
			// comboBoxEncodings
			// 
			this.comboBoxEncodings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEncodings.FormattingEnabled = true;
			this.comboBoxEncodings.Location = new System.Drawing.Point(91, 42);
			this.comboBoxEncodings.Name = "comboBoxEncodings";
			this.comboBoxEncodings.Size = new System.Drawing.Size(209, 21);
			this.comboBoxEncodings.TabIndex = 4;
			// 
			// buttonChoseFile
			// 
			this.buttonChoseFile.Location = new System.Drawing.Point(308, 10);
			this.buttonChoseFile.Name = "buttonChoseFile";
			this.buttonChoseFile.Size = new System.Drawing.Size(75, 23);
			this.buttonChoseFile.TabIndex = 5;
			this.buttonChoseFile.Text = "Select File";
			this.buttonChoseFile.UseVisualStyleBackColor = true;
			this.buttonChoseFile.Click += new System.EventHandler(this.buttonChoseFile_Click);
			// 
			// openFileDialogFile
			// 
			this.openFileDialogFile.CheckFileExists = false;
			this.openFileDialogFile.CheckPathExists = false;
			this.openFileDialogFile.SupportMultiDottedExtensions = true;
			this.openFileDialogFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogFile_FileOk);
			// 
			// AddInsertFileContentsMacros
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(393, 108);
			this.Controls.Add(this.buttonChoseFile);
			this.Controls.Add(this.comboBoxEncodings);
			this.Controls.Add(this.textBoxPathToFile);
			this.Controls.Add(this.buttonAddMacros);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "AddInsertFileContentsMacros";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Insert File Contents Macros";
			this.Load += new System.EventHandler(this.AddInsertFileContentsMacros_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddInsertFileContentsMacros_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonAddMacros;
		private System.Windows.Forms.TextBox textBoxPathToFile;
		private System.Windows.Forms.ComboBox comboBoxEncodings;
		private System.Windows.Forms.Button buttonChoseFile;
		private System.Windows.Forms.OpenFileDialog openFileDialogFile;
	}
}