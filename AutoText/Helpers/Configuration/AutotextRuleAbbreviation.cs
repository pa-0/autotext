using System;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public enum Abbriviationtype
	{
		Text,
		Regex
	}

	public class AutotextRuleAbbreviation
	{
		[XmlElement("value")]
		public string AbbreviationText { get;  set; }

		[XmlAttribute("caseSensitive")]
		public bool CaseSensitive { get;  set; }

		[XmlAttribute("type")]
		public Abbriviationtype Type { get; set; }

		[XmlAttribute("lastCharsCount")]
		public int LastCharsCount { get; set; }

	}
}