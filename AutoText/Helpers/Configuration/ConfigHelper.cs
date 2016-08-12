/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

AutoText is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */


using AutoText.Model.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AutoText.Helpers.Configuration
{
	public class ConfigHelper
	{
		private static CommonConfiguration _commonConfiguration;
		private static KeycodesConfiguration _keycodesConfig;
		private static List<AutotextRuleConfiguration> _autotextConfig;

		public static List<AutotextRuleConfiguration> GetAutotextRulesConfiguration()
		{
			if (!File.Exists(Constants.Common.AutotextRulesConfigFileFullPath))
			{
				SaveAutotextRulesConfiguration(new List<AutotextRuleConfiguration>());
			}

			_autotextConfig = DeserailizeXml<AutotextRulesRoot>(Constants.Common.AutotextRulesConfigFileFullPath).AutotextRulesList;

			foreach (AutotextRuleConfiguration config in _autotextConfig)
			{
				config.Phrase = config.Phrase.Replace("\n", "\r\n");
			}


			foreach (AutotextRuleConfiguration config in _autotextConfig)
			{
				config.PhraseCompiled = config.PhraseCompiled.Replace("\n", "\r\n");
			}

			return _autotextConfig;
		}


		public static void SaveAutotextRulesConfiguration(List<AutotextRuleConfiguration> configuration)
		{
			AutotextRulesRoot root = new AutotextRulesRoot { AutotextRulesList = configuration };
			XmlSerializer serializer = new XmlSerializer(typeof(AutotextRulesRoot));

			using (TextWriter writer = new StreamWriter(Constants.Common.AutotextRulesConfigFileFullPath))
			{
				serializer.Serialize(writer, root);
			}
		}

		public static TRes DeserailizeXml<TRes>(string xmlFilePath)
		{
			using (Stream textReader = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
			{
				return DeserailizeXml<TRes>(textReader);
			}
		}

		public static TRes DeserailizeXmlFromString<TRes>(string xmlString)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				using (StreamWriter sw = new StreamWriter(ms))
				{
					sw.Write(xmlString);
					sw.Flush();
					ms.Position = 0;
					return DeserailizeXml<TRes>(ms);
				}
			}
		}

		public static TRes DeserailizeXml<TRes>(Stream stream)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(TRes));
			deserializer.UnknownAttribute += new XmlAttributeEventHandler(deserializer_UnknownAttribute);
			deserializer.UnknownElement += new XmlElementEventHandler(deserializer_UnknownElement);
			deserializer.UnknownNode += new XmlNodeEventHandler(deserializer_UnknownNode);
			deserializer.UnreferencedObject += new UnreferencedObjectEventHandler(deserializer_UnreferencedObject);
			return (TRes)deserializer.Deserialize(stream);
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

		public static bool IsKeycodesConfigurationOk()
		{
			return File.Exists(Constants.Common.KeycodesConfigFileFullPath);
		}

		public static bool IsCommonConfigurationOk()
		{
			return File.Exists(Constants.Common.CommonConfigurationFileFullPath);
		}

		public static CommonConfiguration GetCommonConfiguration()
		{
			if (_commonConfiguration != null)
			{
				return _commonConfiguration;
			}

			_commonConfiguration = DeserailizeXml<CommonConfiguration>(Constants.Common.CommonConfigurationFileFullPath);
			return _commonConfiguration;
		}
	}
}
