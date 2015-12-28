using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AutoText.Helpers.Extensions
{
	public static class Common
	{
		public static string ConcatToString(this List<AutotextInput> input)
		{
			return string.Concat(input.Select(p => p.CharToInput));
		}
		
	}

}
