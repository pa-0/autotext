using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class ConfigHelper
	{
		public static List<AutotextRuleConfig> GetAutotextRules(string configPath)
		{
			AutotextRulesRoot rules = DeserailizeXml<AutotextRulesRoot>(configPath);
			return rules.AutotextRulesList;
		}

		private static TRes DeserailizeXml<TRes>(string xmlFilePath)
		{
			Stream textReader = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read);

			try
			{
				XmlSerializer deserializer = new XmlSerializer(typeof(TRes));
				return (TRes)deserializer.Deserialize(textReader);
			}
			finally
			{
				textReader.Close();
			}
		}

		public static Dictionary<string, int> GetMacrosCharacters(string configPath)
		{
			XDocument document = XDocument.Parse(File.ReadAllText(Path.GetFullPath(configPath)));
			Dictionary<string, int> macrosChars = document.Element("macrosCharacters").Elements("add").ToDictionary(p => p.Attribute("name").Value, g => int.Parse(g.Attribute("value").Value));
			return macrosChars;
		}

		public static MacrosesConfiguration GetMacrosDefinitions(string configPath)
		{
			MacrosesConfiguration macrosDefs = DeserailizeXml<MacrosesConfiguration>(configPath);
			return macrosDefs;
		}

	}
}
