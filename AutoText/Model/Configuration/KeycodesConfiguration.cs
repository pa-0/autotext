using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	[XmlRoot("configuration", IsNullable = false)]
	public class KeycodesConfiguration
	{
		[XmlArray("keycodes"), XmlArrayItem("keycode")]
		public List<KeycodeConfig> Keycodes{get;set;}
	}
}
