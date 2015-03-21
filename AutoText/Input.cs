using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoText
{
	public struct Input
	{
		public InputType Type { get; set; }
		public InputActionType ActionType { get; set; }
		public char CharToInput { get; set; }
		public Keys KeyCodeToInput { get; set; }


		public static List<Input> FromString(string str)
		{
			return FromString(new StringBuilder(str));
		}

		public static List<Input> FromString(StringBuilder stringBuilder)
		{
			List<Input> res = new List<Input>(1000);

			for (int i = 0; i < stringBuilder.Length; i++)
			{
				res.Add(new Input() { ActionType = InputActionType.Press, CharToInput = stringBuilder[i] });
			}

			return res;
		}
	}

	public enum InputActionType
	{
		KeyDown,
		KeyUp,
		Press
	}

	public enum InputType
	{
		UnicodeChar,
		KeyCode
	}
}
