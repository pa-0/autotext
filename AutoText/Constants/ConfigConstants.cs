/*This file is part of AutoText.

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

using System;
using System.IO;
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
		public const string LogFileName = "AutoText.log";
		public const string KeycodesConfigFileName = "Keycodes.xml";
		public const string CommonConfigurationFileName = "CommonConfiguration.xml";
		public const string ConfigFolderName = "Configuration";
		public static readonly string ConfigFolderFullPath = Path.Combine(ApplicationRootDir, ConfigFolderName);
		public static readonly string AutotextRulesConfigFileFullPath;
		public static readonly string KeycodesConfigFileFullPath = Path.Combine(ConfigFolderFullPath, KeycodesConfigFileName);
		public static readonly string CommonConfigurationFileFullPath = Path.Combine(ConfigFolderFullPath, CommonConfigurationFileName);
		public static readonly string UserTempDirPath = Environment.GetEnvironmentVariable("TEMP");
		public static readonly string ApplicationTempDirPath = Path.Combine(UserTempDirPath, CommonConstants.ApplicationName);
		public static readonly string LogFileFullPath = Path.Combine(ApplicationTempDirPath, LogFileName);
	}
}
