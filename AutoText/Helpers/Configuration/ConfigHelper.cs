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
		public static List<AutotextRuleConfig> GetAutotextRules(string configPath)
		{
			/*
			XDocument document = XDocument.Parse(File.ReadAllText(Path.GetFullPath(configPath)));
			List<XElement> rules = document.Element("autotextRules").Elements("rule").ToList();
			List<AutotextRuleConfig> rulesList = rules.Select(g => new AutotextRuleConfig(new RuleAbbreviation(g.Element("abbreviation").Value, bool.Parse(g.Element("abbreviation").Attribute("caseSensitive").Value)), g.Element("phrase").Value, g.Element("description").Value,
				g.Elements("triggers").Count() == 0 ? new List<AutotextRuleTrigger>() : g.Element("triggers").Element("items").Elements("add").Select(p => new AutotextRuleTrigger(p.Value, bool.Parse(p.Attribute("caseSensitive").Value))).ToList())).ToList();
			return rulesList;
			 * */

			return null;
		}

		public static Dictionary<string, int> GetMacrosCharacters(string configPath)
		{
			XDocument document = XDocument.Parse(File.ReadAllText(Path.GetFullPath(configPath)));
			Dictionary<string, int> macrosChars = document.Element("macrosCharacters").Elements("add").ToDictionary(p => p.Attribute("name").Value, g => int.Parse(g.Attribute("value").Value));
			return macrosChars;
		}

	}
}
