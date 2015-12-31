using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
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
