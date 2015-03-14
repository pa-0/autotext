using System;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public class AutotextMatchEventArgs : EventArgs
	{
		public AutotextRuleConfig MatchedRule { get; private set; }

		public AutotextMatchEventArgs(AutotextRuleConfig matchedRule)
		{
			MatchedRule = matchedRule;
		}
	}
}