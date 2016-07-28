using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	public class KeycodeConfigName
	{
		[XmlAttribute("value")]
		public string Value { get; set; }
	}

	public class KeycodeConfig
	{
		//[XmlAttribute("name")]
		//public string Name { get; set; }
		[XmlAttribute("value")]
		public int Value { get; set; }
		[XmlAttribute("toggleable")]
		public bool Toggleable { get; set; }
		[XmlAttribute("canOn")]
		public bool CanOn { get; set; }
		[XmlAttribute("canOff")]
		public bool CanOff { get; set; }
		[XmlAttribute("shortcut")]
		public string Shortcut { get; set; }
		[XmlArray("names"),XmlArrayItem("name")]
		public List<KeycodeConfigName> Names { get; set; }
	}
}
