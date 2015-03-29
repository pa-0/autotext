using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoText
{
	public class KeyCapturedEventArgs : EventArgs
	{
		public string CapturedCharacter { get; private set; }

		public Keys[] CapturedKeys { get; private set; }

		public KeyCapturedEventArgs(Keys[] capturedKeyCodes)
		{
			CapturedKeys = capturedKeyCodes;
		}

		public KeyCapturedEventArgs(string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
		}

		public KeyCapturedEventArgs(Keys[] capturedKeyCodes, string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
			CapturedKeys = capturedKeyCodes;
		}
	}
}