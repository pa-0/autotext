using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoText.Engine
{
	public struct AutotextInput
	{
		public InputType Type { get; set; }
		public InputActionType ActionType { get; set; }
		public char CharToInput { get; set; }
		public Keys KeyCodeToInput { get; set; }
		public int  Sleep { get; set; }


		public AutotextInput(InputType type, InputActionType actionType, char charToInput) : this()
		{
			Type = type;
			ActionType = actionType;
			CharToInput = charToInput;
		}

		public AutotextInput(InputType type, InputActionType actionType, Keys keyCodeToInput) : this()
		{
			Type = type;
			ActionType = actionType;
			KeyCodeToInput = keyCodeToInput;
		}

		public static List<AutotextInput> FromString(string str)
		{
			return FromString(new StringBuilder(str));
		}

		public static List<AutotextInput> FromString(StringBuilder stringBuilder)
		{
			List<AutotextInput> res = new List<AutotextInput>(1000);

			for (int i = 0; i < stringBuilder.Length; i++)
			{
				res.Add(new AutotextInput() { ActionType = InputActionType.Press, CharToInput = stringBuilder[i],Type = InputType.UnicodeChar});
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
