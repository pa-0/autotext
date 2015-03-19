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
			Macros res = new Macros();
			MacrosConfigDefinition matchedConfig = null;
			string regex = null;

			foreach (MacrosConfigDefinition macrosConfig in macrosesConfig.MacrosDefinitions)
			{
				if (Regex.IsMatch(macrosText, macrosConfig.ExplicitParametersRegex))
				{
					matchedConfig = macrosConfig;
					regex = macrosConfig.ExplicitParametersRegex;
					break;
				}

				if (Regex.IsMatch(macrosText, macrosConfig.ImplicitParametersRegex))
				{
					matchedConfig = macrosConfig;
					regex = macrosConfig.ExplicitParametersRegex;
					break;
				}
			}

			res.MacrosName = matchedConfig.ShortName;

			MatchCollection macrosParameters = Regex.Matches(macrosText, matchedConfig.ExplicitParametersRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

			foreach (MacrosConfigParameter parameter in matchedConfig.MacrosParametrers)
			{
				res.Parameters.Add(new MacrosParameter(parameter.Name, macrosParameters[0].Groups[parameter.Name].Value));
			}

			return res;
		}
	}
}
