/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

AutoText is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoText.Constants;

namespace AutoText.Utility
{
	public class Logger
	{
		private static readonly object _syncObject = new object();
		private static string _logFilePath = ConfigConstants.LogFileFullPath;
		private static readonly Mutex _mutex = new Mutex(false, "AutoText/Logging");
		private static readonly BlockingCollection<string> _messageQueue = new BlockingCollection<string>(new ConcurrentQueue<string>());
		private static bool _close;

		static Logger()
		{
			if (!Directory.Exists(ConfigConstants.ApplicationTempDirPath))
			{
				Directory.CreateDirectory(ConfigConstants.ApplicationTempDirPath);
			}

			Task.Factory.StartNew(() =>
			{
				while (true)
				{
					if (_mutex.WaitOne(50))
					{
						string message = _messageQueue.Take();

						int attemptCount = 0;

						while (true)
						{
							try
							{
								File.AppendAllText(_logFilePath, message + "\r\n");
								break;
							}
							catch (Exception ex)//file may be in use by Sender process
							{
								Thread.Sleep(20);
								attemptCount++;
							}

							if (attemptCount > 5)
							{
								throw new InvalidOperationException("Can't write to log file");
							}
						}

						_mutex.ReleaseMutex();
					}

					if (_close)
					{
						return;
					}
				}
			});
		}

		public static void LogInfo(string message)
		{
			Log("Info: " + message);
		}

		public static void LogWarning(string message)
		{
			Log("Warning: " + message);
		}

		public static void LogError(string message)
		{
			Log("Error: " + message);
		}

		private static void Log(string message)
		{
			lock (_syncObject)
			{
				string date = DateTime.Now.ToString("dd.MM.yy HH:mm:ss.fff");
				_messageQueue.Add(date + " (AutoText) " + message);
			}
		}

		public static void Close()
		{
			_close = true;
		}

		public static void ClearLogFile()
		{
			if (File.Exists(ConfigConstants.LogFileFullPath))
			{
				File.WriteAllText(ConfigConstants.LogFileFullPath, "");
			}
		}
	}
}
