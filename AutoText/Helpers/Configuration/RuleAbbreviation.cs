namespace AutoText.Helpers.Configuration
{
	public class RuleAbbreviation
	{
		public string AbbreviationText { get; private set; }
		public bool CaseSensitive { get; private set; }

		public RuleAbbreviation(string abbreviation, bool caseSensitive)
		{
			AbbreviationText = abbreviation;
			CaseSensitive = caseSensitive;
		}
	}
}