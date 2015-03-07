using System.Configuration;

namespace AutoText.Helpers.Configuration
{
	public class AutotextRulesSection : ConfigurationSection
	{
		[ConfigurationProperty("autotextRules")]
		public AutotextRulesCollection Rules
		{
			get { return ((AutotextRulesCollection)(base["autotextRules"])); }
		}
	}
}