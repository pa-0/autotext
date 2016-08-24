namespace AutoText.Forms
{
	partial class AddRandomNumberMacros
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRandomNumberMacros));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.numericUpDownMinimum = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownMaximum = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximum)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Minimum:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Maximum:";
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(130, 69);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(103, 23);
			this.buttonAdd.TabIndex = 4;
			this.buttonAdd.Text = "Insert macros";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// numericUpDownMinimum
			// 
			this.numericUpDownMinimum.Location = new System.Drawing.Point(69, 17);
			this.numericUpDownMinimum.Name = "numericUpDownMinimum";
			this.numericUpDownMinimum.Size = new System.Drawing.Size(164, 20);
			this.numericUpDownMinimum.TabIndex = 5;
			this.numericUpDownMinimum.ValueChanged += new System.EventHandler(this.numericUpDownMinimum_ValueChanged);
			// 
			// numericUpDownMaximum
			// 
			this.numericUpDownMaximum.Location = new System.Drawing.Point(69, 43);
			this.numericUpDownMaximum.Name = "numericUpDownMaximum";
			this.numericUpDownMaximum.Size = new System.Drawing.Size(164, 20);
			this.numericUpDownMaximum.TabIndex = 6;
			this.numericUpDownMaximum.ValueChanged += new System.EventHandler(this.numericUpDownMaximum_ValueChanged);
			// 
			// AddRandomNumberMacros
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(247, 103);
			this.Controls.Add(this.numericUpDownMaximum);
			this.Controls.Add(this.numericUpDownMinimum);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddRandomNumberMacros";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Random Number Macros";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximum)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.NumericUpDown numericUpDownMinimum;
		private System.Windows.Forms.NumericUpDown numericUpDownMaximum;
	}
}