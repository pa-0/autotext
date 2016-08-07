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
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using InputType = AutoText.Engine.InputType;

namespace AutoText.Engine
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
						res.Add(InputSimulator.GetInput((char) p.CharToInput, ActionType.KeyDown));
						res.Add(InputSimulator.GetInput((char) p.CharToInput, ActionType.KeyUp));
					}
					else if (p.ActionType == InputActionType.KeyDown)
					{
						res.Add(InputSimulator.GetInput((char) p.CharToInput, ActionType.KeyDown));
					}
					else if (p.ActionType == InputActionType.KeyUp)
					{
						res.Add(InputSimulator.GetInput((char) p.CharToInput, ActionType.KeyUp));
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
						res.Add(InputSimulator.GetInput((Keys) p.KeyCodeToInput, ActionType.KeyDown));
						res.Add(InputSimulator.GetInput((Keys) p.KeyCodeToInput, ActionType.KeyUp));
					}
					else if (p.ActionType == InputActionType.KeyDown)
					{
						res.Add(InputSimulator.GetInput((Keys) p.KeyCodeToInput, ActionType.KeyDown));
					}
					else if (p.ActionType == InputActionType.KeyUp)
					{
						res.Add(InputSimulator.GetInput((Keys) p.KeyCodeToInput, ActionType.KeyUp));
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
