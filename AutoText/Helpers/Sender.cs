using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoText.Helpers
{
	public class Sender
	{
		static Process process;
		static NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream("autotext");

		public static void StartSender()
		{
			if (process == null)
			{
				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					UseShellExecute = false,
					FileName = "Sender.exe",
					Arguments = Process.GetCurrentProcess().Id.ToString()
				};

				process = new Process
				{
					StartInfo = startInfo
				};

				process.Start();
			}
			else
			{
				throw new InvalidOperationException("Sender module is already started");
			}
		}

		public static void Send(string stringToSend)
		{
			if (process == null || process.HasExited)
			{
				throw new InvalidOperationException("Sender module is not started");
			}

			List<byte> res = new List<byte>();
			res.Add(255);
			res.Add(254);
			res.AddRange(Encoding.Unicode.GetBytes(stringToSend));

			namedPipeServerStream.WaitForConnection();
			namedPipeServerStream.Write(res.ToArray(), 0, res.Count);
			namedPipeServerStream.Flush();
			namedPipeServerStream.Disconnect();
		}

		public static void StopSender()
		{
			if (process != null && !process.HasExited)
			{
				process.Kill();
			}
		}
	}
}
