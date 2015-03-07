using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoText.Helpers;

namespace AutoText
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

		private string GetCharsFromKeys(uint keys, bool shift, bool altGr, int keyboardLayout)
		{
			var buf = new StringBuilder(5);
			var keyboardState = new byte[256];

			if (shift)
			{
				keyboardState[(int)Keys.ShiftKey] = 0xff;
			}

			if (altGr)
			{
				keyboardState[(int)Keys.ControlKey] = 0xff;
				keyboardState[(int)Keys.Menu] = 0xff;
			}

			WinAPI.ToUnicodeEx(keys, 0, keyboardState, buf, 5, 0, keyboardLayout);
			return buf.ToString();
		}

		public void StartCapture()
		{
			_keyCaptureTask = Task.Factory.StartNew(() =>
			{
				string[] names = Enum.GetNames(typeof(Keys));
				int valuesCount = names.Select(name => (int)Enum.Parse(typeof(Keys), name)).Count();

				while (true)
				{
					for (uint i = 0; i < valuesCount; i++)
					{
						short keyState = WinAPI.GetAsyncKeyState((int)i);

						if (keyState == 1 || keyState == -32767)
						{
							string keyChar = GetCharsFromKeys(i, Control.ModifierKeys.HasFlag(Keys.Shift), false,
								WinAPI.GetKeyboardLayout(WinAPI.GetWindowThreadProcessId(WinAPI.GetForegroundWindow(), IntPtr.Zero)));

							OnKeyCaptured(new KeyCapturedEventArgs((Keys)i, keyChar));

							/*
							if (((Keys)i) == Keys.F)
							{
								SendKeys.SendWait("Hello{LEFT}{LEFT}{LEFT}{LEFT}{LEFT}");
							}
							*/
							break;
						}
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
