using AutoText.Model.Configuration;

namespace AutoText.Engine
{
	public class AutotextRuleMatchParameters
	{
		public AutotextRuleConfig AutotextRuleConfig { get; set; }
		public AutotextRuleTrigger MatchTrigger { get; set; }

		public AutotextRuleMatchParameters(AutotextRuleConfig autotextRuleConfig, AutotextRuleTrigger matchTrigger)
		{
			AutotextRuleConfig = autotextRuleConfig;
			MatchTrigger = matchTrigger;
		}
	}
}
