/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

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
using System.Text;
using System.Windows.Forms;

namespace AutoText.Constants
{
	public static class CommonConstants
	{
		public const string ApplicationName = "AutoText";
        public const string ApplicationVersion = "1.5.1";
		public const string ApplicationAuthorCopy = "Copyright © {0} Alexander Litvinov";
        public const string NewPhraseDefaultDescription = "<description>";
		public const string NewPhraseDefaultAutotext = "<autotext{0}>";
		public const string NewPhraseDefaultAutotextRegex = @"^<autotext(\d+)?>$";
		public const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
		public const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string Digits = "1234567890";
		public const string SpecialChars = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
	}
}
