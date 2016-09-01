using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoText.Utility
{
	public class Logger
	{
		private static readonly object _syncObject = new object();
		private static string _logFilePath = @"d:\Downloads\AutoText.log";
		private static readonly Mutex _mutex = new Mutex(true, "AutoText");
		private static readonly BlockingCollection<string> _messageQueue = new BlockingCollection<string>(new ConcurrentQueue<string>());
		private static bool _close;

		static Logger()
		{
			Task.Factory.StartNew(() =>
			{
				while (true)
				{
					string message = _messageQueue.Take();
					_mutex.WaitOne(-1);
					File.AppendAllText(_logFilePath, message + "\r\n");
					_mutex.ReleaseMutex();

					if (_close)
					{
						return;
					}
				}
			});
		}

		public static void LogAsync(string message)
		{
			Task.Factory.StartNew(() =>
			{
				lock (_syncObject)
				{
					_messageQueue.Add(message);
				}
			});
		}

		public static void Close()
		{
			_close = true;
		}
	}
}
