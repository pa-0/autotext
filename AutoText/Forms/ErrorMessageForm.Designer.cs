namespace AutoText.Forms
{
	partial class ErrorMessageForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelErrorMessage = new System.Windows.Forms.Label();
			this.textBoxExceptionDetails = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelErrorCrytical = new System.Windows.Forms.Label();
			this.buttonAction = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(77, 78);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(95, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(318, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Unexpected error occurred.  Error message:";
			// 
			// labelErrorMessage
			// 
			this.labelErrorMessage.AutoSize = true;
			this.labelErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelErrorMessage.Location = new System.Drawing.Point(110, 51);
			this.labelErrorMessage.Name = "labelErrorMessage";
			this.labelErrorMessage.Size = new System.Drawing.Size(28, 17);
			this.labelErrorMessage.TabIndex = 2;
			this.labelErrorMessage.Text = "----";
			// 
			// textBoxExceptionDetails
			// 
			this.textBoxExceptionDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExceptionDetails.Location = new System.Drawing.Point(12, 138);
			this.textBoxExceptionDetails.Multiline = true;
			this.textBoxExceptionDetails.Name = "textBoxExceptionDetails";
			this.textBoxExceptionDetails.ReadOnly = true;
			this.textBoxExceptionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxExceptionDetails.Size = new System.Drawing.Size(501, 207);
			this.textBoxExceptionDetails.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 118);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Error details";
			// 
			// labelErrorCrytical
			// 
			this.labelErrorCrytical.AutoSize = true;
			this.labelErrorCrytical.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelErrorCrytical.Location = new System.Drawing.Point(96, 85);
			this.labelErrorCrytical.Name = "labelErrorCrytical";
			this.labelErrorCrytical.Size = new System.Drawing.Size(164, 17);
			this.labelErrorCrytical.TabIndex = 6;
			this.labelErrorCrytical.Text = "Application will be closed";
			// 
			// buttonAction
			// 
			this.buttonAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAction.Location = new System.Drawing.Point(438, 351);
			this.buttonAction.Name = "buttonAction";
			this.buttonAction.Size = new System.Drawing.Size(75, 23);
			this.buttonAction.TabIndex = 7;
			this.buttonAction.Text = "OK";
			this.buttonAction.UseVisualStyleBackColor = true;
			this.buttonAction.Click += new System.EventHandler(this.buttonAction_Click);
			// 
			// ErrorMessageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(525, 386);
			this.Controls.Add(this.buttonAction);
			this.Controls.Add(this.labelErrorCrytical);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxExceptionDetails);
			this.Controls.Add(this.labelErrorMessage);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(440, 225);
			this.Name = "ErrorMessageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " AutoText -Unexpected error occurred";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelErrorMessage;
		private System.Windows.Forms.TextBox textBoxExceptionDetails;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelErrorCrytical;
		private System.Windows.Forms.Button buttonAction;
	}
}