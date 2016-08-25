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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;
using AutoText.Model.Configuration;

namespace AutoText.Engine
{
	public class KeyLogger
	{
		public KeyLogger()
		{
			KeysScanInterval = 20;
		}

		public KeyLogger(int keysScanInterval)
		{
			KeysScanInterval = keysScanInterval;
		}

		public int KeysScanInterval { get; set; }
		private Task _keyCaptureTask;
		public event EventHandler<KeyCapturedEventArgs> KeyCaptured;
		public event EventHandler<EventArgs> CaptureStopped;
		private bool _interruptCapture;
		private bool _pauseCapture;

		public void StartCapture()
		{
			_interruptCapture = false;

			_keyCaptureTask = Task.Factory.StartNew(() =>
			{
				int[] keysValues = (int[])Enum.GetValues(typeof(Keys));
				List<int> capturedKeyCodes = new List<int>(10);
				string keyChar = string.Empty;

				while (true)
				{
					if (_pauseCapture)
					{
						Thread.Sleep(10);
						continue;
					}

					for (uint i = 0; i < keysValues.Length; i++)
					{
						short keyState = WinAPI.GetAsyncKeyState(keysValues[i]);

						if (keyState == 1 || keyState == -32767)
						{
							capturedKeyCodes.Add(keysValues[i]);

							IntPtr pid;
							keyChar = TextHelper.GetCharsFromKeys(keysValues[i]);
						}
					}


					if (capturedKeyCodes.Count > 0)
					{
						List<KeycodeConfig> kcConfig = ConfigHelper.GetKeycodesConfiguration().Keycodes;
						List<string> capturedKecodes = new List<string>(10);

						foreach (int code in capturedKeyCodes)
						{
							foreach (KeycodeConfig config in kcConfig)
							{
								if (config.VirtualKeyCode == code)
								{
									capturedKecodes.Add(config.Names.First().Value);
								}
							}
						}


						OnKeyCaptured(new KeyCapturedEventArgs(capturedKecodes, keyChar));
						keyChar = string.Empty;
						capturedKeyCodes.Clear();
					}


					if (_interruptCapture)
					{
						return;
					}
					else
					{
						Thread.Sleep(KeysScanInterval);
					}

				}
			});
		}

		public void StopCapture()
		{
			_interruptCapture = true;

			Task.Factory.StartNew(() =>
			{
				_keyCaptureTask.Wait();
			})
			.ContinueWith(prewTask =>
			{
				OnCaptureStopped();
			});
		}


		public void PauseCapture()
		{
			_pauseCapture = true;
		}

		public void ResumeCapture()
		{
			_pauseCapture = false;
		}



		protected virtual void OnKeyCaptured(KeyCapturedEventArgs e)
		{
			var handler = KeyCaptured;
			if (handler != null) handler(this, e);
		}

		protected virtual void OnCaptureStopped()
		{
			var handler = CaptureStopped;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}
