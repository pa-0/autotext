using System;
using System.Windows.Forms;

namespace AutoText.Forms
{
	public partial class LogViewer : Form
	{
		public LogViewer()
		{
			InitializeComponent();

			//FileSystemWatcher watcher = new FileSystemWatcher();
			//watcher.Path = @"d:\Downloads\";
			//watcher.Filter = "AutoText.log";
			//watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
			//watcher.Changed += watcher_Changed;
			//watcher.EnableRaisingEvents = true;
		}

		private void LogViewer_Load(object sender, EventArgs e)
		{
			TopMost = true;
		}

		private void checkBoxStayOnTop_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = checkBoxStayOnTop.Checked;
		}

		private void checkBoxWordWrap_CheckedChanged(object sender, EventArgs e)
		{
			textBoxLog.WordWrap = checkBoxWordWrap.Checked;

			if (checkBoxWordWrap.Checked)
			{
				textBoxLog.ScrollBars = ScrollBars.Vertical;
			}
			else
			{
				textBoxLog.ScrollBars = ScrollBars.Both;
			}
		}
	}
}
