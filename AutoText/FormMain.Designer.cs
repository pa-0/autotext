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
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBoxBufferContents = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxBuffer = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(748, 614);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(811, 275);
			this.textBox1.TabIndex = 1;
			// 
			// textBoxBufferContents
			// 
			this.textBoxBufferContents.Location = new System.Drawing.Point(12, 328);
			this.textBoxBufferContents.Multiline = true;
			this.textBoxBufferContents.Name = "textBoxBufferContents";
			this.textBoxBufferContents.Size = new System.Drawing.Size(811, 249);
			this.textBoxBufferContents.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 301);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Buffer contents";
			// 
			// textBoxBuffer
			// 
			this.textBoxBuffer.Location = new System.Drawing.Point(101, 298);
			this.textBoxBuffer.Name = "textBoxBuffer";
			this.textBoxBuffer.ReadOnly = true;
			this.textBoxBuffer.Size = new System.Drawing.Size(722, 20);
			this.textBoxBuffer.TabIndex = 4;
			// 
			// FormMain
			// 
			this.ClientSize = new System.Drawing.Size(835, 649);
			this.Controls.Add(this.textBoxBuffer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxBufferContents);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "FormMain";
			this.Text = "AutoText";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button button1;
		private TextBox textBox1;
		private TextBox textBoxBufferContents;
		private Label label1;
		private TextBox textBoxBuffer;
	}
}

