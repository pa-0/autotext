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
		private bool _pauseCapture;

		public void StartCapture()
		{
			_interruptCapture = false;

			_keyCaptureTask = Task.Factory.StartNew(() =>
			{
				int[] keysValues = (int[])Enum.GetValues(typeof (Keys));
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

							keyChar = TextHelper.GetCharsFromKeys(keysValues[i], Control.ModifierKeys.HasFlag(Keys.Shift), Control.ModifierKeys.HasFlag(Keys.Alt),Control.IsKeyLocked(Keys.CapsLock),
								WinAPI.GetKeyboardLayout(WinAPI.GetWindowThreadProcessId(WinAPI.GetForegroundWindow(), IntPtr.Zero)));
						}
					}

					if (capturedKeyCodes.Count > 0)
					{
						OnKeyCaptured(new KeyCapturedEventArgs(capturedKeyCodes.Select(p => (Keys)p).ToArray(), keyChar));
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
