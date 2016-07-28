using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	[XmlRoot("autotextRules", IsNullable = false)]
	public class AutotextRulesRoot
	{
		[XmlArray("rules"), XmlArrayItem("rule")]
		public List<AutotextRuleConfig> AutotextRulesList { get; set; }
	}
}
