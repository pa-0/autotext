using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;

namespace AutoText.Helpers
{

	public static class AutotextRuleExecution
	{
		public static void ProcessRule(AutotextRuleMatchParameters rule)
		{
			AutotextExpression expression = null;
			try
			{
				expression = new AutotextExpression(rule);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to execute phrase. Please check phrase syntax and fix contradictory phrase settings.",
					"Faled to execute phrase",
					MessageBoxButtons.OK);
			}

			if (expression != null)
			{
				List<AutotextInput> input = expression.GetInput();
				DoInput(input);
			}

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

			foreach (AutotextInput autotextInput in input)
			{
				if (autotextInput.Sleep > 0)
				{
					Thread.Sleep(autotextInput.Sleep);
				}
				else
				{
					INPUT[] inputSim = ConverInput(new List<AutotextInput>(){ autotextInput});
					InputSimulator.SimulateInputSequence(inputSim);
				}
			}

		}
	}
}
