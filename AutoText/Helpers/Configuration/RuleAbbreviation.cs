using System;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class RuleAbbreviation
	{
		[XmlElement("value")]
		public string AbbreviationText { get;  set; }

		[XmlAttribute("caseSensitive")]
		public bool CaseSensitive { get;  set; }
	}
}