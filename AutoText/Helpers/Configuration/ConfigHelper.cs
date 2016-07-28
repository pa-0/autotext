using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoText.Model.Configuration;

namespace AutoText.Helpers.Configuration
{
	public class ConfigHelper
	{
		private static ExpressionConfiguration _expressionConfiguration;
		private static KeycodesConfiguration _keycodesConfig;
		private static List<AutotextRuleConfig> _autotextConfig;

		public static List<AutotextRuleConfig> GetAutotextRules()
		{
			_autotextConfig = DeserailizeXml<AutotextRulesRoot>(Constants.Common.AutotextRulesConfigFileFullPath).AutotextRulesList;

			foreach (AutotextRuleConfig config in _autotextConfig)
			{
				config.Phrase = config.Phrase.Replace("\n", "\r\n");
			}

			foreach (AutotextRuleConfig config in _autotextConfig)
			{
				config.PhraseCompiled = config.PhraseCompiled.Replace("\n", "\r\n");
			}

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

		public static KeycodesConfiguration GetKeycodesConfiguration()
		{
			if (_keycodesConfig != null)
			{
				return _keycodesConfig;
			}

			_keycodesConfig = DeserailizeXml<KeycodesConfiguration>(Constants.Common.KeycodesConfigFileFullPath);
			return _keycodesConfig;

		}

		public static ExpressionConfiguration GetExpressionsConfiguration()
		{
			if (_expressionConfiguration != null)
			{
				return _expressionConfiguration;
			}

			_expressionConfiguration = DeserailizeXml<ExpressionConfiguration>(Constants.Common.ExpressionDefinitionsConfigFileFullPath);
			return _expressionConfiguration;
		}
	}
}
