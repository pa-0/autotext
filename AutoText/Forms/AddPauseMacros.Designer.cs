namespace AutoText
{
	partial class AddPauseMacros
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPauseMacros));
			this.numericUpDownPuseDuration = new System.Windows.Forms.NumericUpDown();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPuseDuration)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDownPuseDuration
			// 
			this.numericUpDownPuseDuration.Location = new System.Drawing.Point(55, 17);
			this.numericUpDownPuseDuration.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numericUpDownPuseDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownPuseDuration.Name = "numericUpDownPuseDuration";
			this.numericUpDownPuseDuration.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownPuseDuration.TabIndex = 0;
			this.numericUpDownPuseDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(64, 43);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(96, 23);
			this.buttonAdd.TabIndex = 1;
			this.buttonAdd.Text = "Insert macros";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Pause";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(181, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "ms";
			// 
			// AddPauseMacros
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(219, 79);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.numericUpDownPuseDuration);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddPauseMacros";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Add Pause Macros";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPuseDuration)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDownPuseDuration;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}