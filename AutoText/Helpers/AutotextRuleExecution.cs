using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WindowsInput;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;

namespace AutoText.Helpers
{
	public static class AutotextRuleExecution
	{
		public static void ProcessRule(AutotextRuleConfig rule)
		{
			AutotextExpression expression = new AutotextExpression(rule.Phrase);
			List<AutotextInput> input = expression.GetInput();
			string resStr = input.ToStringConcat();
			DoInput(input);
			{ }
		}

		private static INPUT [] ConverInput(List<AutotextInput> input)
		{
			INPUT[] inputSimulationArr = input.SelectMany(p =>
			{
				List<INPUT> res = new List<INPUT>(500);

				if (p.Type == InputType.UnicodeChar)
				{
					if (p.ActionType == InputActionType.Press)
					{
						res.Add(InputSimulator.GetInput(p.CharToInput, ActionType.KeyDown));
						res.Add(InputSimulator.GetInput(p.CharToInput, ActionType.KeyUp));
					}
					else if (p.ActionType == InputActionType.KeyDown)
					{
						res.Add(InputSimulator.GetInput(p.CharToInput, ActionType.KeyDown));
					}
					else if (p.ActionType == InputActionType.KeyUp)
					{
						res.Add(InputSimulator.GetInput(p.CharToInput, ActionType.KeyUp));
					}
					else
					{
						throw new ArgumentOutOfRangeException("InputActionType");
					}
				}
				else if (p.Type == InputType.KeyCode)
				{
					if (p.ActionType == InputActionType.Press)
					{
						res.Add(InputSimulator.GetInput(p.KeyCodeToInput, ActionType.KeyDown));
						res.Add(InputSimulator.GetInput(p.KeyCodeToInput, ActionType.KeyUp));
					}
					else if (p.ActionType == InputActionType.KeyDown)
					{
						res.Add(InputSimulator.GetInput(p.KeyCodeToInput, ActionType.KeyDown));
					}
					else if (p.ActionType == InputActionType.KeyUp)
					{
						res.Add(InputSimulator.GetInput(p.KeyCodeToInput, ActionType.KeyUp));
					}
					else
					{
						throw new ArgumentOutOfRangeException("InputActionType");
					}
				}
				else
				{
					throw new ArgumentOutOfRangeException("InputActionType");
				}

				return res;
			}).ToArray();

			return inputSimulationArr;
		}

		public static void DoInput(List<AutotextInput> input)
		{
			INPUT[] inputSim = ConverInput(input);
			InputSimulator.SimulateInputSequence(inputSim);
		}
	}
}
