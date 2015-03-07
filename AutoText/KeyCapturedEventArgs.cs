using System;
using System.Windows.Forms;

namespace AutoText
{
	public class KeyCapturedEventArgs : EventArgs
	{
		public string CapturedCharacter { get; private set; }
		public Keys CapturedKey { get; private set; }

		public KeyCapturedEventArgs(Keys capturedKey)
		{
			CapturedKey = capturedKey;
		}

		public KeyCapturedEventArgs(string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
		}
	}
}