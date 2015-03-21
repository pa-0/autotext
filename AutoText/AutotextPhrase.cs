using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoText
{
	public class AutotextPhrase
	{

		public string PhraseText { get; private set; }
		public AutotextExpression RootExpression { get; private set; }

		public AutotextPhrase(string phraseText)
		{
			PhraseText = string.Format("{{s:{0} 1}}", phraseText);
			AutotextExpression rootExpr = new AutotextExpression(PhraseText, 0, PhraseText.Length);
			RootExpression = rootExpr;
		}
	}
}
