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
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;
using AutoText.Model.Configuration;

namespace AutoText.Engine
{
	public class AutotextExpression
	{
		private const string OpenBraceEscapeSeq = "{{}";
		private const string ClosingBraceEscapeSeq = "{}}";
		private readonly string ShortcutsRegexTemplate;
		private const string ShortcutsEscapeRegexTemplate = @"{{{0}}}";
		private static readonly Regex _bracketsRegex = new Regex(@"{{}|{}}", RegexOptions.Compiled);

		public string ExpressionText { get; private set; }
		private string _parsedExpressionText;
		public int RelativeStartIndex { get; private set; }
		public int Length { get; private set; }
		public List<AutotextExpression> NestedExpressions { get; private set; }
		private List<int> EscapedBraces { get; set; }
		public string ExpressionName { get; private set; }
		public List<AutotextExpressionParameter> Parameters { get; private set; }
		public AutotextExpression ParentExpression { get; private set; }
		private Dictionary<string, string> _userVariables = new Dictionary<string, string>();

		public AutotextExpression(AutotextRuleMatchParameters autotextRuleMatchParams)
		{
			string abbrRemoveText = "";

			if (autotextRuleMatchParams != null && autotextRuleMatchParams.AutotextRuleConfiguration.RemoveAbbr)
			{
				for (int i = 0; i < autotextRuleMatchParams.AutotextRuleConfiguration.Abbreviation.AbbreviationText.Length; i++)
				{
					abbrRemoveText += "{Back}";
				}


				string[] nonPrintableTriggers = ConfigHelper.GetExpressionsConfiguration().NonPrintableTriggers.Split(',').Select(p => "{" + p + "}").ToArray();
				if (!nonPrintableTriggers.Contains(autotextRuleMatchParams.MatchTrigger.Value))
				{
					abbrRemoveText += "{Back}";
				}
			}

			string phraseText = autotextRuleMatchParams.AutotextRuleConfiguration.PhraseCompiled;


			if (autotextRuleMatchParams.AutotextRuleConfiguration.Macros.Mode == MacrosMode.Skip)
			{
				for (int i = 0; i < phraseText.Length; i++)
				{
					if (phraseText[i] == '{')
					{
						phraseText = phraseText.Remove(i, 1);
						phraseText = phraseText.Insert(i, "{{}");
						i += 2;
						continue;
					}

					if (phraseText[i] == '}')
					{
						phraseText = phraseText.Remove(i, 1);
						phraseText = phraseText.Insert(i, "{}}");
						i += 2;
						continue;

					}

					if (phraseText[i] == '(')
					{
						phraseText = phraseText.Remove(i, 1);
						phraseText = phraseText.Insert(i, "{(}");
						i += 2;
						continue;

					}

					if (phraseText[i] == ')')
					{
						phraseText = phraseText.Remove(i, 1);
						phraseText = phraseText.Insert(i, "{)}");
						i += 2;
						continue;

					}
				}
			}


			ExpressionText = string.Format("{{s:{0} 1}}", abbrRemoveText + phraseText);
			ShortcutsRegexTemplate = ConfigHelper.GetExpressionsConfiguration().ShortcutRegexTemplate;
			RelativeStartIndex = 0;
			Length = ExpressionText.Length;
			EscapedBraces = new List<int>(100);
			NestedExpressions = new List<AutotextExpression>(100);
			Parameters = new List<AutotextExpressionParameter>(20);

			ProcessShortcuts();
			BuildEscapedBracesList();
			ParseExpression(_parsedExpressionText);


			/*List<int> indeces = new List<int>(100);
			GetAllExpressionBraceIndeces(this, indeces);

			for (int i = 0; i < ExpressionText.Length; i++)
			{
				if (indeces.Contains(i))
				{
					continue;
				}
				else
				{
					if (ExpressionText[i] == '{')
					{
						ExpressionText.Insert(i, "{{}");
						i += 2;
					}

					if (ExpressionText[i] == '}')
					{
						ExpressionText.Insert(i, "{}}");
						i += 2;
					}
				}
			}*/



			if (autotextRuleMatchParams.AutotextRuleConfiguration.Abbreviation.Type == Abbriviationtype.Regex && autotextRuleMatchParams.AutotextRuleConfiguration.MatchedString != null)
			{
				Regex reg = new Regex(autotextRuleMatchParams.AutotextRuleConfiguration.Abbreviation.AbbreviationText);
				MatchCollection matches = reg.Matches(autotextRuleMatchParams.AutotextRuleConfiguration.MatchedString);

				string[] groupNames = reg.GetGroupNames();

				if (matches.Count == 0)
				{
					throw new InvalidOperationException("Error in regex abbreviation parsing");
				}

				foreach (string groupName in groupNames)
				{
					_userVariables.Add(groupName, matches.Cast<Match>().Last().Groups[groupName].Value);
				}
			}
		}

/*
		private void GetAllExpressionBraceIndeces(AutotextExpression expr, List<int> res)
		{
			res.Add(expr.GetAbsoluteStartIndex());
			res.Add(expr.GetAbsoluteStartIndex() + expr.Length);

			foreach (AutotextExpression expression in expr.NestedExpressions)
			{
				GetAllExpressionBraceIndeces(expression, res);
			}

		}

*/
		private AutotextExpression(string expressionText, int startIndex, int length, List<int> escapedBraces, AutotextExpression parentExpression, Dictionary<string, string> userVars)
		{
			_userVariables = userVars;
			ParentExpression = parentExpression;
			ExpressionText = expressionText;
			RelativeStartIndex = startIndex;
			Length = length;
			NestedExpressions = new List<AutotextExpression>(100);
			Parameters = new List<AutotextExpressionParameter>(20);
			EscapedBraces = escapedBraces;
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

		private void ProcessShortcuts()
		{
			List<string> shortcutsList = ConfigHelper.GetKeycodesConfiguration().Keycodes.Where(p => p.Shortcut != "none").Select(p => p.Shortcut).ToList();
			List<string> shortcutsListEscaped = shortcutsList.Select(p => string.Format(ShortcutsEscapeRegexTemplate, p)).ToList();
			string shortcuts = Regex.Escape(string.Concat(shortcutsList));
			Regex shortcutsRegex = new Regex(ShortcutsRegexTemplate.Replace("#shortcutsPlaceholder#", shortcuts), RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline);
			MatchCollection matches = shortcutsRegex.Matches(ExpressionText);
			Stack<string> splStack;
			Stack<Match> splStringsStack = new Stack<Match>(matches.Cast<Match>().Select(p => p).Reverse());
			StringBuilder sbRes = new StringBuilder(1000);
			List<string> split = new List<string>();

			int startIndex = 0;

			//Determine shortcuts index for extraction
			for (int i = 0; i < matches.Count; i++)
			{
				Match m = matches[i];

				split.Add(ExpressionText.Substring(startIndex, m.Index - startIndex));
				startIndex = m.Index + m.Length;
			}

			split.Add(ExpressionText.Substring(startIndex));

			split.Reverse();
			splStack = new Stack<string>(split);

			string target = null;

			//Expanding shortcuts to their corresponding macroses
			while (splStack.Count > 0)
			{
				string expandedKeys = "";


				sbRes.Append(splStack.Pop());

				if (splStringsStack.Count > 0)
				{
					Match spMatch = splStringsStack.Pop();
					target = spMatch.Groups["target"].Value;
					string keycodes = "";

					//Keycodes in key combination
					foreach (KeycodeConfig keycode in ConfigHelper.GetKeycodesConfiguration().Keycodes)
					{
						foreach (KeycodeConfigName name in keycode.Names)
						{
							if (target.Contains("{"+ name.Value + "}"))
							{
								string kkToAd = "{k:" + name.Value + " 1}";
								expandedKeys += kkToAd;
								target = target.Replace("{" + name.Value + "}", "");
							}
						}
					}

					//Simple keys
					foreach (char c in target)
					{
						expandedKeys += string.Format("{{k:{0} 1}}", c);
					}

					string strToInput = ExpandShortcuts(string.Concat(spMatch.Groups["shortcuts"].Value.Distinct()), expandedKeys);
					sbRes.Append(strToInput);
				}
			}

			//Replace escaped shortcuts with their symbols
			shortcutsListEscaped.ForEach(p => sbRes = sbRes.Replace(p, p.Trim('{', '}')));
			//Replace escaped parentheses with their symbols
			sbRes = sbRes.Replace("{(}", "(");
			sbRes = sbRes.Replace("{)}", ")");

			ExpressionText = sbRes.ToString();
		}

		private void BuildEscapedBracesList()
		{
			MatchCollection matches = _bracketsRegex.Matches(ExpressionText);
			string[] splitted = _bracketsRegex.Split(ExpressionText);
			StringBuilder resStr = new StringBuilder(1000);

			Stack<string> splittedStr = new Stack<string>(splitted.Reverse());
			Stack<string> braces = new Stack<string>(matches.Cast<Match>().Select(p => p.Value).Reverse());

			while (splittedStr.Count > 0)
			{
				resStr.Append(splittedStr.Pop());

				if (braces.Count > 0)
				{
					string escapedBrace = braces.Pop();

					if (escapedBrace == OpenBraceEscapeSeq)
					{
						resStr.Append("{");
					}

					if (escapedBrace == ClosingBraceEscapeSeq)
					{
						resStr.Append("}");
					}

					EscapedBraces.Add(resStr.Length - 1);
				}
			}

			if (resStr.Length > 0)
			{
				_parsedExpressionText = resStr.ToString();
			}
			else
			{
				_parsedExpressionText = ExpressionText;
			}
		}

		private void ParseExpression(string expressionText)
		{
			#region Parameters parsing

			ExpressionConfiguration expressionConfig = ConfigHelper.GetExpressionsConfiguration();
			ExpressionConfigDefinition matchedConfig = null;
			string regex = null;

			foreach (ExpressionConfigDefinition expressionConfigDefinition in expressionConfig.ExpressionDefinitions)
			{
				if (Regex.IsMatch(expressionText, expressionConfigDefinition.ImplicitParametersRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline))
				{
					matchedConfig = expressionConfigDefinition;
					regex = expressionConfigDefinition.ImplicitParametersRegex;
					break;
				}
			}

			ExpressionName = matchedConfig.ShortName;

			MatchCollection expressionParameters = Regex.Matches(expressionText, regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

			for (int i = 0; i < matchedConfig.ExpessionParametrers.Count; i++)
			{
				ExpressionConfigParameter parameter = matchedConfig.ExpessionParametrers[i];
				Parameters.Add(new AutotextExpressionParameter(parameter.Name, expressionParameters[0].Groups[parameter.Name].Value,
					expressionParameters[0].Groups[parameter.Name].Index, expressionParameters[0].Groups[parameter.Name].Length));
			}

			#endregion


			#region Nested expressions parsing

			int openBraceCounter = 0;
			int closingBraceCounter = 0;
			int nestedExpressionStartIndex = -1;
			int exprEndIndex = 0;
			int absStartIndex = GetAbsoluteStartIndex();


			for (int i = 1; i < expressionText.Length - 1; i++)
			{
				if (expressionText[i] == '{' && !EscapedBraces.Contains(absStartIndex + i))
				{
					openBraceCounter++;

					if (nestedExpressionStartIndex == -1)
					{
						nestedExpressionStartIndex = i;
					}
				}

				if (expressionText[i] == '}' && !EscapedBraces.Contains(absStartIndex + i))
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
							EscapedBraces,
							this,
							_userVariables);

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
					throw new InvalidOperationException("Open brace not found");
				}

				if (openBraceCounter > 0)
				{
					throw new InvalidOperationException("Closing brace not found");
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

				parameters.Add(param.Name, paramInputs);
			}


			List<AutotextInput> res = Evaluate(ExpressionName, parameters, _userVariables);

			return res;
		}

		private static List<AutotextInput> Evaluate(string expressionName, Dictionary<string, List<AutotextInput>> expressionParameters, Dictionary<string, string> userVariables)
		{
			switch (expressionName.ToLower())
			{
				case "s":
					{
						string count = String.Concat(expressionParameters["count"].Select(p => p.CharToInput));

						if (count == "")
						{
							count = "1";
						}

						int repeatCount = Int32.Parse(count);
						List<AutotextInput> res = new List<AutotextInput>(repeatCount * expressionParameters["text"].Count);

						for (int i = 0; i < repeatCount; i++)
						{
							res.AddRange(expressionParameters["text"]);
						}

						return res;
						break;
					}
				case "k":
					{
						string keycodeStr = String.Concat(expressionParameters["keycode"].Select(p => p.CharToInput));
						string action = String.Concat(expressionParameters["action"].Select(p => p.CharToInput));

						KeycodesConfiguration keycodesConfiguration = ConfigHelper.GetKeycodesConfiguration();
						KeycodeConfig keycodeToProcess = keycodesConfiguration.Keycodes.SingleOrDefault(p => p.Names.Any(g => String.Equals(g.Value, keycodeStr, StringComparison.CurrentCultureIgnoreCase)));

						if (keycodeToProcess == null)
						{
							throw new ExpressionEvaluationException("No keycode name is not recognized in given expression");
						}

						//Keys keycode = (Keys)Enum.Parse(typeof(Keys), keycodeToProcess.Name, true);
						Keys keycode = (Keys)keycodeToProcess.Value;
						InputActionType actionType = InputActionType.Press;
						int pressCount = -1;

						List<AutotextInput> res = new List<AutotextInput>(100);

						switch (action.ToLower())
						{
							case "":
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
										throw new ExpressionEvaluationException(string.Format("Specified key({0}) can't be set to On", keycode));
									}

									if (!Control.IsKeyLocked(keycode))
									{
										actionType = InputActionType.Press;
									}
									else
									{
										actionType = InputActionType.Press;
										keycode = Keys.None;
									}
									break;
								}
							case "off":
								{
									if (!keycodeToProcess.CanOff)
									{
										throw new ExpressionEvaluationException(string.Format("Specified key({0}) can't be set to Off", keycode));
									}

									if (Control.IsKeyLocked(keycode))
									{
										actionType = InputActionType.Press;
									}
									else
									{
										actionType = InputActionType.Press;
										keycode = Keys.None;
									}
									break;
								}
							case "t":
							case "toggle":
								{
									if (!keycodeToProcess.Toggleable)
									{
										throw new ExpressionEvaluationException(string.Format("Specified key({0}) can be toggled", keycode));
									}

									actionType = InputActionType.Press;
									break;
								}
							default://Numeric, to press multiple times
								{
									//pressCount Will be 0 if parsing fails
									if (!int.TryParse(action, out pressCount))
									{
										throw new ExpressionEvaluationException("No action is recognized in given expression");
									}

									break;
								}
						}

						if (pressCount > 0)
						{
							for (int i = 0; i < pressCount; i++)
							{
								res.Add(new AutotextInput(InputType.KeyCode, actionType, keycode));
							}
						}
						else
						{
							res.Add(new AutotextInput(InputType.KeyCode, actionType, keycode));
						}

						return res;
						break;
					}
				case "v":
					{
						string userVarName = String.Concat(expressionParameters["name"].Select(p => p.CharToInput));

						if (!userVariables.ContainsKey(userVarName))
						{
							throw new ExpressionEvaluationException(string.Format("User variable with name \"{0}\" is not found in input string", userVarName));
						}

						return AutotextInput.FromString(userVariables[userVarName]);

						break;
					}

				case "d":
					{
						string dateFormat = expressionParameters["format"].ConcatToString().Replace("\\","\\\\").Replace("/","\\/");
						DateTime now = DateTime.Now;
						string resDateStr;

						try
						{
							resDateStr = now.ToString(dateFormat);
						}
						catch (FormatException ex)
						{
							ExpressionEvaluationException expressionEvaluationException =
								new ExpressionEvaluationException("Error during expression parsing. Specified date or time format is not a vilid date or time format", ex);
							throw expressionEvaluationException;
						}

						return AutotextInput.FromString(resDateStr);

						break;
					}

				case "p":
				{
					int sleepTime = int.Parse(expressionParameters["duration"].ConcatToString());
					return new List<AutotextInput>() { new AutotextInput(InputType.KeyCode, InputActionType.Press, Keys.None) { Sleep = sleepTime } };
					break;

				}

				case "r":
					{
						if (!expressionParameters.ContainsKey("palette") && !expressionParameters.ContainsKey("chars"))
						{
							throw new ExpressionEvaluationException("No palette or user character set found in parameters");
						}

						StringBuilder resPalette = new StringBuilder();

						if (expressionParameters.ContainsKey("palette"))
						{
							string palette = new string(expressionParameters["palette"].Select(p => p.CharToInput).ToArray());

							if (palette.Contains("l"))
							{
								resPalette.Append(Constants.Common.LowercaseLetters);
							}

							if (palette.Contains("L"))
							{
								resPalette.Append(Constants.Common.UppercaseLetters);
							}

							if (palette.Contains("d"))
							{
								resPalette.Append(Constants.Common.Digits);
							}

							if (palette.Contains("s"))
							{
								resPalette.Append(Constants.Common.SpecialChars);
							}
						}

						if (expressionParameters.ContainsKey("chars"))
						{
							resPalette.Append(new string(expressionParameters["chars"].Select(p => p.CharToInput).ToArray()));
						}

						string[] range = new string(expressionParameters["count"].Select(p => p.CharToInput).ToArray()).Split('-');

						int minLength = int.Parse(range[0]);
						int maxLength = int.Parse(range[1]);
						int actualRange = new Random(DateTime.Now.Millisecond).Next(minLength, maxLength + 1);

						char[] resChars = new char[actualRange];
						Random random = new Random(DateTime.Now.Millisecond);

						for (int i = 0; i < resChars.Length; i++)
						{
							resChars[i] = resPalette[random.Next(resPalette.Length)];
						}

						string finalString = new string(resChars);
						return AutotextInput.FromString(finalString);
						break;

					}

				default:
					{
						throw new ArgumentOutOfRangeException("expressionName");
					}
			}

			throw new InvalidOperationException();
		}

		private static string ExpandShortcuts(string shortcuts, string target)
		{
			StringBuilder res = new StringBuilder(100);
			KeycodesConfiguration kkConfiguration = ConfigHelper.GetKeycodesConfiguration();

			for (int i = 0; i < shortcuts.Length; i++)
			{
				string shortcut = shortcuts[i].ToString();
				List<string> keycodeStr = kkConfiguration.Keycodes.Where(p => p.Shortcut == shortcut).Select(p => p.Names.First().Value).ToList();

				foreach (string s in keycodeStr)
				{
					res.Append(string.Format("{{{0} +}}", s));
				}

			}

			res.Append(target);

			//Reverse, so Up event should go in reversed order
			shortcuts = string.Concat(shortcuts.Reverse());

			for (int i = 0; i < shortcuts.Length; i++)
			{
				string shortcut = shortcuts[i].ToString();
				List<string> keycodeStr = kkConfiguration.Keycodes.Where(p => p.Shortcut == shortcut).Select(p => p.Names.First().Value).ToList();

				foreach (string s in keycodeStr)
				{
					res.Append(string.Format("{{{0} -}}", s));
				}
			}

			{ }

			return res.ToString();
		}

	}
}
