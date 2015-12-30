using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoText
{
	public class KeyCapturedEventArgs : EventArgs
	{
		public string CapturedCharacter { get; private set; }

		public List<string> CapturedKeys { get; private set; }

		public KeyCapturedEventArgs(List<string> capturedKeyCodes)
		{
			CapturedKeys = capturedKeyCodes;
		}

		public KeyCapturedEventArgs(string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
		}

		public KeyCapturedEventArgs(List<string> capturedKeyCodes, string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
			CapturedKeys = capturedKeyCodes;
		}
	}
}