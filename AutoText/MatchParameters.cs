using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public class MatchParameters
	{
		public AutotextRuleConfig AutotextRuleConfig { get; set; }
		public AutotextRuleTrigger MatchTrigger { get; set; }

		public MatchParameters(AutotextRuleConfig autotextRuleConfig, AutotextRuleTrigger matchTrigger)
		{
			AutotextRuleConfig = autotextRuleConfig;
			MatchTrigger = matchTrigger;
		}
	}
}
