using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoText
{
	public class KeyCapturedEventArgs : EventArgs
	{
		public string CapturedCharacter { get; private set; }

		public Keys[] CapturedKeys
		{
			get { return CapturedKeyCodes.Select( p => (Keys)p).ToArray(); }
		}

		public int[] CapturedKeyCodes { get; private set; }

		public KeyCapturedEventArgs(int[] capturedKeyCodes)
		{
			CapturedKeyCodes = capturedKeyCodes;
		}

		public KeyCapturedEventArgs(string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
		}

		public KeyCapturedEventArgs(int[] capturedKeyCodes, string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
			CapturedKeyCodes = capturedKeyCodes;
		}
	}
}