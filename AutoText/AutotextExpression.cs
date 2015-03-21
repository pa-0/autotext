using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;
using AutoText.Helpers.Extensions;

namespace AutoText
{
	public class MacrosParameter
	{
		public string Name { get; private set; }
		public string Value { get; private set; }
		public int StartIndex { get; private set; }
		public int Length { get; private set; }

		public MacrosParameter(string name, string value, int startIndex, int length)
		{
			Name = name;
			Value = value;
			StartIndex = startIndex;
			Length = length;
		}
	}

	public class AutotextExpression
	{
		private const string OpenBraceEscapeSeq = "{{}";
		private const string ClosingBraceEscapeSeq = "{}}";
		private static Regex _bracketsRegex = new Regex(@"{{}|{}}", RegexOptions.Compiled);

		public string ExpressionText { get; private set; }
		private string _parsedExpressionText;
		public int StartIndex { get; private set; }
		public int Length { get; private set; }
		public List<AutotextExpression> NestedExpressions { get; private set; }
		private List<int> EscapedBraces { get; set; }
		public string MacrosName { get; private set; }
		public List<MacrosParameter> Parameters { get; private set; }


		public AutotextExpression(string expressionText, int startIndex, int length)
		{
			ExpressionText = expressionText;
			StartIndex = startIndex;
			Length = length;
			EscapedBraces = new List<int>(100);
			NestedExpressions = new List<AutotextExpression>(100);
			Parameters = new List<MacrosParameter>(20);
			BuildEscapedBracesList();
			ParseExpression(_parsedExpressionText);
		}

		public AutotextExpression(string expressionText, int startIndex, int length, List<int> escapedBraces)
		{
			ExpressionText = expressionText;
			StartIndex = startIndex;
			Length = length;
			NestedExpressions = new List<AutotextExpression>(100);
			Parameters = new List<MacrosParameter>(20);
			EscapedBraces = escapedBraces;
			ParseExpression(ExpressionText);
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
			MacrosesConfiguration macrosesConfig = ConfigHelper.GetMacrosesConfiguration();
			MacrosConfigDefinition matchedConfig = null;
			string regex = null;

			foreach (MacrosConfigDefinition macrosConfig in macrosesConfig.MacrosDefinitions)
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

			MacrosName = matchedConfig.ShortName;

			MatchCollection macrosParameters = Regex.Matches(expressionText, regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

			for (int i = 0; i < matchedConfig.MacrosParametrers.Count; i++)
			{
				MacrosConfigParameter parameter = matchedConfig.MacrosParametrers[i];
				Parameters.Add(new MacrosParameter(parameter.Name, macrosParameters[0].Groups[parameter.Name].Value, macrosParameters[0].Groups[parameter.Name].Index, macrosParameters[0].Groups[parameter.Name].Length));
			}
/*-------------------------------------------------------------------------------------------------------------------------------*/

			int openBraceCounter = 0;
			int closingBraceCounter = 0;
			int macrosStartIndex = -1;
			int macrosEndIndex = 0;

			for (int i = 1; i < expressionText.Length - 1; i++)
			{
				if (expressionText[i] == '{' && !EscapedBraces.Contains(i))
				{
					openBraceCounter++;

					if (macrosStartIndex == -1)
					{
						macrosStartIndex = i;
					}
				}

				if (expressionText[i] == '}' && !EscapedBraces.Contains(i))
				{
					closingBraceCounter++;
					macrosEndIndex = i;
				}

				if (openBraceCounter != 0 && closingBraceCounter != 0 && openBraceCounter == closingBraceCounter)
				{
					int macrosLength = (macrosEndIndex + 1) - macrosStartIndex;
					AutotextExpression expressionToAdd = new AutotextExpression(expressionText.Substring(macrosStartIndex, macrosLength), macrosStartIndex, macrosLength, EscapedBraces);
					NestedExpressions.Add(expressionToAdd);
					macrosStartIndex = -1;
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
		}

		public List<Input> GetInput()
		{
			List<List<Input>> nestedExpressionsInput = new List<List<Input>>();

			for (int i = 0; i < NestedExpressions.Count; i++)
			{
				nestedExpressionsInput.Add(NestedExpressions[i].GetInput());
			}

			Dictionary<string,List<Input>> parameters = new Dictionary<string, List<Input>>(20);

			for (int i = 0; i < Parameters.Count; i++)
			{
				MacrosParameter param = Parameters[i];
				bool paramAdded = false;
				List<Input> paramInputs = Input.FromString(param.Value);

				for (int j = 0; j < NestedExpressions.Count; j++)
				{
					if ( NestedExpressions[j].StartIndex >= param.StartIndex 
						&& (NestedExpressions[j].StartIndex + NestedExpressions[j].Length) <= (param.StartIndex + param.Length))
					{
						paramAdded = true;
						paramInputs.RemoveRange(NestedExpressions[j].StartIndex - param.StartIndex, NestedExpressions[j].Length);
						paramInputs.InsertRange(NestedExpressions[j].StartIndex - param.StartIndex, nestedExpressionsInput[j]);
						parameters.Add(param.Name, paramInputs);
					}
				}

				if (!paramAdded)
				{
					parameters.Add(param.Name, paramInputs);
				}
			}

			List<Input> res = Macros.Evaluate(MacrosName, parameters);
			return res;
		}
	}
}
