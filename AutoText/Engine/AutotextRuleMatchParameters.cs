using AutoText.Model.Configuration;

namespace AutoText.Engine
{
	public class AutotextRuleMatchParameters
	{
		public AutotextRuleConfiguration AutotextRuleConfiguration { get; set; }
		public AutotextRuleTrigger MatchTrigger { get; set; }

		public AutotextRuleMatchParameters(AutotextRuleConfiguration autotextRuleConfiguration, AutotextRuleTrigger matchTrigger)
		{
			AutotextRuleConfiguration = autotextRuleConfiguration;
			MatchTrigger = matchTrigger;
		}
	}
}
