using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	[XmlRootAttribute("autotextRules", IsNullable = false)]
	public class AutotextRulesRoot
	{
		[XmlArray("rules"), XmlArrayItem("rule")]
		public List<AutotextRuleConfig> AutotextRulesList { get; set; }
	}
}
