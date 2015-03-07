using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AutoText.Helpers.Configuration
{
	public class ConfigHelper
	{
		public static Dictionary<string, AutotextRule> GetAutotextRules(string configPath)
		{
			XDocument document = XDocument.Parse(File.ReadAllText(Path.GetFullPath(configPath)));
			IEnumerable<XElement> rules = document.Elements("rule");
			Dictionary<string, AutotextRule> rulesDict = rules.ToDictionary(p => p.Element("trigger").Value, g => new AutotextRule(g.Element("trigger").Value, g.Element("phrase").Value, g.Element("description").Value));

			return rulesDict;
		}
	}
}
