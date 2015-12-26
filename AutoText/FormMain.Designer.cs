using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoText
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			this.button1 = new Button();
			this.textBox1 = new TextBox();
			this.textBoxBufferContents = new TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new Point(748, 614);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = ScrollBars.Both;
			this.textBox1.Size = new Size(811, 275);
			this.textBox1.TabIndex = 1;
			// 
			// textBoxBufferContents
			// 
			this.textBoxBufferContents.Location = new Point(12, 293);
			this.textBoxBufferContents.Multiline = true;
			this.textBoxBufferContents.Name = "textBoxBufferContents";
			this.textBoxBufferContents.Size = new Size(811, 284);
			this.textBoxBufferContents.TabIndex = 2;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new SizeF(6F, 13F);
			this.ClientSize = new Size(835, 649);
			this.Controls.Add(this.textBoxBufferContents);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "FormMain";
			this.Text = "AutoText";
			this.Load += new EventHandler(this.FormMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button button1;
		private TextBox textBox1;
		private TextBox textBoxBufferContents;
	}
}

