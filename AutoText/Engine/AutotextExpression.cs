/*This file is part of AutoText.

Copyright © 2022 Alexander Litvinov

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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;
using AutoText.Model.Configuration;
using MoreLinq;

namespace AutoText.Engine
{
	public class AutotextExpression
	{
		private const string OpenCurlyBraceEscapeSeq = "{{}";
		private const string ClosingCurlyBraceEscapeSeq = "{}}";
		private const string OpenSquareBraceEscapeSeq = "{[}";
		private const string ClosingSquareBraceEscapeSeq = "{]}";
		private static readonly Regex _escapedCurlyBracketsRegex = new Regex(@"{{}|{}}|{\[}|{\]}", RegexOptions.Compiled);

		public string ExpressionText { get; private set; }
		public int RelativeStartIndex { get; private set; }
		public int Length { get; private set; }
		public List<AutotextExpression> NestedExpressions { get; private set; }
		private List<int> EscapedBracesIndeces { get; set; }
		public string ExpressionName { get; private set; }
		public List<AutotextExpressionParameter> Parameters { get; private set; }
		public AutotextExpression ParentExpression { get; private set; }

		public AutotextExpression(AutotextRuleMatchParameters autotextRuleMatchParams)
		{
			if (autotextRuleMatchParams == null) throw new ArgumentNullException("autotextRuleMatchParams");
			string abbrRemoveText = "";

			if (autotextRuleMatchParams.AutotextRuleConfiguration.RemoveAbbr)
			{
				for (int i = 0; i < autotextRuleMatchParams.AutotextRuleConfiguration.Abbreviation.AbbreviationText.Length; i++)
				{
					abbrRemoveText += "{k [Back]}";
				}

				if (autotextRuleMatchParams.MatchTrigger.TriggerType == AutotextRuleTriggerType.OneOfChars)
				{
					abbrRemoveText += "{k [Back]}";
				}
				else if (autotextRuleMatchParams.MatchTrigger.TriggerType == AutotextRuleTriggerType.Key)
				{
					if (autotextRuleMatchParams.MatchTrigger.Value != "Cancel" && !string.IsNullOrEmpty(TextHelper.GetCharsFromKeys((int)Enum.Parse(typeof(Keys), autotextRuleMatchParams.MatchTrigger.Value))))
					{
						abbrRemoveText += "{k [Back]}";
					}
				}
				else if (autotextRuleMatchParams.MatchTrigger.TriggerType == AutotextRuleTriggerType.String)
				{
					autotextRuleMatchParams.MatchTrigger.Value.ForEach(p => abbrRemoveText += "{k [Back]}");
				}
				else
				{
					throw new InvalidOperationException("Can't recognize trigger type");
				}
			}

			string phraseText = autotextRuleMatchParams.AutotextRuleConfiguration.Phrase;

			if (autotextRuleMatchParams.AutotextRuleConfiguration.Macros.Mode == MacrosMode.Skip)
			{
				phraseText = phraseText.EscapeSpecialExpressionChars();
			}

			ExpressionText = string.Format("{{s text[{0}] count[1]}}", abbrRemoveText + phraseText);
			RelativeStartIndex = 0;
			Length = ExpressionText.Length;
			EscapedBracesIndeces = new List<int>(100);
			NestedExpressions = new List<AutotextExpression>(100);
			Parameters = new List<AutotextExpressionParameter>(20);

			string exprTextEscBrackets = BuildEscapedBracesList(ExpressionText);
			ParseExpression(exprTextEscBrackets);
		}


		private AutotextExpression(string expressionText, int startIndex, int length, List<int> escapedBracesIndeces, AutotextExpression parentExpression)
		{
			ParentExpression = parentExpression;
			ExpressionText = expressionText;
			RelativeStartIndex = startIndex;
			Length = length;
			NestedExpressions = new List<AutotextExpression>(100);
			Parameters = new List<AutotextExpressionParameter>(20);
			EscapedBracesIndeces = escapedBracesIndeces;
			ParseExpression(ExpressionText);
		}

		private int GetAbsoluteStartIndex()
		{
			int index = RelativeStartIndex;

			if (ParentExpression != null)
			{
				index += ParentExpression.GetAbsoluteStartIndex();
			}

			return index;
		}

		private string BuildEscapedBracesList(string expressionText)
		{
			MatchCollection matches = _escapedCurlyBracketsRegex.Matches(expressionText);
			string[] splitted = _escapedCurlyBracketsRegex.Split(expressionText);
			StringBuilder resStr = new StringBuilder(1000);

			Stack<string> splittedStr = new Stack<string>(splitted.Reverse());
			Stack<string> braces = new Stack<string>(matches.Cast<Match>().Select(p => p.Value).Reverse());

			while (splittedStr.Count > 0)
			{
				resStr.Append(splittedStr.Pop());

				if (braces.Count > 0)
				{
					string escapedBrace = braces.Pop();

					if (escapedBrace == OpenCurlyBraceEscapeSeq)
					{
						resStr.Append("{");
					}
					else if (escapedBrace == ClosingCurlyBraceEscapeSeq)
					{
						resStr.Append("}");
					}
					else if (escapedBrace == OpenSquareBraceEscapeSeq)
					{
						resStr.Append("[");
					}
					else if (escapedBrace == ClosingSquareBraceEscapeSeq)
					{
						resStr.Append("]");
					}
					else
					{
						throw new MacrosParseException("Can't recognize bracket to escape in given macros");
					}

					EscapedBracesIndeces.Add(resStr.Length - 1);
				}
			}

			if (resStr.Length > 0)
			{
				return resStr.ToString();
			}
			else
			{
				return expressionText;
			}
		}


		private void ParseExpression(string expressionText)
		{
			int absStartIndex = GetAbsoluteStartIndex();


			#region Parameters parsing

			StringBuilder sbMacrosName = new StringBuilder();

			for (int i = 1; char.IsLetter(expressionText[i]); i++)
			{
				sbMacrosName.Append(expressionText[i]);
			}

			ExpressionName = sbMacrosName.ToString();

			int expressionParameterOpenBraceCounter = 0;
			int expressionParameterClosingBraceCounter = 0;
			int parameterStartIndex = -1;
			int parameterEndIndex = 0;


			for (int i = 1; i < expressionText.Length - 1; i++)
			{
				if (expressionText[i] == '[' && !EscapedBracesIndeces.Contains(absStartIndex + i))
				{
					expressionParameterOpenBraceCounter++;

					if (parameterStartIndex == -1)
					{
						parameterStartIndex = i;
					}
				}

				if (expressionText[i] == ']' && !EscapedBracesIndeces.Contains(absStartIndex + i))
				{
					expressionParameterClosingBraceCounter++;
					parameterEndIndex = i;
				}

				if (expressionParameterOpenBraceCounter != 0 && expressionParameterClosingBraceCounter != 0 && expressionParameterOpenBraceCounter == expressionParameterClosingBraceCounter)
				{
					int parameterLength = (parameterEndIndex + 1) - parameterStartIndex;

					StringBuilder sbParameterName = new StringBuilder();

					for (int j = parameterStartIndex - 1; char.IsLetter(expressionText[j]); j--)
					{
						sbParameterName.Append(expressionText[j]);
					}

					Parameters.Add(new AutotextExpressionParameter(
						new string(sbParameterName.ToString().Reverse().ToArray()),
						expressionText.Substring(parameterStartIndex + 1, parameterLength - 2),
						parameterStartIndex + 1,
						parameterLength - 2));

					parameterStartIndex = -1;
					parameterEndIndex = 0;
					expressionParameterOpenBraceCounter = 0;
					expressionParameterClosingBraceCounter = 0;
				}
			}

			if (expressionParameterOpenBraceCounter != expressionParameterClosingBraceCounter)
			{
				if (expressionParameterClosingBraceCounter > 0)
				{
					throw new InvalidOperationException("Parameter open bracket not found in given '" + ExpressionName + "' macros");
				}

				if (expressionParameterOpenBraceCounter > 0)
				{
					throw new InvalidOperationException("Parameter closing bracket not found in given '" + ExpressionName + "' macros");
				}
			}

			#endregion


			#region Nested expressions parsing

			int openBraceCounter = 0;
			int closingBraceCounter = 0;
			int nestedExpressionStartIndex = -1;
			int exprEndIndex = 0;


			for (int i = 1; i < expressionText.Length - 1; i++)
			{
				if (expressionText[i] == '{' && !EscapedBracesIndeces.Contains(absStartIndex + i))
				{
					openBraceCounter++;

					if (nestedExpressionStartIndex == -1)
					{
						nestedExpressionStartIndex = i;
					}
				}

				if (expressionText[i] == '}' && !EscapedBracesIndeces.Contains(absStartIndex + i))
				{
					closingBraceCounter++;
					exprEndIndex = i;
				}

				if (openBraceCounter != 0 && closingBraceCounter != 0 && openBraceCounter == closingBraceCounter)
				{
					int nestedExpressionLength = (exprEndIndex + 1) - nestedExpressionStartIndex;
					AutotextExpression expressionToAdd =
						new AutotextExpression(expressionText.Substring(nestedExpressionStartIndex, nestedExpressionLength),
							nestedExpressionStartIndex,
							nestedExpressionLength,
							EscapedBracesIndeces,
							this);

					NestedExpressions.Add(expressionToAdd);
					nestedExpressionStartIndex = -1;
					exprEndIndex = 0;
					openBraceCounter = 0;
					closingBraceCounter = 0;
				}
			}

			if (openBraceCounter != closingBraceCounter)
			{
				if (closingBraceCounter > 0)
				{
					throw new InvalidOperationException("Open bracket not found in given '" + ExpressionName + "' macros");
				}

				if (openBraceCounter > 0)
				{
					throw new InvalidOperationException("Closing bracket not found in given '" + ExpressionName + "' macros");
				}
			}

			#endregion
		}

		public List<AutotextInput> GetInput()
		{
			//Generate nested expressions result
			List<List<AutotextInput>> nestedExpressionsInput = NestedExpressions.Select(t => t.GetInput()).ToList();

			Dictionary<string, List<AutotextInput>> parameters = new Dictionary<string, List<AutotextInput>>(20);

			//Replace expression definitions to expression evaluation results in the parameters before current expression evaluation
			for (int i = 0; i < Parameters.Count; i++)
			{
				AutotextExpressionParameter param = Parameters[i];
				List<AutotextInput> paramInputs = AutotextInput.FromString(param.Value);

				for (int j = 0; j < NestedExpressions.Count; j++)
				{
					if (NestedExpressions[j].RelativeStartIndex >= param.RelativeStartIndex
						&& (NestedExpressions[j].RelativeStartIndex + NestedExpressions[j].Length) <= (param.RelativeStartIndex + param.Length))
					{
						int lengthDiff = NestedExpressions[j].Length - nestedExpressionsInput[j].Count;
						paramInputs.RemoveRange(NestedExpressions[j].RelativeStartIndex - param.RelativeStartIndex, NestedExpressions[j].Length);
						paramInputs.InsertRange(NestedExpressions[j].RelativeStartIndex - param.RelativeStartIndex, nestedExpressionsInput[j]);
						param.Length -= lengthDiff;

						//Correct subsequent parameter start index
						for (int k = i + 1; k < Parameters.Count; k++)
						{
							Parameters[k].RelativeStartIndex -= lengthDiff;
						}

						//Correct subsequent expressions start index
						for (int k = j + 1; k < NestedExpressions.Count; k++)
						{
							NestedExpressions[k].RelativeStartIndex -= lengthDiff;
						}
					}
				}

				parameters.Add(string.IsNullOrEmpty(param.Name) ? (i + 1).ToString() : param.Name, paramInputs);
			}

			List<AutotextInput> res = Evaluate(ExpressionName, parameters);
			return res;
		}

		private static List<AutotextInput> Evaluate(string expressionName, Dictionary<string, List<AutotextInput>> expressionParameters)
		{
			switch (expressionName.ToLower())
			{
				case "s":
					{
						string count;

						if (expressionParameters.ContainsKey("2"))
						{
							count = expressionParameters["2"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("count"))
						{
							count = expressionParameters["count"].ConcatToString();
						}
						else
						{
							count = "1";
						}

						int repeatCount;

						try
						{
							repeatCount = Int32.Parse(count);
						}
						catch (Exception ex)
						{
							throw new MacrosEvaluationException("Failed to parse 'count' param for given 's' macros("+
							ex.Message +")", ex);
						}

						List<AutotextInput> res;

						if (expressionParameters.ContainsKey("1"))
						{
							res = new List<AutotextInput>(repeatCount * expressionParameters["1"].Count);

							for (int i = 0; i < repeatCount; i++)
							{
								res.AddRange(expressionParameters["1"]);
							}

						}
						else if (expressionParameters.ContainsKey("text"))
						{
							res = new List<AutotextInput>(repeatCount * expressionParameters["text"].Count);

							for (int i = 0; i < repeatCount; i++)
							{
								res.AddRange(expressionParameters["text"]);
							}
						}
						else
						{
							throw new MacrosEvaluationException("Failed to find text parameter in given 's' macros");
						}

						return res;
						break;
					}
				case "k":
					{
						string action;
						string keycodeStr;

						if (expressionParameters.ContainsKey("action"))
						{
							action = expressionParameters["action"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("2"))
						{
							action = expressionParameters["2"].ConcatToString();
						}
						else
						{
							action = "press";
						}

						if (expressionParameters.ContainsKey("keycode"))
						{
							keycodeStr = expressionParameters["keycode"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("1"))
						{
							keycodeStr = expressionParameters["1"].ConcatToString();
						}
						else
						{
							throw new MacrosEvaluationException("Failed to find keycode parameter in given 'k' macros");
						}

						KeycodesConfiguration keycodesConfiguration = ConfigHelper.GetKeycodesConfiguration();
						KeycodeConfig keycodeToProcess = keycodesConfiguration.Keycodes.SingleOrDefault(p => p.Names.Where(n => n.KeyRelation == KeyRelation.Native).Any(g => String.Equals(g.Value, keycodeStr, StringComparison.CurrentCultureIgnoreCase)));

						if (keycodeToProcess == null)
						{
							throw new MacrosEvaluationException("No keycode name is recognized in given 'k' macros");
						}

						Keys keyToProcess = (Keys)keycodeToProcess.VirtualKeyCode;
						InputActionType actionType = InputActionType.Press;
						int pressCount = -1;

						List<AutotextInput> res = new List<AutotextInput>(100);

						switch (action.ToLower())
						{
							case "press":
								{
									actionType = InputActionType.Press;
									break;
								}
							case "+":
								{
									actionType = InputActionType.KeyDown;
									break;
								}
							case "-":
								{
									actionType = InputActionType.KeyUp;
									break;
								}
							case "on":
								{
									if (!keycodeToProcess.CanOn)
									{
										throw new MacrosEvaluationException(string.Format("Specified key({0}) can't be set to On state in given 'k' macros", keyToProcess));
									}

									if (!Control.IsKeyLocked(keyToProcess))
									{
										actionType = InputActionType.Press;
									}
									else
									{
										actionType = InputActionType.Press;
										keyToProcess = Keys.None;
									}
									break;
								}
							case "off":
								{
									if (!keycodeToProcess.CanOff)
									{
										throw new MacrosEvaluationException(string.Format("Specified key({0}) can't be set to Off state in given 'k' macros", keyToProcess));
									}

									if (Control.IsKeyLocked(keyToProcess))
									{
										actionType = InputActionType.Press;
									}
									else
									{
										actionType = InputActionType.Press;
										keyToProcess = Keys.None;
									}
									break;
								}
							default://Numeric, to press multiple times
								{
									//pressCount Will be 0 if parsing fails
									if (!int.TryParse(action, out pressCount))
									{
										throw new MacrosEvaluationException("No action is recognized in given 'k' macros");
									}

									break;
								}
						}

						if (pressCount > 0)
						{
							for (int i = 0; i < pressCount; i++)
							{
								res.Add(new AutotextInput(InputType.KeyCode, actionType, keyToProcess));
							}
						}
						else
						{
							res.Add(new AutotextInput(InputType.KeyCode, actionType, keyToProcess));
						}

						return res;
						break;
					}
				//output char
				case "c":
					{
						string action;
						string charToProcess;

						if (expressionParameters.ContainsKey("action"))
						{
							action = expressionParameters["action"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("2"))
						{
							action = expressionParameters["2"].ConcatToString();
						}
						else
						{
							action = "press";
						}

						if (expressionParameters.ContainsKey("char"))
						{
							charToProcess = expressionParameters["char"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("1"))
						{
							charToProcess = expressionParameters["1"].ConcatToString();
						}
						else
						{
							throw new MacrosEvaluationException("Failed to find 'char' parameter in given 'c' macros");
						}

						InputActionType actionType = InputActionType.Press;
						int pressCount = -1;

						List<AutotextInput> res = new List<AutotextInput>(100);

						switch (action.ToLower())
						{
							case "press":
								{
									actionType = InputActionType.Press;
									break;
								}
							case "+":
								{
									actionType = InputActionType.KeyDown;
									break;
								}
							case "-":
								{
									actionType = InputActionType.KeyUp;
									break;
								}
							default://Numeric, to press multiple times
								{
									//pressCount Will be 0 if parsing fails
									if (!int.TryParse(action, out pressCount))
									{
										throw new MacrosEvaluationException("No action is recognized in given 'c' macros");
									}

									break;
								}
						}

						if (pressCount > 0)
						{
							for (int i = 0; i < pressCount; i++)
							{
								res.Add(new AutotextInput(InputType.UnicodeChar, actionType, charToProcess.First()));
							}
						}
						else
						{
							res.Add(new AutotextInput(InputType.UnicodeChar, actionType, charToProcess.First()));
						}

						return res;
						break;
					}
				//output formatted date
				case "d":
					{
						string dateFormat;

						if (expressionParameters.ContainsKey("format"))
						{
							dateFormat = expressionParameters["format"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("1"))
						{
							dateFormat = expressionParameters["1"].ConcatToString();
						}
						else
						{
							throw new MacrosEvaluationException("Failed to find date format parameter in given 'd' macros");
						}

						if (string.IsNullOrEmpty(dateFormat))
						{
							throw new MacrosEvaluationException("Date/time format can't be empty in given 'd' macros");
						}


						dateFormat = dateFormat.Replace("\\", "\\\\").Replace("/", "\\/");

						DateTime now = DateTime.Now;
						string resDateStr;

						try
						{
							resDateStr = now.ToString(dateFormat);
						}
						catch (FormatException ex)
						{
							MacrosEvaluationException expressionEvaluationException =
								new MacrosEvaluationException("Specified date or time format is not a vilid date or time format in given 'd' macros", ex);
							throw expressionEvaluationException;
						}

						return AutotextInput.FromString(resDateStr);

						break;
					}

				case "p":
					{
						int sleepTime;

						if (expressionParameters.ContainsKey("duration"))
						{
							sleepTime = int.Parse(expressionParameters["duration"].ConcatToString());
						}
						else if (expressionParameters.ContainsKey("1"))
						{
							sleepTime = int.Parse(expressionParameters["1"].ConcatToString());
						}
						else
						{
							throw new MacrosEvaluationException("Failed to find pause duration parameter in given 'p' macros");
						}

						return new List<AutotextInput>() { new AutotextInput(InputType.KeyCode, InputActionType.Press, Keys.None) { Sleep = sleepTime } };
						break;

					}
				//output random string
				case "r":
					{
						if (!expressionParameters.ContainsKey("palette") && !expressionParameters.ContainsKey("chars"))
						{
							throw new MacrosEvaluationException("No palette or user character set found in given 'r' macros parameters");
						}

						StringBuilder resPalette = new StringBuilder();

						if (expressionParameters.ContainsKey("palette"))
						{
							string palette = expressionParameters["palette"].ConcatToString();

							if (palette.Contains("l"))
							{
								resPalette.Append(Constants.CommonConstants.LowercaseLetters);
							}

							if (palette.Contains("L"))
							{
								resPalette.Append(Constants.CommonConstants.UppercaseLetters);
							}

							if (palette.Contains("d"))
							{
								resPalette.Append(Constants.CommonConstants.Digits);
								resPalette.Append(Constants.CommonConstants.Digits);
							}

							if (palette.Contains("s"))
							{
								resPalette.Append(Constants.CommonConstants.SpecialChars);
							}
						}

						if (expressionParameters.ContainsKey("chars"))
						{
							resPalette.Append(expressionParameters["chars"].ConcatToString());
						}

						string[] range = expressionParameters["count"].ConcatToString().Split('-');

						long actualRange;

						try
						{
							int minLength = int.Parse(range[0]);
							int maxLength = int.Parse(range[1]);
							actualRange = RandomNumberGeneration.RandomLong(minLength, maxLength + 1);
						}
						catch (Exception ex)
						{
							throw new MacrosEvaluationException("Failed to parse 'count' parameter for given 'r' macros(" + ex.Message+ ")", ex);
						}

						char[] resChars = new char[actualRange];

						for (int i = 0; i < resChars.Length; i++)
						{
							resChars[i] = resPalette[(int)RandomNumberGeneration.RandomLong(resPalette.Length)];
						}

						string finalString = new string(resChars);
						return AutotextInput.FromString(finalString);
						break;

					}
				//output random number
				case "n":
					{
						string range;

						if (expressionParameters.ContainsKey("range"))
						{
							range = expressionParameters["range"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("1"))
						{
							range = expressionParameters["1"].ConcatToString();
						}
						else
						{
							throw new MacrosEvaluationException("No range parameter found in given 'n' macros");
						}

						long min;
						long max;

						try
						{
							string[] split = range.Split('|');
							min = long.Parse(split[0]);
							max = long.Parse(split[1]);
						}
						catch (Exception ex)
						{
							throw new MacrosEvaluationException("Failed to parse 'range' parameter in given 'n' macros(" +
							ex.Message +")", ex);
						}

						if (min > max)
						{
							throw new MacrosEvaluationException("'n' macros: min value can't be greater than max value");
						}

						return AutotextInput.FromString(RandomNumberGeneration.RandomLong(min, max + 1).ToString());
						break;
					}
				//output file contents
				case "f":
					{
						string path;

						if (expressionParameters.ContainsKey("path"))
						{
							path = expressionParameters["path"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("1"))
						{
							path = expressionParameters["1"].ConcatToString();
						}
						else
						{
							throw new MacrosEvaluationException("No path parameter found in given 'f' macros");
						}

						string encName;

						if (expressionParameters.ContainsKey("encoding"))
						{
							encName = expressionParameters["encoding"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("2"))
						{
							encName = expressionParameters["2"].ConcatToString();
						}
						else
						{
							encName = Encoding.Default.EncodingName;
						}

						if (!Encoding.GetEncodings().Any(p => p.Name == encName))
						{
							throw new MacrosEvaluationException("Encoding name is not recognized in given 'f' macros");
						}

						if (!File.Exists(path))
						{
							throw new MacrosEvaluationException("Specified in 'f' macros file is not found");
						}

						try
						{
							string fileContents = File.ReadAllText(path, Encoding.GetEncoding(encName));
							return AutotextInput.FromString(fileContents);
						}
						catch (Exception ex)
						{
							throw new MacrosEvaluationException("Failed to read file contents for given 'f' macros(" + ex.Message +")", ex);
						}

						break;
					}
				//output environment variable
				case "e":
					{
						string name;

						if (expressionParameters.ContainsKey("name"))
						{
							name = expressionParameters["name"].ConcatToString();
						}
						else if (expressionParameters.ContainsKey("1"))
						{
							name = expressionParameters["1"].ConcatToString();
						}
						else
						{
							throw new MacrosEvaluationException("No environment variable parameter found in given 'e' macros");
						}

						return AutotextInput.FromString(Environment.GetEnvironmentVariable(name));
						break;
					}
				default:
					{
						throw new MacrosEvaluationException("Provided macros name(" + expressionName + ") is not a valid macros name");
					}
			}
		}


		public override string ToString()
		{
			return ExpressionText;
		}
	}
}
