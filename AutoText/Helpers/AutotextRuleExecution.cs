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
		public string PhraseText { get; set; }
		public List<AutotextExpression> Expressions { get; set; }

		public AutotextPhrase(string phraseText)
		{
			PhraseText = phraseText;
			Expressions = new List<AutotextExpression>(100);
		}
	}

	public enum AutotextExpressionType
	{
		PlainText,
		Macros
	}

	public class AutotextExpression
	{
		public AutotextExpressionType ExpressionType { get; set; }
		public string ExpressionText { get; set; }
		public int StartIndex { get; set; }
		public int EndIndex { get; set; }
		public List<AutotextExpression> NestedExpressions { get; set; }

		public AutotextExpression()
		{
			NestedExpressions = new List<AutotextExpression>(100);
		}

		public AutotextExpression(AutotextExpressionType expressionType, string expressionText, int startIndex, int endIndex)
		{
			ExpressionType = expressionType;
			ExpressionText = expressionText;
			StartIndex = startIndex;
			EndIndex = endIndex;
			NestedExpressions = new List<AutotextExpression>(100);
		}
	}

	public static class AutotextRuleExecution
	{
		private const string OpeningBraceMacrosReplacement = "#f5672947-23ad-4e71-a2e6-f4440d34a874#";
		private const string ClosingBraceMacrosReplacement = "#eb283470-f0bd-43bb-b13b-2fdbd6e4f949#";
		private const string OpenBraceEscapeSeq = "{{}";
		private const string ClosingBraceEscapeSeq = "{}}";

		public static void ProcessRule(AutotextRuleConfig rule)
		{
			List<AutotextExpression> expressions = ParsePhrase(rule.Phrase);
			{ }
		}

		public static List<int> GetEscapedBracesList(string rulePhrase)
		{
			List<int> res = new List<int>();
			int indexOfEscapedBracket;
			int curIndex = 0;


			while (true)
			{
				 indexOfEscapedBracket = rulePhrase.IndexOf(OpenBraceEscapeSeq,curIndex);

				if (indexOfEscapedBracket != -1)
				{
					rulePhrase.Remove(indexOfEscapedBracket, 3);
					rulePhrase.Insert(indexOfEscapedBracket, "{");
					res.Add(indexOfEscapedBracket);
					curIndex = indexOfEscapedBracket - 2;
				}
				else
				{
					break;
				}
			}

			curIndex = 0;

			while (true)
			{
				indexOfEscapedBracket = rulePhrase.IndexOf(ClosingBraceEscapeSeq, curIndex);

				if (indexOfEscapedBracket != -1)
				{
					rulePhrase.Remove(indexOfEscapedBracket, 3);
					rulePhrase.Insert(indexOfEscapedBracket, "}");
					res.Add(indexOfEscapedBracket);
					curIndex = indexOfEscapedBracket - 2;
				}
				else
				{
					break;
				}
			}

			return res;
		}

		private static List<AutotextExpression> ParsePhrase(string rulePhrase)
		{
			/*
			string phrase = rulePhrase.Replace(OpenBraceEscapeSeq,OpeningBraceMacrosReplacement).
				Replace(ClosingBraceEscapeSeq,ClosingBraceMacrosReplacement);
			*/

			

			/*
			AutotextExpression rootExpression = new AutotextExpression(AutotextExpressionType.PlainText, phrase, 0, phrase.Length);
			ParseExpressionRecursive(rootExpression);
			*/
			throw new NotImplementedException();
		}

		private static void ParseExpressionRecursive(AutotextExpression expression)
		{
			int openBraceCounter = 0;
			int closingBraceCounter = 0;
			int macrosStartIndex = -1;
			int macrosEndIndex = 0;

			for (int i = 0; i < expression.ExpressionText.Length; i++)
			{
				if (expression.ExpressionText[i] == '{')
				{
					openBraceCounter++;

					if (macrosStartIndex == -1)
					{
						macrosStartIndex = i;
					}
				}

				if (expression.ExpressionText[i] == '}')
				{
					closingBraceCounter++;
					macrosEndIndex = i;
				}

				if (openBraceCounter != 0 && closingBraceCounter != 0 && openBraceCounter == closingBraceCounter)
				{
					AutotextExpression expressionToAdd = new AutotextExpression();
					//expression.NestedExpressions.Add(new AutotextExpression(AutotextExpressionType.Macros, expression.ExpressionText.Substring(macrosStartIndex + 1, macrosEndIndex - macrosStartIndex - 1)));
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
	}
}
