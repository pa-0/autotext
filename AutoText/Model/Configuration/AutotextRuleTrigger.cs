using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	public class AutotextRuleTrigger
	{
		[XmlElement("value")]
		public string Value { get;  set; }
		[XmlAttribute("caseSensitive")]
		public bool CaseSensitive { get; set; }
	}
}