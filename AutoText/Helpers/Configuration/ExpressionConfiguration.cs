using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	[XmlRootAttribute("expressionsConfiguration", IsNullable = false)]
	public class ExpressionConfiguration
	{
		[XmlElement("expressionNameSuffixStr")]
		public string MacrosNameSuffix { get; set; }
		[XmlElement("expressionParameterPrefix")]
		public string MacrosParameterPrefix { get; set; }
		[XmlElement("expressionParameterAssignmentStr")]
		public string MacrosParameterAssigmentString { get; set; }

		[XmlArray("expressionDefinitions"), XmlArrayItem("expression")]
		public List<ExpressionConfigDefinition> MacrosDefinitions { get; set; }

	}
}
