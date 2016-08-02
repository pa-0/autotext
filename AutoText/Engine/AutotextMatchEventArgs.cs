using System;
using AutoText.Model.Configuration;

namespace AutoText.Engine
{
	public class AutotextMatchEventArgs : EventArgs
	{
		public AutotextRuleConfiguration MatchedRule { get; private set; }
		public AutotextRuleTrigger Trigger { get; private set; }

		public AutotextMatchEventArgs(AutotextRuleConfiguration matchedRule, AutotextRuleTrigger trigger)
		{
			MatchedRule = matchedRule;
			Trigger = trigger;
		}
	}
}