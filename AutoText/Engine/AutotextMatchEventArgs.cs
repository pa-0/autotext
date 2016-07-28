using System;
using AutoText.Model.Configuration;

namespace AutoText.Engine
{
	public class AutotextMatchEventArgs : EventArgs
	{
		public AutotextRuleConfig MatchedRule { get; private set; }
		public AutotextRuleTrigger Trigger { get; private set; }

		public AutotextMatchEventArgs(AutotextRuleConfig matchedRule, AutotextRuleTrigger trigger)
		{
			MatchedRule = matchedRule;
			Trigger = trigger;
		}
	}
}