using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoText
{
	public class AutotextPhrase
	{
		private const string OpenBraceEscapeSeq = "{{}";
		private const string ClosingBraceEscapeSeq = "{}}";
		private static Regex _bracketsRegex = new Regex(@"{{}|{}}", RegexOptions.Compiled);

		public string PhraseText { get; private set; }
		private string _parsedPhrase;
		public AutotextExpression RootExpression { get; private set; }
		public List<int> EscapedBrackets { get; private set; }

		public AutotextPhrase(string phraseText)
		{
			EscapedBrackets = new List<int>();
			PhraseText = string.Format("{{s:{0} 1}}", phraseText);
			BuildEscapedBracesList();
			AutotextExpression rootExpr = new AutotextExpression(_parsedPhrase, 0, _parsedPhrase.Length);
			ParseExpressionsRecursive(rootExpr);
			RootExpression = rootExpr;
			{ }
		}

		private void BuildEscapedBracesList()
		{
			MatchCollection matches = _bracketsRegex.Matches(PhraseText);
			string[] splitted = _bracketsRegex.Split(PhraseText);
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

					EscapedBrackets.Add(resStr.Length - 1);
				}
			}

			if (resStr.Length > 0)
			{
				_parsedPhrase = resStr.ToString();
			}
			else
			{
				_parsedPhrase = PhraseText;
			}
		}

		private void ParseExpressionsRecursive(AutotextExpression expression)
		{
			int openBraceCounter = 0;
			int closingBraceCounter = 0;
			int macrosStartIndex = -1;
			int macrosEndIndex = 0;

			for (int i = 1; i < expression.ExpressionText.Length - 1; i++)
			{
				if (expression.ExpressionText[i] == '{' && !EscapedBrackets.Contains(i))
				{
					openBraceCounter++;

					if (macrosStartIndex == -1)
					{
						macrosStartIndex = i;
					}
				}

				if (expression.ExpressionText[i] == '}' && !EscapedBrackets.Contains(i))
				{
					closingBraceCounter++;
					macrosEndIndex = i;
				}

				if (openBraceCounter != 0 && closingBraceCounter != 0 && openBraceCounter == closingBraceCounter)
				{
					int macrosLength = (macrosEndIndex + 1) - macrosStartIndex ;
					AutotextExpression expressionToAdd = new AutotextExpression(expression.ExpressionText.Substring(macrosStartIndex , macrosLength), macrosStartIndex , macrosLength);
					expression.NestedExpressions.Add(expressionToAdd);
					macrosStartIndex = -1;
					macrosEndIndex = 0;
					openBraceCounter = 0;
					closingBraceCounter = 0;
				}
			}

			foreach (AutotextExpression parsedMacros in expression.NestedExpressions)
			{
				ParseExpressionsRecursive(parsedMacros);
			}
		}
	}
}
