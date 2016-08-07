/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

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
