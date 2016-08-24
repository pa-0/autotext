namespace AutoText.Forms
{
	partial class DebugTools
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugTools));
			this.textBoxKeyCaptured = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxStayOnTop = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textBoxKeyCaptured
			// 
			this.textBoxKeyCaptured.Location = new System.Drawing.Point(12, 32);
			this.textBoxKeyCaptured.Multiline = true;
			this.textBoxKeyCaptured.Name = "textBoxKeyCaptured";
			this.textBoxKeyCaptured.ReadOnly = true;
			this.textBoxKeyCaptured.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxKeyCaptured.Size = new System.Drawing.Size(354, 242);
			this.textBoxKeyCaptured.TabIndex = 0;
			this.textBoxKeyCaptured.TextChanged += new System.EventHandler(this.textBoxKeyCaptured_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Keys pressed";
			// 
			// checkBoxStayOnTop
			// 
			this.checkBoxStayOnTop.AutoSize = true;
			this.checkBoxStayOnTop.Checked = true;
			this.checkBoxStayOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxStayOnTop.Location = new System.Drawing.Point(286, 9);
			this.checkBoxStayOnTop.Name = "checkBoxStayOnTop";
			this.checkBoxStayOnTop.Size = new System.Drawing.Size(80, 17);
			this.checkBoxStayOnTop.TabIndex = 2;
			this.checkBoxStayOnTop.Text = "Stay on top";
			this.checkBoxStayOnTop.UseVisualStyleBackColor = true;
			this.checkBoxStayOnTop.CheckedChanged += new System.EventHandler(this.checkBoxStayOnTop_CheckedChanged);
			// 
			// DebugTools
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(378, 286);
			this.Controls.Add(this.checkBoxStayOnTop);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxKeyCaptured);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "DebugTools";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Debug Tools";
			this.Load += new System.EventHandler(this.DebugTools_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxKeyCaptured;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxStayOnTop;
	}
}