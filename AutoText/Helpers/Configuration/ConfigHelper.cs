﻿/*This file is part of AutoText.

Copyright © 2022 Alexander Litvinov

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


using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using AutoText.Constants;
using AutoText.Model.Configuration;

namespace AutoText.Helpers.Configuration
{
	public class ConfigHelper
	{
		private static CommonConfiguration _commonConfiguration;
		private static KeycodesConfiguration _keycodesConfig;
		private static List<AutotextRuleConfiguration> _autotextConfig;

		public static List<AutotextRuleConfiguration> GetAutotextRulesConfiguration()
		{
			if (!File.Exists(ConfigConstants.AutotextRulesConfigFileFullPath))
			{
				SaveAutotextRulesConfiguration(new List<AutotextRuleConfiguration>());
			}

			_autotextConfig = DeserailizeXml<AutotextRulesRoot>(ConfigConstants.AutotextRulesConfigFileFullPath).AutotextRulesList;

			foreach (AutotextRuleConfiguration config in _autotextConfig)
			{
				config.Phrase = config.Phrase.Replace("\n", "\r\n");
			}


			return _autotextConfig;
		}


		public static void SaveAutotextRulesConfiguration(List<AutotextRuleConfiguration> configuration)
		{
			AutotextRulesRoot root = new AutotextRulesRoot { AutotextRulesList = configuration };
			XmlSerializer serializer = new XmlSerializer(typeof(AutotextRulesRoot));

			using (TextWriter writer = new StreamWriter(Constants.ConfigConstants.AutotextRulesConfigFileFullPath))
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
			StreamReader sr = new StreamReader(stream);
			XmlReader xr = XmlReader.Create(sr);
			XmlSerializer deserializer = new XmlSerializer(typeof(TRes));
			deserializer.UnknownAttribute += new XmlAttributeEventHandler(deserializer_UnknownAttribute);
			deserializer.UnknownElement += new XmlElementEventHandler(deserializer_UnknownElement);
			deserializer.UnknownNode += new XmlNodeEventHandler(deserializer_UnknownNode);
			deserializer.UnreferencedObject += new UnreferencedObjectEventHandler(deserializer_UnreferencedObject);
			return (TRes)deserializer.Deserialize(xr);
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

			_keycodesConfig = DeserailizeXml<KeycodesConfiguration>(Constants.ConfigConstants.KeycodesConfigFileFullPath);
			return _keycodesConfig;

		}

		public static bool IsKeycodesConfigurationOk()
		{
			return File.Exists(Constants.ConfigConstants.KeycodesConfigFileFullPath);
		}

		public static bool IsCommonConfigurationOk()
		{
			return File.Exists(Constants.ConfigConstants.CommonConfigurationFileFullPath);
		}

		public static CommonConfiguration GetCommonConfiguration()
		{
			if (_commonConfiguration != null)
			{
				return _commonConfiguration;
			}

			_commonConfiguration = DeserailizeXml<CommonConfiguration>(Constants.ConfigConstants.CommonConfigurationFileFullPath);
			return _commonConfiguration;
		}

		public static void SaveCommonConfiguration()
		{
			if (_commonConfiguration != null)
			{
				XmlSerializer serializer = new XmlSerializer(typeof(CommonConfiguration));

				using (TextWriter writer = new StreamWriter(Constants.ConfigConstants.CommonConfigurationFileFullPath))
				{
					serializer.Serialize(writer, _commonConfiguration);
				}
			}
		}

		public static List<string> GetNativeKeyByDisplayKey(string diaplayKey)
		{
			return GetKeycodesConfiguration().Keycodes.Single(p => p.Names.Any(g => g.KeyRelation == KeyRelation.Display && g.Value == diaplayKey)).
					   Names.Where(g => g.KeyRelation == KeyRelation.Native).Select(p => p.Value).ToList();
		}

		public static string GetDisplayKeyByNativeKey(string nativeKey)
		{
			return GetKeycodesConfiguration().Keycodes.Single(p => p.Names.Any(g => g.KeyRelation == KeyRelation.Native && g.Value == nativeKey)).
					   Names.Single(g => g.KeyRelation == KeyRelation.Display).Value;
		}

		public static List<string> GetAllDisplayKeys()
		{
			return GetKeycodesConfiguration().Keycodes.Where(p => p.Names.Any(g => g.KeyRelation == KeyRelation.Display)).Select(p => p.Names.Single(g => g.KeyRelation == KeyRelation.Display).Value).ToList();
		}

		public static List<string> GetDisplayKeysTriggerListVisible()
		{
			return GetKeycodesConfiguration().Keycodes.
				Where(p => p.Names.Any(g => g.KeyRelation == KeyRelation.Display && g.TriggerListVisible)).
				Select(p => p.Names.Single(g => g.KeyRelation == KeyRelation.Display).Value).ToList();
		}

		public static string GetSenderKeyByNativeKey(string nativeKey)
		{
			return GetKeycodesConfiguration().Keycodes.Single(p => p.Names.Any(g => g.KeyRelation == KeyRelation.Native && g.Value == nativeKey)).
					   Names.Single(g => g.KeyRelation == KeyRelation.Sender).Value;
		}

		public static readonly bool IsInstalled = bool.Parse(ConfigurationManager.AppSettings[ConfigConstants.IsInstalled]);
	}
}
