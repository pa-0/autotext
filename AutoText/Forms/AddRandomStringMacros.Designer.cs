namespace AutoText.Forms
{
	partial class AddRandomStringMacros
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxCharsToContain = new System.Windows.Forms.TextBox();
			this.checkBoxContainFollowingChars = new System.Windows.Forms.CheckBox();
			this.checkBoxSpecialCharacters = new System.Windows.Forms.CheckBox();
			this.checkBoxDigits = new System.Windows.Forms.CheckBox();
			this.checkBoxLowercaseLetters = new System.Windows.Forms.CheckBox();
			this.checkBoxUppercaseLetters = new System.Windows.Forms.CheckBox();
			this.numericUpDownMinStringLength = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownMaxStringLenth = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonAddMacros = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStringLength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxStringLenth)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxCharsToContain);
			this.groupBox1.Controls.Add(this.checkBoxContainFollowingChars);
			this.groupBox1.Controls.Add(this.checkBoxSpecialCharacters);
			this.groupBox1.Controls.Add(this.checkBoxDigits);
			this.groupBox1.Controls.Add(this.checkBoxLowercaseLetters);
			this.groupBox1.Controls.Add(this.checkBoxUppercaseLetters);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(435, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "String should contain";
			// 
			// textBoxCharsToContain
			// 
			this.textBoxCharsToContain.Enabled = false;
			this.textBoxCharsToContain.Location = new System.Drawing.Point(174, 63);
			this.textBoxCharsToContain.Name = "textBoxCharsToContain";
			this.textBoxCharsToContain.Size = new System.Drawing.Size(251, 20);
			this.textBoxCharsToContain.TabIndex = 5;
			// 
			// checkBoxContainFollowingChars
			// 
			this.checkBoxContainFollowingChars.AutoSize = true;
			this.checkBoxContainFollowingChars.Location = new System.Drawing.Point(6, 65);
			this.checkBoxContainFollowingChars.Name = "checkBoxContainFollowingChars";
			this.checkBoxContainFollowingChars.Size = new System.Drawing.Size(162, 17);
			this.checkBoxContainFollowingChars.TabIndex = 4;
			this.checkBoxContainFollowingChars.Text = "Contain following characters:";
			this.checkBoxContainFollowingChars.UseVisualStyleBackColor = true;
			this.checkBoxContainFollowingChars.CheckedChanged += new System.EventHandler(this.checkBoxContainFollowingChars_CheckedChanged);
			// 
			// checkBoxSpecialCharacters
			// 
			this.checkBoxSpecialCharacters.AutoSize = true;
			this.checkBoxSpecialCharacters.Location = new System.Drawing.Point(174, 42);
			this.checkBoxSpecialCharacters.Name = "checkBoxSpecialCharacters";
			this.checkBoxSpecialCharacters.Size = new System.Drawing.Size(177, 17);
			this.checkBoxSpecialCharacters.TabIndex = 3;
			this.checkBoxSpecialCharacters.Text = "Special characters(!, @, #, etc.)";
			this.checkBoxSpecialCharacters.UseVisualStyleBackColor = true;
			// 
			// checkBoxDigits
			// 
			this.checkBoxDigits.AutoSize = true;
			this.checkBoxDigits.Location = new System.Drawing.Point(6, 42);
			this.checkBoxDigits.Name = "checkBoxDigits";
			this.checkBoxDigits.Size = new System.Drawing.Size(52, 17);
			this.checkBoxDigits.TabIndex = 2;
			this.checkBoxDigits.Text = "Digits";
			this.checkBoxDigits.UseVisualStyleBackColor = true;
			// 
			// checkBoxLowercaseLetters
			// 
			this.checkBoxLowercaseLetters.AutoSize = true;
			this.checkBoxLowercaseLetters.Location = new System.Drawing.Point(6, 19);
			this.checkBoxLowercaseLetters.Name = "checkBoxLowercaseLetters";
			this.checkBoxLowercaseLetters.Size = new System.Drawing.Size(109, 17);
			this.checkBoxLowercaseLetters.TabIndex = 1;
			this.checkBoxLowercaseLetters.Text = "Lowercase letters";
			this.checkBoxLowercaseLetters.UseVisualStyleBackColor = true;
			// 
			// checkBoxUppercaseLetters
			// 
			this.checkBoxUppercaseLetters.AutoSize = true;
			this.checkBoxUppercaseLetters.Location = new System.Drawing.Point(174, 19);
			this.checkBoxUppercaseLetters.Name = "checkBoxUppercaseLetters";
			this.checkBoxUppercaseLetters.Size = new System.Drawing.Size(109, 17);
			this.checkBoxUppercaseLetters.TabIndex = 0;
			this.checkBoxUppercaseLetters.Text = "Uppercase letters";
			this.checkBoxUppercaseLetters.UseVisualStyleBackColor = true;
			// 
			// numericUpDownMinStringLength
			// 
			this.numericUpDownMinStringLength.Location = new System.Drawing.Point(197, 118);
			this.numericUpDownMinStringLength.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.numericUpDownMinStringLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownMinStringLength.Name = "numericUpDownMinStringLength";
			this.numericUpDownMinStringLength.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMinStringLength.TabIndex = 1;
			this.numericUpDownMinStringLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownMinStringLength.ValueChanged += new System.EventHandler(this.numericUpDownMinStringLength_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(273, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(25, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "and";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(182, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Length of a string should be between";
			// 
			// numericUpDownMaxStringLenth
			// 
			this.numericUpDownMaxStringLenth.Location = new System.Drawing.Point(304, 118);
			this.numericUpDownMaxStringLenth.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.numericUpDownMaxStringLenth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownMaxStringLenth.Name = "numericUpDownMaxStringLenth";
			this.numericUpDownMaxStringLenth.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMaxStringLenth.TabIndex = 4;
			this.numericUpDownMaxStringLenth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownMaxStringLenth.ValueChanged += new System.EventHandler(this.numericUpDownMaxStringLenth_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(380, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "characters";
			// 
			// buttonAddMacros
			// 
			this.buttonAddMacros.Location = new System.Drawing.Point(372, 144);
			this.buttonAddMacros.Name = "buttonAddMacros";
			this.buttonAddMacros.Size = new System.Drawing.Size(75, 23);
			this.buttonAddMacros.TabIndex = 6;
			this.buttonAddMacros.Text = "Add";
			this.buttonAddMacros.UseVisualStyleBackColor = true;
			this.buttonAddMacros.Click += new System.EventHandler(this.buttonAddMacros_Click);
			// 
			// AddRandomStringMacros
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(465, 175);
			this.Controls.Add(this.buttonAddMacros);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numericUpDownMaxStringLenth);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numericUpDownMinStringLength);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddRandomStringMacros";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Add Random String Macros";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStringLength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxStringLenth)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxSpecialCharacters;
		private System.Windows.Forms.CheckBox checkBoxDigits;
		private System.Windows.Forms.CheckBox checkBoxLowercaseLetters;
		private System.Windows.Forms.CheckBox checkBoxUppercaseLetters;
		private System.Windows.Forms.TextBox textBoxCharsToContain;
		private System.Windows.Forms.CheckBox checkBoxContainFollowingChars;
		private System.Windows.Forms.NumericUpDown numericUpDownMinStringLength;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownMaxStringLenth;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonAddMacros;
	}
}