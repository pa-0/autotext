using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace AutoText.Helpers.Configuration
{
	public class RuleElement : ConfigurationElement
	{
		[ConfigurationProperty("trigger", DefaultValue = "",  IsRequired = true)]
		public string Trigger
		{
			get { return (string)this["trigger"]; }
			set { this["trigger"] = value; }
		}
	}
}
