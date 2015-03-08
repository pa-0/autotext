using System;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public class AutotextMatchEventArgs : EventArgs
	{
		public AutotextRule MatchedRule { get; private set; }

		public AutotextMatchEventArgs(AutotextRule matchedRule)
		{
			MatchedRule = matchedRule;
		}
	}
}