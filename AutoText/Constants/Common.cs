using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoText.Constants
{
	public static class Common
	{
		public const string NewPhraseDefaultDescription = "<description>";
		public const string NewPhraseDefaultAutotext = "<autotext{0}>";
		public const string NewPhraseDefaultAutotextRegex = @"^<autotext(\d+)?>$";
		public const string AutotextRulesConfigFileName = "AutotextRules.xml";
	}
}
