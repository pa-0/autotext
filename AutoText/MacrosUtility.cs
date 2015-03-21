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
					regex = macrosConfig.ImplicitParametersRegex;
					break;
				}
			}

			res.MacrosName = matchedConfig.ShortName;

			MatchCollection macrosParameters = Regex.Matches(macrosText, regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

			foreach (MacrosConfigParameter parameter in matchedConfig.MacrosParametrers)
			{
				res.Parameters.Add(new MacrosParameter(parameter.Name, macrosParameters[0].Groups[parameter.Name].Value));
			}

			return res;
		}

		public List<Input> GetInput()
		{
			switch (MacrosName.ToLower())
			{
				case "s":
				{
					StringBuilder resStr = new StringBuilder(1000);

					int repeatCount = Int32.Parse(Parameters.Single(p => p.Name == "count").Value);
					string value = Parameters.Single(p => p.Name == "text").Value;

					for (int i = 0; i < repeatCount; i++)
					{
						resStr.Append(value);
					}

					List<Input> res = Input.FromString(resStr);
					return res;
					break;
				}
					
				default:
				{
					break;
				}
			}

			throw new NotImplementedException();
		}
	}
}
