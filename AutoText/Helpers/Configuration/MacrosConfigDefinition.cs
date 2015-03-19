using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class MacrosConfigDefinition
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlAttribute("shortName")]
		public string ShortName { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }
		[XmlElement("explicitParamsRegex")]
		public string ExplicitParametersRegex { get; set; }
		[XmlElement("implicitParamsRegex")]
		public string ImplicitParametersRegex { get; set; }

		[XmlArray("parameters"), XmlArrayItem("parameter")]
		public List<MacrosConfigParameter> MacrosParametrers { get; set; }
	}
}
