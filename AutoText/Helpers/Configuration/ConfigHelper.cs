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
		private static MacrosesConfiguration _macrosConfig;
		private static KeycodesConfiguration _keycodesConfig;
		private static List<AutotextRuleConfig> _autotextConfig;

		public static List<AutotextRuleConfig> GetAutotextRules()
		{
			if (_autotextConfig != null)
			{
				return _autotextConfig;
			}

			_autotextConfig = DeserailizeXml<AutotextRulesRoot>(@"AutotextRules.xml").AutotextRulesList;
			return _autotextConfig;

		}

		private static TRes DeserailizeXml<TRes>(string xmlFilePath)
		{
			Stream textReader = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read);

			try
			{
				XmlSerializer deserializer = new XmlSerializer(typeof(TRes));
				deserializer.UnknownAttribute += new XmlAttributeEventHandler(deserializer_UnknownAttribute);
				deserializer.UnknownElement += new XmlElementEventHandler(deserializer_UnknownElement);
				deserializer.UnknownNode += new XmlNodeEventHandler(deserializer_UnknownNode);
				deserializer.UnreferencedObject += new UnreferencedObjectEventHandler(deserializer_UnreferencedObject);
				return (TRes)deserializer.Deserialize(textReader);
			}
			finally
			{
				textReader.Close();
			}
		}

		static void deserializer_UnreferencedObject(object sender, UnreferencedObjectEventArgs e)
		{
			{ }
		}

		static void deserializer_UnknownNode(object sender, XmlNodeEventArgs e)
		{
			{ }
		}

		static void deserializer_UnknownElement(object sender, XmlElementEventArgs e)
		{
			{ }
		}

		static void deserializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
		{
			{ }
		}

		public static KeycodesConfiguration GetKecodesConfiguration()
		{
			if (_keycodesConfig != null)
			{
				return _keycodesConfig;
			}

			_keycodesConfig = DeserailizeXml<KeycodesConfiguration>(@"Keycodes.xml");
			return _keycodesConfig;

		}

		public static MacrosesConfiguration GetMacrosesConfiguration()
		{
			if (_macrosConfig != null)
			{
				return _macrosConfig;
			}

			_macrosConfig = DeserailizeXml<MacrosesConfiguration>(@"MacrosDefinitions.xml");
			return _macrosConfig;
		}

	}
}
