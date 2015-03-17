using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoText.Helpers.Configuration;

namespace AutoText.Helpers
{
	public class AutotextPhrase
	{
		private const string OpenBraceEscapeSeq = "{{}";
		private const string ClosingBraceEscapeSeq = "{}}";
		private static Regex _bracketsRegex = new Regex(@"{{}|{}}", RegexOptions.Compiled);

		public string PhraseText { get; private set; }
		public string _parsedPhrase;
		public AutotextExpression RootExpression { get; private set; }
		public List<int> EscapedBrackets { get; private set; }

		public AutotextPhrase(string phraseText)
		{
			EscapedBrackets = new List<int>();
			PhraseText = phraseText;
			BuildEscapedBracesList();
			RootExpression = new AutotextExpression(_parsedPhrase, 0, _parsedPhrase.Length);
			ParseExpressionRecursive(RootExpression);
			{ }
		}

		private  void BuildEscapedBracesList()
		{
			MatchCollection matches = _bracketsRegex.Matches(PhraseText);
			string[] splitted = _bracketsRegex.Split(PhraseText);
			StringBuilder resStr = new StringBuilder(1000);


			for (int i = 0; i < splitted.Length; i++)
			{
				if (i < matches.Count)
				{
					if (matches[i].Value == OpenBraceEscapeSeq)
					{
						resStr.Append(splitted[i] + "{");
					}

					if (matches[i].Value == ClosingBraceEscapeSeq)
					{
						resStr.Append(splitted[i] + "}");
					}

					EscapedBrackets.Add(resStr.Length - 1);
				}
				else
				{
					resStr.Append(splitted[i]);
				}
			}

			if (resStr.Length > 0)
			{
				_parsedPhrase = resStr.ToString(); ;
			}
			else
			{
				_parsedPhrase = PhraseText;

			}
		}

		private  void ParseExpressionRecursive(AutotextExpression expression)
		{
			int openBraceCounter = 0;
			int closingBraceCounter = 0;
			int macrosStartIndex = -1;
			int macrosEndIndex = 0;

			for (int i = 0; i < expression.ExpressionText.Length; i++)
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
					int macrosLength = macrosEndIndex - macrosStartIndex - 1;
					AutotextExpression expressionToAdd = new AutotextExpression(expression.ExpressionText.Substring(macrosStartIndex + 1, macrosLength), macrosStartIndex + 1, macrosLength);
					expression.NestedExpressions.Add(expressionToAdd);
					macrosStartIndex = -1;
					macrosEndIndex = 0;
					openBraceCounter = 0;
					closingBraceCounter = 0;
				}
			}

			foreach (AutotextExpression parsedMacros in expression.NestedExpressions)
			{
				ParseExpressionRecursive(parsedMacros);
			}
		}

		private  List<AutotextExpression> ParsePhrase(string rulePhrase)
		{
			throw new NotImplementedException();
		}
	}

	public class AutotextExpression
	{
		public string ExpressionText { get; set; }
		public int StartIndex { get; set; }
		public int Length { get; set; }
		public List<AutotextExpression> NestedExpressions { get; set; }

		public AutotextExpression()
		{
			NestedExpressions = new List<AutotextExpression>(100);
		}

		public AutotextExpression( string expressionText, int startIndex, int length)
		{
			ExpressionText = expressionText;
			StartIndex = startIndex;
			Length = length;
			NestedExpressions = new List<AutotextExpression>(100);
		}

		private List<Input> BuildExpressionInput()
		{
			throw new NotImplementedException();
		}

		public List<Input> GetInput()
		{

			throw new NotImplementedException();
		}
	}

	public class Input
	{
		public InputType Type { get; set; }
		public InputActionType ActionType { get; set; }
		public char CharToInput { get; set; }
		public int KeyCodeValueToInput{ get; set; }
	}

	public enum InputActionType
	{
		KeyDown,
		KeyUp,
		Press
	}

	public enum InputType
	{
		UnicideChar,
		KeyCode
	}

	public static class AutotextRuleExecution
	{
		public static void ProcessRule(AutotextRuleConfig rule)
		{
			AutotextPhrase phrase = new AutotextPhrase(rule.Phrase);
			{ }
		}
	}
}
