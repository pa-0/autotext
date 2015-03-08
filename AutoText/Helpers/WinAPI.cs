using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoText.Helpers
{
	class WinAPI
	{
		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(int i);

		[DllImport("user32.dll")]
		public static extern int ToUnicodeEx(int wVirtKey,
			uint wScanCode,
			byte[] lpKeyState,
			[Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff,
			int cchBuff, uint wFlags, int dwhkl);

		[DllImport("User32.dll")]
		public static extern int GetKeyboardLayout(int idThread);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

	}
}
