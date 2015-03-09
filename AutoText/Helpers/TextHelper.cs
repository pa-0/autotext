using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoText.Helpers
{
	public class TextHelper
	{
		public static string GetCharsFromKeys(int keyCode, bool shift, bool altGr, bool capsLocked, int keyboardLayout)
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

			if (capsLocked)
			{
				keyboardState[(int)Keys.CapsLock] = 0xff;
			}

			WinAPI.ToUnicodeEx(keyCode, 0, keyboardState, buf, 5, 0, keyboardLayout);
			return buf.ToString();
		}

	}
}
