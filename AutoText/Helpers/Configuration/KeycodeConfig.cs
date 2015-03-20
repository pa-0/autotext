﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class KeycodeConfig
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlAttribute("value")]
		public int Value { get; set; }
		[XmlAttribute("toggleable")]
		public bool Toggleable { get; set; }
		[XmlAttribute("canOn")]
		public bool CanOn { get; set; }
		[XmlAttribute("canOff")]
		public bool CanOff { get; set; }
	}
}