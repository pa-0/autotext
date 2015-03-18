using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	[XmlRootAttribute("macrosesConfiguration", IsNullable = false)]
	public class MacrosesConfiguration
	{
		[XmlElement("macrosNameSuffixStr")]
		public string MacrosNameSuffix { get; set; }
		[XmlElement("macrosParameterPrefix")]
		public string MacrosParameterPrefix { get; set; }
		[XmlElement("macrosParameterAssignmentStr")]
		public string MacrosParameterAssigmentString { get; set; }

		[XmlArray("macrosDefinitions"), XmlArrayItem("macros")]
		public List<MacrosConfigDefinition> MacrosDefinitions { get; set; }

	}
}
