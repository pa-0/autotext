using System;
using System.Windows.Forms;

namespace AutoText
{
	public class KeyCapturedEventArgs : EventArgs
	{
		public string CapturedCharacter { get; private set; }

		public Keys CapturedKey
		{
			get { return (Keys)KeyCode; }
		}

		public int KeyCode { get; private set; }

		public KeyCapturedEventArgs(int capturedKeyCode)
		{
			KeyCode = capturedKeyCode;
		}

		public KeyCapturedEventArgs(string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
		}

		public KeyCapturedEventArgs(int capturedKeyCode, string capturedCharacter)
		{
			CapturedCharacter = capturedCharacter;
			KeyCode = capturedKeyCode;
		}
	}
}