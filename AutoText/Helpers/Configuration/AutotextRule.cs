namespace AutoText.Helpers.Configuration
{
	public class AutotextRule
	{
		public string Abbreviation { get; private set; }
		public string Phrase { get; private set; }
		public string Description { get; private set; }

		public AutotextRule(string trigger, string phrase, string description)
		{
			Abbreviation = trigger;
			Phrase = phrase;
			Description = description;
		}
	}
}