namespace AutoText
{
	partial class AddShortcutKeys
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
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxKeys = new System.Windows.Forms.ComboBox();
			this.checkBoxControl = new System.Windows.Forms.CheckBox();
			this.checkBoxAlt = new System.Windows.Forms.CheckBox();
			this.checkBoxShift = new System.Windows.Forms.CheckBox();
			this.checkBoxWin = new System.Windows.Forms.CheckBox();
			this.checkBoxMenu = new System.Windows.Forms.CheckBox();
			this.listBoxKeysToPress = new System.Windows.Forms.ListBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(86, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Key to press";
			// 
			// comboBoxKeys
			// 
			this.comboBoxKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKeys.FormattingEnabled = true;
			this.comboBoxKeys.Location = new System.Drawing.Point(157, 10);
			this.comboBoxKeys.Name = "comboBoxKeys";
			this.comboBoxKeys.Size = new System.Drawing.Size(140, 21);
			this.comboBoxKeys.TabIndex = 3;
			// 
			// checkBoxControl
			// 
			this.checkBoxControl.AutoSize = true;
			this.checkBoxControl.Location = new System.Drawing.Point(12, 12);
			this.checkBoxControl.Name = "checkBoxControl";
			this.checkBoxControl.Size = new System.Drawing.Size(59, 17);
			this.checkBoxControl.TabIndex = 4;
			this.checkBoxControl.Text = "Control";
			this.checkBoxControl.UseVisualStyleBackColor = true;
			// 
			// checkBoxAlt
			// 
			this.checkBoxAlt.AutoSize = true;
			this.checkBoxAlt.Location = new System.Drawing.Point(12, 35);
			this.checkBoxAlt.Name = "checkBoxAlt";
			this.checkBoxAlt.Size = new System.Drawing.Size(38, 17);
			this.checkBoxAlt.TabIndex = 5;
			this.checkBoxAlt.Text = "Alt";
			this.checkBoxAlt.UseVisualStyleBackColor = true;
			// 
			// checkBoxShift
			// 
			this.checkBoxShift.AutoSize = true;
			this.checkBoxShift.Location = new System.Drawing.Point(12, 58);
			this.checkBoxShift.Name = "checkBoxShift";
			this.checkBoxShift.Size = new System.Drawing.Size(47, 17);
			this.checkBoxShift.TabIndex = 6;
			this.checkBoxShift.Text = "Shift";
			this.checkBoxShift.UseVisualStyleBackColor = true;
			// 
			// checkBoxWin
			// 
			this.checkBoxWin.AutoSize = true;
			this.checkBoxWin.Location = new System.Drawing.Point(12, 81);
			this.checkBoxWin.Name = "checkBoxWin";
			this.checkBoxWin.Size = new System.Drawing.Size(45, 17);
			this.checkBoxWin.TabIndex = 7;
			this.checkBoxWin.Text = "Win";
			this.checkBoxWin.UseVisualStyleBackColor = true;
			// 
			// checkBoxMenu
			// 
			this.checkBoxMenu.AutoSize = true;
			this.checkBoxMenu.Location = new System.Drawing.Point(12, 104);
			this.checkBoxMenu.Name = "checkBoxMenu";
			this.checkBoxMenu.Size = new System.Drawing.Size(53, 17);
			this.checkBoxMenu.TabIndex = 8;
			this.checkBoxMenu.Text = "Menu";
			this.checkBoxMenu.UseVisualStyleBackColor = true;
			// 
			// listBoxKeysToPress
			// 
			this.listBoxKeysToPress.FormattingEnabled = true;
			this.listBoxKeysToPress.Location = new System.Drawing.Point(89, 37);
			this.listBoxKeysToPress.Name = "listBoxKeysToPress";
			this.listBoxKeysToPress.Size = new System.Drawing.Size(257, 95);
			this.listBoxKeysToPress.TabIndex = 9;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(303, 8);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(43, 23);
			this.buttonAdd.TabIndex = 10;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Location = new System.Drawing.Point(89, 138);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(64, 23);
			this.buttonRemove.TabIndex = 11;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(271, 138);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 12;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// AddShortcutKeys
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(363, 175);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.listBoxKeysToPress);
			this.Controls.Add(this.checkBoxMenu);
			this.Controls.Add(this.checkBoxWin);
			this.Controls.Add(this.checkBoxShift);
			this.Controls.Add(this.checkBoxAlt);
			this.Controls.Add(this.checkBoxControl);
			this.Controls.Add(this.comboBoxKeys);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddShortcutKeys";
			this.Text = "Add shortcut keys";
			this.Load += new System.EventHandler(this.AddShortcutKeys_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxKeys;
		private System.Windows.Forms.CheckBox checkBoxControl;
		private System.Windows.Forms.CheckBox checkBoxAlt;
		private System.Windows.Forms.CheckBox checkBoxShift;
		private System.Windows.Forms.CheckBox checkBoxWin;
		private System.Windows.Forms.CheckBox checkBoxMenu;
		private System.Windows.Forms.ListBox listBoxKeysToPress;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonOk;
	}
}