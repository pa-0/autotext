using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	public class ExpressionConfigParameter
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
	}
}
