using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class ParameterDataType
	{
		[XmlAttribute("name")]
		public string TypeName { get; set; }
	}
}
