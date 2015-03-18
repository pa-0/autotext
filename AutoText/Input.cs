using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoText
{
	public enum InputActionType
	{
		KeyDown,
		KeyUp,
		Press
	}

	public enum InputType
	{
		UnicideChar,
		KeyCode
	}

	public class Input
	{
		public InputType Type { get; set; }
		public InputActionType ActionType { get; set; }
		public char CharToInput { get; set; }
		public int KeyCodeValueToInput { get; set; }
	}
}
