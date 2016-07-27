namespace AutoText
{
	partial class AddKeyCode
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
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxKey = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxAction = new System.Windows.Forms.ComboBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(25, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Key";
			// 
			// comboBoxKey
			// 
			this.comboBoxKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKey.FormattingEnabled = true;
			this.comboBoxKey.Location = new System.Drawing.Point(55, 24);
			this.comboBoxKey.Name = "comboBoxKey";
			this.comboBoxKey.Size = new System.Drawing.Size(132, 21);
			this.comboBoxKey.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Action";
			// 
			// comboBoxAction
			// 
			this.comboBoxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAction.FormattingEnabled = true;
			this.comboBoxAction.Items.AddRange(new object[] {
            "Down",
            "Up",
            "On",
            "Off",
            "Toggle",
            "Press number of times"});
			this.comboBoxAction.Location = new System.Drawing.Point(55, 57);
			this.comboBoxAction.Name = "comboBoxAction";
			this.comboBoxAction.Size = new System.Drawing.Size(132, 21);
			this.comboBoxAction.TabIndex = 3;
			this.comboBoxAction.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(112, 84);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 4;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// numericUpDownQuantity
			// 
			this.numericUpDownQuantity.Location = new System.Drawing.Point(193, 58);
			this.numericUpDownQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numericUpDownQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownQuantity.Name = "numericUpDownQuantity";
			this.numericUpDownQuantity.Size = new System.Drawing.Size(47, 20);
			this.numericUpDownQuantity.TabIndex = 5;
			this.numericUpDownQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownQuantity.Visible = false;
			// 
			// AddKeyCode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(250, 121);
			this.Controls.Add(this.numericUpDownQuantity);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.comboBoxAction);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboBoxKey);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddKeyCode";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Add key macros";
			this.Load += new System.EventHandler(this.AddKeyCode_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxKey;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxAction;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.NumericUpDown numericUpDownQuantity;
	}
}