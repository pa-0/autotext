﻿/*This file is part of AutoText.

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
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	[Serializable]
	public enum SpecificProgramsListtype
	{
		Whitelist,
		Blacklist
	}

	[Serializable]
	public class AutotextRuleSpecificPrograms
	{
		[XmlAttribute("listType")]
		public SpecificProgramsListtype ProgramsListType { get; set; }
		[XmlAttribute("listEnabled")]
		public bool ListEnabled { get; set; }

		[XmlArray("programs")]
		[XmlArrayItem("item", typeof(AutotextRuleSpecificProgram))]
		public List<AutotextRuleSpecificProgram> Programs { get; set; }
	}
}
