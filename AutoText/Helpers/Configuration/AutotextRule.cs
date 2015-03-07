namespace AutoText.Helpers.Configuration
{
	public class AutotextRule
	{
		public string Trigger { get; private set; }
		public string Phrase { get; private set; }
		public string Description { get; private set; }

		public AutotextRule(string trigger, string phrase, string description)
		{
			Trigger = trigger;
			Phrase = phrase;
			Description = description;
		}
	}
}