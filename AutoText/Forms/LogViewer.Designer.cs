namespace AutoText.Forms
{
	partial class LogViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewer));
			this.checkBoxStayOnTop = new System.Windows.Forms.CheckBox();
			this.checkBoxScrollToBottom = new System.Windows.Forms.CheckBox();
			this.numericUpDownMaxLogEntries = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.checkBoxWordWrap = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLogEntries)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxStayOnTop
			// 
			this.checkBoxStayOnTop.AutoSize = true;
			this.checkBoxStayOnTop.Checked = true;
			this.checkBoxStayOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxStayOnTop.Location = new System.Drawing.Point(360, 15);
			this.checkBoxStayOnTop.Name = "checkBoxStayOnTop";
			this.checkBoxStayOnTop.Size = new System.Drawing.Size(80, 17);
			this.checkBoxStayOnTop.TabIndex = 0;
			this.checkBoxStayOnTop.Text = "Stay on top";
			this.checkBoxStayOnTop.UseVisualStyleBackColor = true;
			this.checkBoxStayOnTop.CheckedChanged += new System.EventHandler(this.checkBoxStayOnTop_CheckedChanged);
			// 
			// checkBoxScrollToBottom
			// 
			this.checkBoxScrollToBottom.AutoSize = true;
			this.checkBoxScrollToBottom.Checked = true;
			this.checkBoxScrollToBottom.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxScrollToBottom.Location = new System.Drawing.Point(192, 15);
			this.checkBoxScrollToBottom.Name = "checkBoxScrollToBottom";
			this.checkBoxScrollToBottom.Size = new System.Drawing.Size(162, 17);
			this.checkBoxScrollToBottom.TabIndex = 1;
			this.checkBoxScrollToBottom.Text = "Automatically scroll to bottom";
			this.checkBoxScrollToBottom.UseVisualStyleBackColor = true;
			// 
			// numericUpDownMaxLogEntries
			// 
			this.numericUpDownMaxLogEntries.Location = new System.Drawing.Point(96, 12);
			this.numericUpDownMaxLogEntries.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numericUpDownMaxLogEntries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownMaxLogEntries.Name = "numericUpDownMaxLogEntries";
			this.numericUpDownMaxLogEntries.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMaxLogEntries.TabIndex = 2;
			this.numericUpDownMaxLogEntries.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Max log entries";
			// 
			// textBoxLog
			// 
			this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLog.Location = new System.Drawing.Point(12, 38);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.ReadOnly = true;
			this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLog.Size = new System.Drawing.Size(698, 370);
			this.textBoxLog.TabIndex = 4;
			// 
			// checkBoxWordWrap
			// 
			this.checkBoxWordWrap.AutoSize = true;
			this.checkBoxWordWrap.Checked = true;
			this.checkBoxWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxWordWrap.Location = new System.Drawing.Point(446, 15);
			this.checkBoxWordWrap.Name = "checkBoxWordWrap";
			this.checkBoxWordWrap.Size = new System.Drawing.Size(78, 17);
			this.checkBoxWordWrap.TabIndex = 5;
			this.checkBoxWordWrap.Text = "Word wrap";
			this.checkBoxWordWrap.UseVisualStyleBackColor = true;
			this.checkBoxWordWrap.CheckedChanged += new System.EventHandler(this.checkBoxWordWrap_CheckedChanged);
			// 
			// LogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(722, 420);
			this.Controls.Add(this.checkBoxWordWrap);
			this.Controls.Add(this.textBoxLog);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numericUpDownMaxLogEntries);
			this.Controls.Add(this.checkBoxScrollToBottom);
			this.Controls.Add(this.checkBoxStayOnTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "LogViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Log Viewer";
			this.Load += new System.EventHandler(this.LogViewer_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLogEntries)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxStayOnTop;
		private System.Windows.Forms.CheckBox checkBoxScrollToBottom;
		private System.Windows.Forms.NumericUpDown numericUpDownMaxLogEntries;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.CheckBox checkBoxWordWrap;
	}
}