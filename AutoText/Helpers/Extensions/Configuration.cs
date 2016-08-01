using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AutoText.Helpers.Extensions
{
	public static class Configuration
	{
		public static string ToXmlString(this XDocument xmlDocument)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				using (StreamWriter sw = new StreamWriter(ms))
				{
					xmlDocument.Save(sw);
					sw.Flush();
					ms.Position = 0;

					using (StreamReader sr = new StreamReader(ms))
					{
						return sr.ReadToEnd();
					}
				}
			}
		}
	}
}
