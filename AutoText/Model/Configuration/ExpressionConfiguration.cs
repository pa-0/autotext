using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	[XmlRoot("expressionsConfiguration", IsNullable = false)]
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
