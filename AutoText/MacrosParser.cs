using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoText.Helpers.Configuration;
using System.Text.RegularExpressions;

namespace AutoText
{
	public class MacrosParameter
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public MacrosParameter(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}

	public class Macros
	{
		public string MacrosName { get; set; }
		public List<MacrosParameter> Parameters { get; set; }

		public Macros()
		{
			Parameters = new List<MacrosParameter>(20);
		}
	}

	public class MacrosParser
	{

		public static Macros Parse(string macrosText)
		{
			MacrosesConfiguration macrosesConfig = ConfigHelper.GetMacrosesConfiguration();
			//string macrosName = macrosText.

			throw new NotImplementedException();
		}
	}
}
