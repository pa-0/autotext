using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	class PhraseCompiler
	{
		public static string Compile(string phrase)
		{
			return phrase.Replace("\r\n", "{Enter}");

//			string[] splitRegexes = ConfigHelper.GetExpressionsConfiguration().
		}
	}
}
