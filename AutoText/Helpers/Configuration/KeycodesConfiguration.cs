using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	[XmlRootAttribute("configuration", IsNullable = false)]
	public class KeycodesConfiguration
	{
		[XmlArray("keycodes"), XmlArrayItem("keycode")]
		public List<KeycodeConfig> Keycodes{get;set;}
	}
}
