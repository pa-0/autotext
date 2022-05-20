/*This file is part of AutoText.

Copyright © 2022 Alexander Litvinov

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
using System.Text;
using System.Windows.Forms;

namespace AutoText.Helpers
{
	public class TextHelper
	{
		public static string GetCharsFromKeys(int keyCode)
		{
			var buf = new StringBuilder(5);
			var keyboardState = new byte[256];

			if (Control.ModifierKeys.HasFlag(Keys.Shift))
			{
				keyboardState[(int)Keys.ShiftKey] = 0xff;
			}

			if (Control.ModifierKeys.HasFlag(Keys.Alt))
			{
				keyboardState[(int)Keys.ControlKey] = 0xff;
				keyboardState[(int)Keys.Menu] = 0xff;
			}

			if (Control.IsKeyLocked(Keys.CapsLock))
			{
				keyboardState[(int)Keys.CapsLock] = 0xff;
			}

			IntPtr pid;
			int keyboardLayout = WinAPI.GetKeyboardLayout(WinAPI.GetWindowThreadProcessId(WinAPI.GetForegroundWindow(), out pid));

			WinAPI.ToUnicodeEx(keyCode, 0, keyboardState, buf, 5, 0, keyboardLayout);
			return buf.ToString();
		}
	}
}
