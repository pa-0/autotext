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
		public string ExpressionNameSuffix { get; set; }
		[XmlElement("expressionParameterPrefix")]
		public string ExpressionParameterPrefix { get; set; }
		[XmlElement("expressionParameterAssignmentStr")]
		public string ExpressionParameterAssigmentString { get; set; }
		[XmlElement("shortcutRegex")]
		public string ShortcutRegexTemplate { get; set; }
		[XmlElement("nonPrintableTriggers")]
		public string NonPrintableTriggers { get; set; }
		[XmlArray("expressionDefinitions"), XmlArrayItem("expression")]
		public List<ExpressionConfigDefinition> ExpressionDefinitions { get; set; }

	}
}
