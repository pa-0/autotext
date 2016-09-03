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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using AutoText.Utility;

namespace AutoText.Helpers
{
	public class Sender
	{
		static Process process;
		static NamedPipeServerStream namedPipeDataServerStream;
		static NamedPipeServerStream namedPipeCommandsServerStream = new NamedPipeServerStream("autotextCommands");

		public static event EventHandler<EventArgs> DataSent;

		public static void StartSender()
		{
			if (process == null)
			{
				Logger.LogInfo("Starting Sender...");

				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					UseShellExecute = false,
					FileName = "Sender.exe",
					Arguments = Process.GetCurrentProcess().Id.ToString(),
					RedirectStandardOutput = true,
					RedirectStandardInput = true,
				};

				process = new Process
				{
					StartInfo = startInfo
				};

				process.Start();
				Logger.LogInfo("Sender started");
			}
			else
			{
				throw new InvalidOperationException("Sender module is already started");
			}

			Logger.LogInfo("Launching Task for new data pipe connection wait(StartSender method)");
			Task.Factory.StartNew(DataPipeWaitForConnection);
			Logger.LogInfo("Launching Task for new input data pipe connection wait");
			Task.Factory.StartNew(StartReceiveData);
		}

		private static void StartReceiveData()
		{
			Logger.LogInfo("Starting receive data from sender through input pipe...");

			if (!namedPipeCommandsServerStream.IsConnected)
			{
				Logger.LogInfo("Input pipe is not connected. Waiting for connection...");
				namedPipeCommandsServerStream.WaitForConnection();
				Logger.LogInfo("Input pipe successfully connected");
			}

			Logger.LogInfo("Entering loop to read incoming pipe data...");

			while (true)
			{
				byte[] buf = new byte[2000];
				Logger.LogInfo("Reading data from input pipe...");
				int bytesRead = namedPipeCommandsServerStream.Read(buf, 0, buf.Length);
				string msg = Encoding.Unicode.GetString(buf, 0, bytesRead);
				Logger.LogInfo("Data read. Val: " + msg);
				Logger.LogInfo("Closing input data pipe");
				namedPipeCommandsServerStream.Close();
				Logger.LogInfo("Input data pipe closed");

				switch (msg)
				{
					case "Done":
						Logger.LogInfo("Triggering on data sent event");
						OnDataSent();
						break;
				}

				Logger.LogInfo("Creating new input pipe");
				namedPipeCommandsServerStream = new NamedPipeServerStream("autotextCommands");
				Logger.LogInfo("Waiting for connection on input pipe...");
				namedPipeCommandsServerStream.WaitForConnection();
				Logger.LogInfo("Input pipe connected");
			}
		}


		private static void DataPipeWaitForConnection()
		{
			Logger.LogInfo("DataPipeWaitForConnection(): Creating new data pipe..");
			namedPipeDataServerStream = new NamedPipeServerStream("autotextData", PipeDirection.Out, 250);
			Logger.LogInfo("DataPipeWaitForConnection(): New data pipe created");
			Logger.LogInfo("DataPipeWaitForConnection(): Waiting for connection on data pipe...");
			namedPipeDataServerStream.WaitForConnection();
			Logger.LogInfo("DataPipeWaitForConnection(): Data pipe connected");
		}

		public static void Send(string stringToSend)
		{
			if (process == null || process.HasExited)
			{
				throw new InvalidOperationException("Sender module is not started or terminated");
			}

			List<byte> res = new List<byte>();
			//res.Add(255);
			//res.Add(254);
			res.AddRange(Encoding.Unicode.GetBytes(stringToSend));

			//			string dataLength = (res.Count / 2).ToString();
			//			dataLength = dataLength.PadLeft(10,'0');
			//			List<byte> resDataLength = new List<byte>();
			//			//resDataLength.Add(255);
			//			//resDataLength.Add(254);
			//			resDataLength.AddRange(Encoding.Unicode.GetBytes(dataLength));


			//			namedPipeDataServerStream.Write(resDataLength.ToArray(), 0, resDataLength.Count);
			//			namedPipeDataServerStream.Flush();

			Logger.LogInfo("Writing data to data pipe");
			namedPipeDataServerStream.Write(res.ToArray(), 0, res.Count);
			Logger.LogInfo("Data to data pipe written");
			namedPipeDataServerStream.Flush();
			Logger.LogInfo("Closing data pipe");
			namedPipeDataServerStream.Close();
			Logger.LogInfo("Data pipe closed");

			Logger.LogInfo("Launching Task for new data pipe connection wait(Send method)");
			Task.Factory.StartNew(DataPipeWaitForConnection);
		}

		public static void StopSender()
		{
			if (process != null && !process.HasExited)
			{
				process.Kill();
			}
		}

		private static void OnDataSent()
		{
			var handler = DataSent;
			if (handler != null) handler(null, EventArgs.Empty);
		}
	}
}
