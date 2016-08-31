using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;

namespace AutoText.Constants
{
	public class ConfigConstants
	{
		static ConfigConstants()
		{
			if (ConfigHelper.IsInstalled)
			{
				AutotextRulesConfigFileFullPath = Path.Combine(ApplicationUserSettingsDir, CommonConstants.ApplicationName,
					AutotextRulesConfigFileName);
			}
			else
			{
				AutotextRulesConfigFileFullPath = Path.Combine(ConfigFolderFullPath, AutotextRulesConfigFileName);
			}
		}

		public const string IsInstalled = "isInstalled";

		public static readonly string ApplicationRootDir = Application.StartupPath;
		public static readonly string ApplicationUserSettingsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		public const string AutotextRulesConfigFileName = "AutotextRules.xml";
		public const string KeycodesConfigFileName = "Keycodes.xml";
		public const string CommonConfigurationFileName = "CommonConfiguration.xml";
		public const string ConfigFolderName = "Configuration";
		public static readonly string ConfigFolderFullPath = Path.Combine(ApplicationRootDir, ConfigFolderName);
		public static readonly string AutotextRulesConfigFileFullPath;
		public static readonly string KeycodesConfigFileFullPath = Path.Combine(ConfigFolderFullPath, KeycodesConfigFileName);
		public static readonly string CommonConfigurationFileFullPath = Path.Combine(ConfigFolderFullPath, CommonConfigurationFileName);
	}
}
