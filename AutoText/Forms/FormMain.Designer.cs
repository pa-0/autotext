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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxPhraseContent = new System.Windows.Forms.TextBox();
			this.contextMenuStripPhraseContentEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.macrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keyPressMacrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keyComboMacrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseMacrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dateAndTimeMacrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.debugWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.dataGridViewPhrases = new System.Windows.Forms.DataGridView();
			this.columnHeaderAutotext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderDescripton = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.AutotextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStripPhraseContentEdit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhrases)).BeginInit();
			this.SuspendLayout();
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
			this.label3.Size = new System.Drawing.Size(112, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Phrase description";
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
			this.label4.Size = new System.Drawing.Size(251, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Phrase content(right-click to insert macros)";
			// 
			// textBoxPhraseContent
			// 
			this.textBoxPhraseContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPhraseContent.ContextMenuStrip = this.contextMenuStripPhraseContentEdit;
			this.textBoxPhraseContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxPhraseContent.Location = new System.Drawing.Point(328, 91);
			this.textBoxPhraseContent.Multiline = true;
			this.textBoxPhraseContent.Name = "textBoxPhraseContent";
			this.textBoxPhraseContent.Size = new System.Drawing.Size(535, 186);
			this.textBoxPhraseContent.TabIndex = 10;
			this.textBoxPhraseContent.TextChanged += new System.EventHandler(this.textBoxPhraseContent_TextChanged);
			this.textBoxPhraseContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPhraseContent_KeyDown);
			// 
			// contextMenuStripPhraseContentEdit
			// 
			this.contextMenuStripPhraseContentEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.macrosToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolsToolStripMenuItem});
			this.contextMenuStripPhraseContentEdit.Name = "contextMenuStripPhraseContentEdit";
			this.contextMenuStripPhraseContentEdit.Size = new System.Drawing.Size(123, 192);
			// 
			// macrosToolStripMenuItem
			// 
			this.macrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyPressMacrosToolStripMenuItem,
            this.keyComboMacrosToolStripMenuItem,
            this.pauseMacrosToolStripMenuItem,
            this.dateAndTimeMacrosToolStripMenuItem});
			this.macrosToolStripMenuItem.Name = "macrosToolStripMenuItem";
			this.macrosToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.macrosToolStripMenuItem.Text = "Macros";
			// 
			// keyPressMacrosToolStripMenuItem
			// 
			this.keyPressMacrosToolStripMenuItem.Name = "keyPressMacrosToolStripMenuItem";
			this.keyPressMacrosToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.keyPressMacrosToolStripMenuItem.Text = "Key Action Macros";
			this.keyPressMacrosToolStripMenuItem.Click += new System.EventHandler(this.keyActionMacrosToolStripMenuItem_Click);
			// 
			// keyComboMacrosToolStripMenuItem
			// 
			this.keyComboMacrosToolStripMenuItem.Name = "keyComboMacrosToolStripMenuItem";
			this.keyComboMacrosToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.keyComboMacrosToolStripMenuItem.Text = "Key Combo Macros";
			this.keyComboMacrosToolStripMenuItem.Click += new System.EventHandler(this.keyComboMacrosToolStripMenuItem_Click);
			// 
			// pauseMacrosToolStripMenuItem
			// 
			this.pauseMacrosToolStripMenuItem.Name = "pauseMacrosToolStripMenuItem";
			this.pauseMacrosToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.pauseMacrosToolStripMenuItem.Text = "Pause Macros";
			this.pauseMacrosToolStripMenuItem.Click += new System.EventHandler(this.pauseMacrosToolStripMenuItem_Click);
			// 
			// dateAndTimeMacrosToolStripMenuItem
			// 
			this.dateAndTimeMacrosToolStripMenuItem.Name = "dateAndTimeMacrosToolStripMenuItem";
			this.dateAndTimeMacrosToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.dateAndTimeMacrosToolStripMenuItem.Text = "Date And Time Macros";
			this.dateAndTimeMacrosToolStripMenuItem.Click += new System.EventHandler(this.dateAndTimeMacrosToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugWindowToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// debugWindowToolStripMenuItem
			// 
			this.debugWindowToolStripMenuItem.Name = "debugWindowToolStripMenuItem";
			this.debugWindowToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.debugWindowToolStripMenuItem.Text = "Debug Window";
			this.debugWindowToolStripMenuItem.Click += new System.EventHandler(this.debugWindowToolStripMenuItem_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(328, 286);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Phrase autotext";
			// 
			// textBoxAutotext
			// 
			this.textBoxAutotext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAutotext.Location = new System.Drawing.Point(424, 283);
			this.textBoxAutotext.Name = "textBoxAutotext";
			this.textBoxAutotext.Size = new System.Drawing.Size(214, 20);
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
			this.groupBoxTriggers.Text = "Phrase triggers";
			// 
			// buttonRemovePhrase
			// 
			this.buttonRemovePhrase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemovePhrase.Location = new System.Drawing.Point(247, 552);
			this.buttonRemovePhrase.Name = "buttonRemovePhrase";
			this.buttonRemovePhrase.Size = new System.Drawing.Size(75, 23);
			this.buttonRemovePhrase.TabIndex = 22;
			this.buttonRemovePhrase.Text = "Delete";
			this.buttonRemovePhrase.UseVisualStyleBackColor = true;
			this.buttonRemovePhrase.Click += new System.EventHandler(this.buttonRemovePhrase_Click);
			// 
			// buttonAddPhrase
			// 
			this.buttonAddPhrase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddPhrase.Location = new System.Drawing.Point(166, 552);
			this.buttonAddPhrase.Name = "buttonAddPhrase";
			this.buttonAddPhrase.Size = new System.Drawing.Size(75, 23);
			this.buttonAddPhrase.TabIndex = 23;
			this.buttonAddPhrase.Text = "Add new";
			this.buttonAddPhrase.UseVisualStyleBackColor = true;
			this.buttonAddPhrase.Click += new System.EventHandler(this.buttonAddPhrase_Click);
			// 
			// buttonSavePhrase
			// 
			this.buttonSavePhrase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSavePhrase.Location = new System.Drawing.Point(787, 552);
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
			this.comboBoxProcessMacros.Location = new System.Drawing.Point(741, 64);
			this.comboBoxProcessMacros.Name = "comboBoxProcessMacros";
			this.comboBoxProcessMacros.Size = new System.Drawing.Size(121, 21);
			this.comboBoxProcessMacros.TabIndex = 26;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(611, 67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 13);
			this.label1.TabIndex = 27;
			this.label1.Text = "Phrase macros mode";
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "AutoText";
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
			// 
			// dataGridViewPhrases
			// 
			this.dataGridViewPhrases.AllowUserToAddRows = false;
			this.dataGridViewPhrases.AllowUserToDeleteRows = false;
			this.dataGridViewPhrases.AllowUserToResizeRows = false;
			this.dataGridViewPhrases.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridViewPhrases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPhrases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AutotextColumn,
            this.ColumnDescription});
			this.dataGridViewPhrases.Location = new System.Drawing.Point(12, 29);
			this.dataGridViewPhrases.MultiSelect = false;
			this.dataGridViewPhrases.Name = "dataGridViewPhrases";
			this.dataGridViewPhrases.ReadOnly = true;
			this.dataGridViewPhrases.RowHeadersVisible = false;
			this.dataGridViewPhrases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewPhrases.Size = new System.Drawing.Size(307, 517);
			this.dataGridViewPhrases.TabIndex = 28;
			this.dataGridViewPhrases.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewPhrases_CellFormatting);
			this.dataGridViewPhrases.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewPhrases_RowValidating);
			this.dataGridViewPhrases.SelectionChanged += new System.EventHandler(this.dataGridViewPhrases_SelectionChanged);
			// 
			// AutotextColumn
			// 
			this.AutotextColumn.DataPropertyName = "Abbreviation.AbbreviationText";
			this.AutotextColumn.HeaderText = "Autotext";
			this.AutotextColumn.Name = "AutotextColumn";
			this.AutotextColumn.ReadOnly = true;
			this.AutotextColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.AutotextColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColumnDescription
			// 
			this.ColumnDescription.DataPropertyName = "Description";
			this.ColumnDescription.HeaderText = "Description";
			this.ColumnDescription.Name = "ColumnDescription";
			this.ColumnDescription.ReadOnly = true;
			this.ColumnDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnDescription.Width = 203;
			// 
			// FormMain
			// 
			this.ClientSize = new System.Drawing.Size(874, 583);
			this.Controls.Add(this.dataGridViewPhrases);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxProcessMacros);
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
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AutoText";
			this.Activated += new System.EventHandler(this.FormMain_Activated);
			this.Deactivate += new System.EventHandler(this.FormMain_Deactivate);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.contextMenuStripPhraseContentEdit.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhrases)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label label2;
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
		private ComboBox comboBoxProcessMacros;
		private Label label1;
		private NotifyIcon notifyIcon;
		private ContextMenuStrip contextMenuStripPhraseContentEdit;
		private ToolStripMenuItem macrosToolStripMenuItem;
		private ToolStripMenuItem copyToolStripMenuItem;
		private ToolStripMenuItem pasteToolStripMenuItem;
		private ToolStripMenuItem cutToolStripMenuItem;
		private ToolStripMenuItem selectAllToolStripMenuItem;
		private ToolStripMenuItem deleteToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem keyPressMacrosToolStripMenuItem;
		private ToolStripMenuItem keyComboMacrosToolStripMenuItem;
		private ToolStripMenuItem pauseMacrosToolStripMenuItem;
		private ToolStripMenuItem dateAndTimeMacrosToolStripMenuItem;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private ToolStripMenuItem debugWindowToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem undoToolStripMenuItem;
		private ColumnHeader columnHeaderAutotext;
		private ColumnHeader columnHeaderDescripton;
		private DataGridView dataGridViewPhrases;
		private DataGridViewTextBoxColumn AutotextColumn;
		private DataGridViewTextBoxColumn ColumnDescription;
	}
}

