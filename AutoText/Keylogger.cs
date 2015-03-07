using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoText
{
	public class Keylogger
	{
		public Keylogger()
		{
			KeysScanInterval = 20;
		}

		public Keylogger(int keysScanInterval)
		{
			KeysScanInterval = keysScanInterval;
		}

		public int KeysScanInterval { get; set; }

		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(Int32 i);

		[DllImport("user32.dll")]
		static extern int ToUnicodeEx(uint wVirtKey, 
			uint wScanCode, 
			byte[] lpKeyState, 
			[Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, 
			int cchBuff, uint wFlags, int dwhkl);

		[DllImport("User32.dll")]
		public static extern int GetKeyboardLayout(int idThread);

		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		private Task _keyCapture;
		public event EventHandler<KeyCapturedEventArgs> KeyCaptured;
		List<short> keyStates = new List<short>();

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

			ToUnicodeEx(keys, 0, keyboardState, buf, 5, 0, keyboardLayout);
			return buf.ToString();
		}

		public void StartCapture()
		{
			_keyCapture = Task.Factory.StartNew(() =>
			{
				string[] names = Enum.GetNames(typeof(Keys));
				int valuesCount = names.Select(name => (int)Enum.Parse(typeof(Keys), name)).Count();

				while (true)
				{
					for (uint i = 0; i < valuesCount; i++)
					{
						short keyState = GetAsyncKeyState((int)i);

						/*
						if (!keyStates.Contains(keyState))
						{
							keyStates.Add(keyState);
						}
						*/
						if (keyState == 1 || keyState == -32767)
						{
							string keyChar = GetCharsFromKeys(i, Control.ModifierKeys.HasFlag(Keys.Shift), false,
								GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(),IntPtr.Zero)));

							//OnKeyCaptured(new KeyCapturedEventArgs((Keys)i));
							OnKeyCaptured(new KeyCapturedEventArgs(keyChar));

							/*
							if (((Keys)i) == Keys.F)
							{
								SendKeys.SendWait("Hello{LEFT}{LEFT}{LEFT}{LEFT}{LEFT}");
							}
							*/
							break;
						}
					}

					Thread.Sleep(KeysScanInterval);
				}
			});
		}

		protected virtual void OnKeyCaptured(KeyCapturedEventArgs e)
		{
			var handler = KeyCaptured;
			if (handler != null) handler(this, e);
		}
	}
}
