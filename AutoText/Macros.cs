using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoText.Helpers.Configuration;
using System.Text.RegularExpressions;

namespace AutoText
{
	public class Macros
	{
		public static List<Input> Evaluate(string macrosName, Dictionary<string,List<Input>> macrosParameters)
		{
			switch (macrosName.ToLower())
			{
				case "s":
				{
					StringBuilder resStr = new StringBuilder(1000);

					int repeatCount = Int32.Parse(string.Concat(macrosParameters["count"].Select(p => p.CharToInput)));
					string value = string.Concat(macrosParameters["text"].Select(p => p.CharToInput));

					for (int i = 0; i < repeatCount; i++)
					{
						resStr.Append(value);
					}

					List<Input> res = Input.FromString(resStr);
					return res;
					break;
				}
					
				default:
				{
					throw new ArgumentOutOfRangeException("macrosName");
				}
			}

			throw new NotImplementedException();
		}
	}
}
