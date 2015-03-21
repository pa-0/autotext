using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WindowsInput;
using AutoText.Helpers.Configuration;

namespace AutoText.Helpers
{
	public static class AutotextRuleExecution
	{
		public static void ProcessRule(AutotextRuleConfig rule)
		{
			AutotextPhrase phrase = new AutotextPhrase(rule.Phrase);
			List<Input> input = phrase.RootExpression.GetInput();
			DoInput(input);

			//string res = string.Concat(input.Select(p => p.CharToInput));

			{ }
		}

		public static void DoInput(List<Input> input)
		{
			foreach (Input inpt in input)
			{
				switch (inpt.ActionType)
				{
					case InputActionType.KeyDown:
					{
						if (inpt.CharToInput != '\0')
						{
							InputSimulator.SimulateKeyDown(inpt.CharToInput);
						}
						else if (inpt.KeyCodeToInput != 0)
						{
							InputSimulator.SimulateKeyDown(inpt.KeyCodeToInput);
						}
						else
						{
							throw new InvalidOperationException("No iput data found");
						}
						break;
					}
					case InputActionType.KeyUp:
					{
						if (inpt.CharToInput != '\0')
						{
							InputSimulator.SimulateKeyUp(inpt.CharToInput);
						}
						else if (inpt.KeyCodeToInput != 0)
						{
							InputSimulator.SimulateKeyUp(inpt.KeyCodeToInput);
						}
						else
						{
							throw new InvalidOperationException("No iput data found");
						}
						break;
					}

					case InputActionType.Press:
					{
						if (inpt.CharToInput != '\0')
						{
							InputSimulator.SimulateKeyPress(inpt.CharToInput);
						}
						else if (inpt.KeyCodeToInput != 0)
						{
							InputSimulator.SimulateKeyPress(inpt.KeyCodeToInput);
						}
						else
						{
							throw new InvalidOperationException("No iput data found");
						}
						break;
					}
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}
