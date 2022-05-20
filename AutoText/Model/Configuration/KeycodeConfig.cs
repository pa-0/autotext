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


using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoText.Model.Configuration
{
	public enum KeyRelation
	{
		Native,
		Sender,
		Display
	}

	public class KeycodeConfigName
	{
		[XmlAttribute("value")]
		public string Value { get; set; }
		[XmlAttribute("rel")]
		public KeyRelation KeyRelation { get; set; }
		[XmlAttribute("triggerListVisible")]
		public bool TriggerListVisible { get; set; }
	}

	public class KeycodeConfig
	{
		[XmlAttribute("virtualKeyCode")]
		public int VirtualKeyCode { get; set; }
		[XmlAttribute("toggleable")]
		public bool Toggleable { get; set; }
		[XmlAttribute("canOn")]
		public bool CanOn { get; set; }
		[XmlAttribute("canOff")]
		public bool CanOff { get; set; }
		[XmlAttribute("shortcut")]
		public string Shortcut { get; set; }
		[XmlArray("names"), XmlArrayItem("name")]
		public List<KeycodeConfigName> Names { get; set; }
	}
}
