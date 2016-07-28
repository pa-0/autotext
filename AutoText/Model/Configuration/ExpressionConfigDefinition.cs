using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	public class ExpressionConfigDefinition
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlAttribute("shortName")]
		public string ShortName { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }
		[XmlElement("implicitParamsRegex")]
		public string ImplicitParametersRegex { get; set; }

		[XmlArray("parameters"), XmlArrayItem("parameter")]
		public List<ExpressionConfigParameter> ExpessionParametrers { get; set; }
	}
}
