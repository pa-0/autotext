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
		public static List<AutotextRule> GetAutotextRules(string configPath)
		{
			XDocument document = XDocument.Parse(File.ReadAllText(Path.GetFullPath(configPath)));
			List<XElement> rules = document.Element("autotextRules").Elements("rule").ToList();
			List<AutotextRule> rulesList = rules.Select(g => new AutotextRule(g.Element("abbreviation").Value, g.Element("phrase").Value, g.Element("description").Value)).ToList();
			return rulesList;
		}
	}
}
