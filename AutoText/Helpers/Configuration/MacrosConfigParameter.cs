using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class MacrosConfigParameter
	{
		[XmlAttribute("allowOmitName")]
		public bool AllowOmitName { get; set; }
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlArray("allowedDataTypes"), XmlArrayItem("dataType")]
		public List<ParameterDataType> AllowedDataTypes { get; set; }

	}
}
