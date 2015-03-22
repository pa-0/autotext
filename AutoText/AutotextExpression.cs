using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public class AutotextExpression
	{
		private const string OpenBraceEscapeSeq = "{{}";
		private const string ClosingBraceEscapeSeq = "{}}";
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


		public AutotextExpression(string expressionText)
		{
			ExpressionText = string.Format("{{s:{0} 1}}", expressionText);
			RelativeStartIndex = 0;
			Length = ExpressionText.Length;
			EscapedBraces = new List<int>(100);
			NestedExpressions = new List<AutotextExpression>(100);
			Parameters = new List<AutotextExpressionParameter>(20);
			BuildEscapedBracesList();
			ParseExpression(_parsedExpressionText);
		}

		private AutotextExpression(string expressionText, int startIndex, int length, List<int> escapedBraces, AutotextExpression parentExpression)
		{
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

			ExpressionConfiguration macrosesConfig = ConfigHelper.GetMacrosesConfiguration();
			ExpressionConfigDefinition matchedConfig = null;
			string regex = null;

			foreach (ExpressionConfigDefinition macrosConfig in macrosesConfig.MacrosDefinitions)
			{
				if (Regex.IsMatch(expressionText, macrosConfig.ExplicitParametersRegex))
				{
					matchedConfig = macrosConfig;
					regex = macrosConfig.ExplicitParametersRegex;
					break;
				}

				if (Regex.IsMatch(expressionText, macrosConfig.ImplicitParametersRegex))
				{
					matchedConfig = macrosConfig;
					regex = macrosConfig.ImplicitParametersRegex;
					break;
				}
			}

			ExpressionName = matchedConfig.ShortName;

			MatchCollection macrosParameters = Regex.Matches(expressionText, regex,
				RegexOptions.Singleline | RegexOptions.IgnoreCase);

			for (int i = 0; i < matchedConfig.MacrosParametrers.Count; i++)
			{
				ExpressionConfigParameter parameter = matchedConfig.MacrosParametrers[i];
				Parameters.Add(new AutotextExpressionParameter(parameter.Name, macrosParameters[0].Groups[parameter.Name].Value,
					macrosParameters[0].Groups[parameter.Name].Index, macrosParameters[0].Groups[parameter.Name].Length));
			}

			#endregion


			#region Nested expressions parsing

			int openBraceCounter = 0;
			int closingBraceCounter = 0;
			int nestedExpressionStartIndex = -1;
			int macrosEndIndex = 0;
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
					macrosEndIndex = i;
				}

				if (openBraceCounter != 0 && closingBraceCounter != 0 && openBraceCounter == closingBraceCounter)
				{
					int nestedExpressionLength = (macrosEndIndex + 1) - nestedExpressionStartIndex;
					AutotextExpression expressionToAdd =
						new AutotextExpression(expressionText.Substring(nestedExpressionStartIndex, nestedExpressionLength),
							nestedExpressionStartIndex,
							nestedExpressionLength,
							EscapedBraces,
							this);

					NestedExpressions.Add(expressionToAdd);
					nestedExpressionStartIndex = -1;
					macrosEndIndex = 0;
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

			//Replace macros definitions to macros evaluation results in the parameters before current expression evaluation
			for (int i = 0; i < Parameters.Count; i++)
			{
				AutotextExpressionParameter param = Parameters[i];
				List<AutotextInput> paramInputs = AutotextInput.FromString(param.Value);

				for (int j = 0; j < NestedExpressions.Count; j++)
				{
					if ( NestedExpressions[j].RelativeStartIndex >= param.RelativeStartIndex 
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

			List<AutotextInput> res = Evaluate(ExpressionName, parameters);
			return res;
		}

		private static List<AutotextInput> Evaluate(string macrosName, Dictionary<string, List<AutotextInput>> macrosParameters)
		{
			switch (macrosName.ToLower())
			{
				case "s":
				{
					StringBuilder resStr = new StringBuilder(1000);

					int repeatCount = Int32.Parse(String.Concat(macrosParameters["count"].Select(p => p.CharToInput)));
					string value = String.Concat(macrosParameters["text"].Select(p => p.CharToInput));

					for (int i = 0; i < repeatCount; i++)
					{
						resStr.Append(value);
					}

					List<AutotextInput> res = AutotextInput.FromString(resStr);
					return res;
					break;
				}

				default:
				{
					throw new ArgumentOutOfRangeException("macrosName");
				}
			}

			throw new NotImplementedException();
		}
	}
}
