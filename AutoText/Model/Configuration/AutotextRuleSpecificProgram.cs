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
using System.Xml;
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	[Serializable]
	public enum TitleCondition
	{
		Exact,
		StartsWith,
		EndsWith,
		Contains,
		Any
	}

	[Serializable]
	public class AutotextRuleSpecificProgram
	{
		[XmlElement("programId")]
		public string ProgramModuleName { get; set; }
		[XmlElement("programDescription")]
		public string ProgramDescription { get; set; }
		[XmlAttribute("titleMatchCondition")]
		public TitleCondition TitelMatchCondition { get; set; }
		[XmlElement("titleText")]
		public string TitleText { get; set; }

		public string TitelMatchConditionFormatted
		{
			get
			{
				switch (TitelMatchCondition)
				{
					case TitleCondition.Exact:
						return "with window title that exactly matches";
						break;
					case TitleCondition.StartsWith:
						return "with window title that starts with";
						break;
					case TitleCondition.EndsWith:
						return "with window title that ends with";
						break;
					case TitleCondition.Contains:
						return "with window title that contain";
						break;
					case TitleCondition.Any:
						return "with any window title";
						break;
					default:
						throw new InvalidOperationException("Enum value not recognized");
						break;
				}
			}
		}

		public string ProgramIdFormatted
		{
			get
			{
				if (string.IsNullOrEmpty(ProgramDescription) || string.IsNullOrWhiteSpace(ProgramDescription))
				{
					return ProgramModuleName;
				}
				else
				{
					return ProgramDescription + " (" + ProgramModuleName + ")";
				}
			}
		}
	}
}
