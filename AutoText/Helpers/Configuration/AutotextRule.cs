using System.Collections.Generic;

namespace AutoText.Helpers.Configuration
{
	public class AutotextRule
	{
		public RuleAbbreviation Abbreviation { get; private set; }
		public string Phrase { get; private set; }
		public string Description { get; private set; }
		public List<AutotextRuleTrigger> Triggers { get; private set; }

		public AutotextRule(RuleAbbreviation abbreviation, string phrase, string description, List<AutotextRuleTrigger> triggers)
		{
			Abbreviation = abbreviation;
			Phrase = phrase;
			Description = description;
			Triggers = triggers;
		}
	}
}