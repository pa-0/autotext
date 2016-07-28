using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	public enum MacrosMode
	{
		Execute,
		Skip
	}

	public class AutotextRuleMacrosMode
	{
		[XmlAttribute("mode")]
		public MacrosMode Mode { get; set; }
	}
}
