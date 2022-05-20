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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using AutoText.Engine;

namespace AutoText.Helpers.Extensions
{
	public static class Common
	{
		public static string ConcatToString(this List<AutotextInput> input)
		{
			return new string(input.Select(p => p.CharToInput).ToArray());
		}

		public static string EscapeSpecialExpressionChars(this string str)
		{
			if (str == null) throw new ArgumentNullException("str");

			StringBuilder sb = new StringBuilder(str);

			for (int i = 0; i < sb.Length; i++)
			{
				if (sb[i] == '{')
				{
					sb = sb.Remove(i, 1);
					sb = sb.Insert(i, "{{}");
					i += 2;
					continue;
				}

				if (sb[i] == '}')
				{
					sb = sb.Remove(i, 1);
					sb = sb.Insert(i, "{}}");
					i += 2;
					continue;
				}

				if (sb[i] == '[')
				{
					sb = sb.Remove(i, 1);
					sb = sb.Insert(i, "{[}");
					i += 2;
					continue;
				}

				if (sb[i] == ']')
				{
					sb = sb.Remove(i, 1);
					sb = sb.Insert(i, "{]}");
					i += 2;
					continue;
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Perform a deep Copy of the object.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T Clone<T>(this T source)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", "source");
			}

			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			using (stream)
			{
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
