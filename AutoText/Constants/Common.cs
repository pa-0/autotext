using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoText.Constants
{
	public static class Common
	{
		public static readonly string ApplicationRootDir = Application.StartupPath;
		public const string NewPhraseDefaultDescription = "<description>";
		public const string NewPhraseDefaultAutotext = "<autotext{0}>";
		public const string NewPhraseDefaultAutotextRegex = @"^<autotext(\d+)?>$";
		public const string AutotextRulesConfigFileName = "AutotextRules.xml";
		public const string KeycodesConfigFileName = "Keycodes.xml";
		public const string ExpressionDefinitionsConfigFileName = "ExpressionDefinitions.xml";
		public const string ConfigFolderName = "Configuration";
		public static readonly string ConfigFolderFullPath = Path.Combine(ApplicationRootDir, ConfigFolderName);
		public static readonly string AutotextRulesConfigFileFullPath = Path.Combine(ConfigFolderFullPath, AutotextRulesConfigFileName);
		public static readonly string KeycodesConfigFileFullPath = Path.Combine(ConfigFolderFullPath, KeycodesConfigFileName);
		public static readonly string ExpressionDefinitionsConfigFileFullPath = Path.Combine(ConfigFolderFullPath, ExpressionDefinitionsConfigFileName);

	}
}
