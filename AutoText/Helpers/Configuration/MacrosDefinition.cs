using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class MacrosDefinition
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlAttribute("shortName")]
		public string ShortName { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }
		[XmlAttribute("allowOmitShortName")]
		public bool AllowOmitShortName { get; set; }

		[XmlArray("parameters"), XmlArrayItem("parameter")]
		public List<MacrosParameter> MacrosParametrers { get; set; }
	}
}
