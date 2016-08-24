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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;

namespace AutoText.Engine
{

	public static class AutotextRuleExecution
	{
		public static void ProcessRule(AutotextRuleMatchParameters rule)
		{
			try
			{
				AutotextExpression expression = new AutotextExpression(rule);
				List<AutotextInput> input = expression.GetInput();
				DoInput(input);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to execute phrase. If phrase contains macros please check macros syntax and fix contradictory phrase settings.",
					"AutoText",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}


		private static string ConverInput(List<AutotextInput> input)
		{
			List<char> specialChars = new List<char>() { '!', '+', '^', '#', '{', '}' };
			StringBuilder res = new StringBuilder();

			for (int i = 0; i < input.Count; i++)
			{
				AutotextInput autotextInput = input[i];

				if (autotextInput.Type == InputType.UnicodeChar)
				{
					if (specialChars.Contains(autotextInput.CharToInput))
					{
						res.Append('{');
						res.Append(autotextInput.CharToInput);
						res.Append('}');
					}
					else
					{
						string template = "{{{0}{1}}}";

						switch (autotextInput.ActionType)
						{
							case InputActionType.KeyDown:
								res.Append(string.Format(template, autotextInput.CharToInput, " down"));
								break;
							case InputActionType.KeyUp:
								res.Append(string.Format(template, autotextInput.CharToInput, " up"));
								break;
							case InputActionType.Press:
								res.Append(autotextInput.CharToInput);
								break;
							default:
								throw new InvalidOperationException("Action is not recognized");
						}
					}
				}
				else if (autotextInput.Type == InputType.KeyCode)
				{
					string senderKeyName = ConfigHelper.GetSenderKeyByNativeKey(autotextInput.KeyCodeToInput.ToString());

					string template = "{{{0}{1}}}";

					switch (autotextInput.ActionType)
					{
						case InputActionType.KeyDown:
							res.Append(string.Format(template, senderKeyName, " down"));
							break;
						case InputActionType.KeyUp:
							res.Append(string.Format(template, senderKeyName, " up"));
							break;
						case InputActionType.Press:
							res.Append(string.Format(template, senderKeyName, ""));
							break;
						default:
							throw new InvalidOperationException("Action is not recognized");
					}
				}
				else
				{
					throw new InvalidOperationException("Can't recognize input type");
				}
			}

			return res.Replace("\r\n", "\r").ToString();
		}

		public static void DoInput(List<AutotextInput> input)
		{
			string inpt = ConverInput(input);
			Sender.Send(inpt);
		}
	}
}
