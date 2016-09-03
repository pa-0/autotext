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
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.checkBoxWordWrap = new System.Windows.Forms.CheckBox();
			this.buttonClearLog = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// checkBoxStayOnTop
			// 
			this.checkBoxStayOnTop.AutoSize = true;
			this.checkBoxStayOnTop.Checked = true;
			this.checkBoxStayOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxStayOnTop.Location = new System.Drawing.Point(180, 13);
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
			this.checkBoxScrollToBottom.Location = new System.Drawing.Point(12, 13);
			this.checkBoxScrollToBottom.Name = "checkBoxScrollToBottom";
			this.checkBoxScrollToBottom.Size = new System.Drawing.Size(162, 17);
			this.checkBoxScrollToBottom.TabIndex = 1;
			this.checkBoxScrollToBottom.Text = "Automatically scroll to bottom";
			this.checkBoxScrollToBottom.UseVisualStyleBackColor = true;
			this.checkBoxScrollToBottom.CheckedChanged += new System.EventHandler(this.checkBoxScrollToBottom_CheckedChanged);
			// 
			// textBoxLog
			// 
			this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxLog.Location = new System.Drawing.Point(12, 38);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.ReadOnly = true;
			this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLog.Size = new System.Drawing.Size(809, 457);
			this.textBoxLog.TabIndex = 4;
			// 
			// checkBoxWordWrap
			// 
			this.checkBoxWordWrap.AutoSize = true;
			this.checkBoxWordWrap.Checked = true;
			this.checkBoxWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxWordWrap.Location = new System.Drawing.Point(266, 13);
			this.checkBoxWordWrap.Name = "checkBoxWordWrap";
			this.checkBoxWordWrap.Size = new System.Drawing.Size(78, 17);
			this.checkBoxWordWrap.TabIndex = 5;
			this.checkBoxWordWrap.Text = "Word wrap";
			this.checkBoxWordWrap.UseVisualStyleBackColor = true;
			this.checkBoxWordWrap.CheckedChanged += new System.EventHandler(this.checkBoxWordWrap_CheckedChanged);
			// 
			// buttonClearLog
			// 
			this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearLog.Location = new System.Drawing.Point(746, 9);
			this.buttonClearLog.Name = "buttonClearLog";
			this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
			this.buttonClearLog.TabIndex = 6;
			this.buttonClearLog.Text = "Clear";
			this.buttonClearLog.UseVisualStyleBackColor = true;
			this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
			// 
			// LogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(833, 507);
			this.Controls.Add(this.buttonClearLog);
			this.Controls.Add(this.checkBoxWordWrap);
			this.Controls.Add(this.textBoxLog);
			this.Controls.Add(this.checkBoxScrollToBottom);
			this.Controls.Add(this.checkBoxStayOnTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "LogViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Log Viewer";
			this.Load += new System.EventHandler(this.LogViewer_Load);
			this.Shown += new System.EventHandler(this.LogViewer_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxStayOnTop;
		private System.Windows.Forms.CheckBox checkBoxScrollToBottom;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.CheckBox checkBoxWordWrap;
		private System.Windows.Forms.Button buttonClearLog;
	}
}