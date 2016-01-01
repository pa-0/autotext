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
			this.textBoxKeysLog = new System.Windows.Forms.TextBox();
			this.listViewPhrases = new System.Windows.Forms.ListView();
			this.columnHeaderAutotext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxPhraseContent = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxAutotext = new System.Windows.Forms.TextBox();
			this.checkBoxAutotextCaseSensetive = new System.Windows.Forms.CheckBox();
			this.checkBoxSubstitute = new System.Windows.Forms.CheckBox();
			this.groupBoxTriggers = new System.Windows.Forms.GroupBox();
			this.buttonRemovePhrase = new System.Windows.Forms.Button();
			this.buttonAddPhrase = new System.Windows.Forms.Button();
			this.buttonSavePhrase = new System.Windows.Forms.Button();
			this.comboBoxProcessMacros = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonAddKeyCodeEpression = new System.Windows.Forms.Button();
			this.buttonAddPauseScript = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxKeysLog
			// 
			this.textBoxKeysLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKeysLog.Location = new System.Drawing.Point(644, 325);
			this.textBoxKeysLog.Multiline = true;
			this.textBoxKeysLog.Name = "textBoxKeysLog";
			this.textBoxKeysLog.ReadOnly = true;
			this.textBoxKeysLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxKeysLog.Size = new System.Drawing.Size(219, 217);
			this.textBoxKeysLog.TabIndex = 1;
			this.textBoxKeysLog.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// listViewPhrases
			// 
			this.listViewPhrases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewPhrases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAutotext,
            this.columnHeaderDescription});
			this.listViewPhrases.FullRowSelect = true;
			this.listViewPhrases.Location = new System.Drawing.Point(12, 29);
			this.listViewPhrases.MultiSelect = false;
			this.listViewPhrases.Name = "listViewPhrases";
			this.listViewPhrases.Size = new System.Drawing.Size(310, 517);
			this.listViewPhrases.TabIndex = 5;
			this.listViewPhrases.UseCompatibleStateImageBehavior = false;
			this.listViewPhrases.View = System.Windows.Forms.View.Details;
			this.listViewPhrases.SelectedIndexChanged += new System.EventHandler(this.listViewPhrases_SelectedIndexChanged);
			this.listViewPhrases.Enter += new System.EventHandler(this.listViewPhrases_Enter);
			// 
			// columnHeaderAutotext
			// 
			this.columnHeaderAutotext.Text = "Autotext";
			this.columnHeaderAutotext.Width = 80;
			// 
			// columnHeaderDescription
			// 
			this.columnHeaderDescription.Text = "Description";
			this.columnHeaderDescription.Width = 200;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(13, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Phrases";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(325, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Description";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDescription.Location = new System.Drawing.Point(328, 29);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(535, 20);
			this.textBoxDescription.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(325, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Phrase content";
			// 
			// textBoxPhraseContent
			// 
			this.textBoxPhraseContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPhraseContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxPhraseContent.Location = new System.Drawing.Point(328, 91);
			this.textBoxPhraseContent.Multiline = true;
			this.textBoxPhraseContent.Name = "textBoxPhraseContent";
			this.textBoxPhraseContent.Size = new System.Drawing.Size(535, 186);
			this.textBoxPhraseContent.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(328, 286);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Autotext";
			// 
			// textBoxAutotext
			// 
			this.textBoxAutotext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAutotext.Location = new System.Drawing.Point(385, 283);
			this.textBoxAutotext.Name = "textBoxAutotext";
			this.textBoxAutotext.Size = new System.Drawing.Size(253, 20);
			this.textBoxAutotext.TabIndex = 12;
			// 
			// checkBoxAutotextCaseSensetive
			// 
			this.checkBoxAutotextCaseSensetive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxAutotextCaseSensetive.AutoSize = true;
			this.checkBoxAutotextCaseSensetive.Location = new System.Drawing.Point(768, 285);
			this.checkBoxAutotextCaseSensetive.Name = "checkBoxAutotextCaseSensetive";
			this.checkBoxAutotextCaseSensetive.Size = new System.Drawing.Size(94, 17);
			this.checkBoxAutotextCaseSensetive.TabIndex = 13;
			this.checkBoxAutotextCaseSensetive.Text = "Case sensitive";
			this.checkBoxAutotextCaseSensetive.UseVisualStyleBackColor = true;
			// 
			// checkBoxSubstitute
			// 
			this.checkBoxSubstitute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxSubstitute.AutoSize = true;
			this.checkBoxSubstitute.Checked = true;
			this.checkBoxSubstitute.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxSubstitute.Location = new System.Drawing.Point(644, 285);
			this.checkBoxSubstitute.Name = "checkBoxSubstitute";
			this.checkBoxSubstitute.Size = new System.Drawing.Size(122, 17);
			this.checkBoxSubstitute.TabIndex = 20;
			this.checkBoxSubstitute.Text = "Substitute by phrase";
			this.checkBoxSubstitute.UseVisualStyleBackColor = true;
			// 
			// groupBoxTriggers
			// 
			this.groupBoxTriggers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBoxTriggers.Location = new System.Drawing.Point(328, 309);
			this.groupBoxTriggers.Name = "groupBoxTriggers";
			this.groupBoxTriggers.Size = new System.Drawing.Size(310, 238);
			this.groupBoxTriggers.TabIndex = 21;
			this.groupBoxTriggers.TabStop = false;
			this.groupBoxTriggers.Text = "Triggers";
			// 
			// buttonRemovePhrase
			// 
			this.buttonRemovePhrase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemovePhrase.Location = new System.Drawing.Point(166, 552);
			this.buttonRemovePhrase.Name = "buttonRemovePhrase";
			this.buttonRemovePhrase.Size = new System.Drawing.Size(75, 23);
			this.buttonRemovePhrase.TabIndex = 22;
			this.buttonRemovePhrase.Text = "Remove";
			this.buttonRemovePhrase.UseVisualStyleBackColor = true;
			this.buttonRemovePhrase.Click += new System.EventHandler(this.buttonRemovePhrase_Click);
			// 
			// buttonAddPhrase
			// 
			this.buttonAddPhrase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddPhrase.Location = new System.Drawing.Point(247, 552);
			this.buttonAddPhrase.Name = "buttonAddPhrase";
			this.buttonAddPhrase.Size = new System.Drawing.Size(75, 23);
			this.buttonAddPhrase.TabIndex = 23;
			this.buttonAddPhrase.Text = "Add";
			this.buttonAddPhrase.UseVisualStyleBackColor = true;
			this.buttonAddPhrase.Click += new System.EventHandler(this.buttonAddPhrase_Click);
			// 
			// buttonSavePhrase
			// 
			this.buttonSavePhrase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSavePhrase.Location = new System.Drawing.Point(787, 548);
			this.buttonSavePhrase.Name = "buttonSavePhrase";
			this.buttonSavePhrase.Size = new System.Drawing.Size(75, 23);
			this.buttonSavePhrase.TabIndex = 24;
			this.buttonSavePhrase.Text = "Save";
			this.buttonSavePhrase.UseVisualStyleBackColor = true;
			this.buttonSavePhrase.Click += new System.EventHandler(this.buttonSavePhrase_Click);
			// 
			// comboBoxProcessMacros
			// 
			this.comboBoxProcessMacros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProcessMacros.FormattingEnabled = true;
			this.comboBoxProcessMacros.Items.AddRange(new object[] {
            "Execute",
            "Skip"});
			this.comboBoxProcessMacros.Location = new System.Drawing.Point(697, 64);
			this.comboBoxProcessMacros.Name = "comboBoxProcessMacros";
			this.comboBoxProcessMacros.Size = new System.Drawing.Size(121, 21);
			this.comboBoxProcessMacros.TabIndex = 26;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(649, 67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 27;
			this.label1.Text = "Macros";
			// 
			// button1
			// 
			this.button1.Image = global::AutoText.Properties.Resources.keys_combo_30_30;
			this.button1.Location = new System.Drawing.Point(462, 56);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 32);
			this.button1.TabIndex = 28;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonAddKeyCodeEpression
			// 
			this.buttonAddKeyCodeEpression.Image = global::AutoText.Properties.Resources.keycode_small;
			this.buttonAddKeyCodeEpression.Location = new System.Drawing.Point(424, 56);
			this.buttonAddKeyCodeEpression.Name = "buttonAddKeyCodeEpression";
			this.buttonAddKeyCodeEpression.Size = new System.Drawing.Size(32, 32);
			this.buttonAddKeyCodeEpression.TabIndex = 25;
			this.buttonAddKeyCodeEpression.UseVisualStyleBackColor = true;
			this.buttonAddKeyCodeEpression.Click += new System.EventHandler(this.buttonAddKeyCodeEpression_Click);
			// 
			// buttonAddPauseScript
			// 
			this.buttonAddPauseScript.Location = new System.Drawing.Point(500, 56);
			this.buttonAddPauseScript.Name = "buttonAddPauseScript";
			this.buttonAddPauseScript.Size = new System.Drawing.Size(32, 32);
			this.buttonAddPauseScript.TabIndex = 29;
			this.buttonAddPauseScript.Text = "| |";
			this.buttonAddPauseScript.UseVisualStyleBackColor = true;
			this.buttonAddPauseScript.Click += new System.EventHandler(this.buttonAddPauseScript_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(644, 309);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 13);
			this.label6.TabIndex = 30;
			this.label6.Text = "Keys pressed";
			// 
			// FormMain
			// 
			this.ClientSize = new System.Drawing.Size(874, 583);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.buttonAddPauseScript);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxProcessMacros);
			this.Controls.Add(this.buttonAddKeyCodeEpression);
			this.Controls.Add(this.buttonSavePhrase);
			this.Controls.Add(this.buttonAddPhrase);
			this.Controls.Add(this.buttonRemovePhrase);
			this.Controls.Add(this.groupBoxTriggers);
			this.Controls.Add(this.checkBoxSubstitute);
			this.Controls.Add(this.checkBoxAutotextCaseSensetive);
			this.Controls.Add(this.textBoxAutotext);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxPhraseContent);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listViewPhrases);
			this.Controls.Add(this.textBoxKeysLog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AutoText";
			this.Activated += new System.EventHandler(this.FormMain_Activated);
			this.Deactivate += new System.EventHandler(this.FormMain_Deactivate);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TextBox textBoxKeysLog;
		private ListView listViewPhrases;
		private Label label2;
		private ColumnHeader columnHeaderAutotext;
		private ColumnHeader columnHeaderDescription;
		private Label label3;
		private TextBox textBoxDescription;
		private Label label4;
		private TextBox textBoxPhraseContent;
		private Label label5;
		private TextBox textBoxAutotext;
		private CheckBox checkBoxAutotextCaseSensetive;
		private CheckBox checkBoxSubstitute;
		private GroupBox groupBoxTriggers;
		private Button buttonRemovePhrase;
		private Button buttonAddPhrase;
		private Button buttonSavePhrase;
		private Button buttonAddKeyCodeEpression;
		private ComboBox comboBoxProcessMacros;
		private Label label1;
		private Button button1;
		private Button buttonAddPauseScript;
		private Label label6;
	}
}

