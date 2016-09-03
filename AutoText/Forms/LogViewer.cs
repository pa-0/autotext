using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AutoText.Constants;
using AutoText.Helpers;

namespace AutoText.Forms
{
	public partial class LogViewer : Form
	{
		public LogViewer()
		{
			InitializeComponent();

			TopMost = true;

			if (!File.Exists(ConfigConstants.LogFileFullPath))
			{
				Directory.CreateDirectory(ConfigConstants.ApplicationTempDirPath);
				File.Create(ConfigConstants.LogFileFullPath);
			}

			textBoxLog.Text = File.ReadAllText(ConfigConstants.LogFileFullPath, Encoding.UTF8);

			FileSystemWatcher logFileWatcher = new FileSystemWatcher();
			logFileWatcher.Path = ConfigConstants.ApplicationTempDirPath;
			logFileWatcher.Filter = ConfigConstants.LogFileName;
			logFileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
			logFileWatcher.Changed += logFileWatcher_Changed;
			logFileWatcher.EnableRaisingEvents = true;
		}

		void logFileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			try
			{
				if (IsHandleCreated)
				{
					Invoke(new Action(() =>
					{
						textBoxLog.Text = File.ReadAllText(ConfigConstants.LogFileFullPath, Encoding.UTF8);

						if (checkBoxScrollToBottom.Checked)
						{
							textBoxLog.SelectionStart = textBoxLog.TextLength;
							textBoxLog.ScrollToCaret();
						}
					}));
				}
			}
			//Ignore if file is busy
			catch (IOException ex){}
			//Ignore if handle is not created(randomly happens on window close)
			catch (InvalidOperationException ex){}
		}

		private void LogViewer_Load(object sender, EventArgs e)
		{

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

		private void LogViewer_Shown(object sender, EventArgs e)
		{
			if (checkBoxScrollToBottom.Checked)
			{
				textBoxLog.SelectionStart = textBoxLog.TextLength;
				textBoxLog.ScrollToCaret();
			}
		}

		private void checkBoxScrollToBottom_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void buttonClearLog_Click(object sender, EventArgs e)
		{
			File.WriteAllText(ConfigConstants.LogFileFullPath, string.Empty);
		}
	}
}
