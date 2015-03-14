using System;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class AutotextRuleTrigger
	{
		[XmlElement("value")]
		public string Value { get;  set; }
		[XmlAttribute("caseSensitive")]
		public bool CaseSensitive { get; set; }
	}
}