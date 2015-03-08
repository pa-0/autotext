namespace AutoText.Helpers.Configuration
{
	public class AutotextRuleTrigger
	{
		public string Value { get; private set; }
		public bool CaseSensitive { get; private set; }

		public AutotextRuleTrigger(string value, bool caseSensitive)
		{
			Value = value;
			CaseSensitive = caseSensitive;
		}
	}
}