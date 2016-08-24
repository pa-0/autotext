namespace AutoText.Forms
{
	partial class AddEnvironmentVariableMacros
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEnvironmentVariableMacros));
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxEnvVraNames = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelExample = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(205, 80);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(92, 23);
			this.buttonAdd.TabIndex = 0;
			this.buttonAdd.Text = "Insert macros";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(132, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Environmet variable name:";
			// 
			// comboBoxEnvVraNames
			// 
			this.comboBoxEnvVraNames.FormattingEnabled = true;
			this.comboBoxEnvVraNames.Location = new System.Drawing.Point(15, 25);
			this.comboBoxEnvVraNames.Name = "comboBoxEnvVraNames";
			this.comboBoxEnvVraNames.Size = new System.Drawing.Size(282, 21);
			this.comboBoxEnvVraNames.TabIndex = 2;
			this.comboBoxEnvVraNames.TextChanged += new System.EventHandler(this.comboBoxEnvVraNames_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Example:";
			// 
			// labelExample
			// 
			this.labelExample.AutoSize = true;
			this.labelExample.Location = new System.Drawing.Point(68, 49);
			this.labelExample.Name = "labelExample";
			this.labelExample.Size = new System.Drawing.Size(16, 13);
			this.labelExample.TabIndex = 4;
			this.labelExample.Text = "---";
			// 
			// AddEnvironmentVariableMacros
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(313, 118);
			this.Controls.Add(this.labelExample);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboBoxEnvVraNames);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonAdd);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddEnvironmentVariableMacros";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Environment Variable Macros";
			this.Load += new System.EventHandler(this.AddEnvironmentVariableMacros_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxEnvVraNames;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelExample;
	}
}