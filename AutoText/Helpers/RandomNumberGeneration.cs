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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AutoText.Helpers
{
	public class RandomNumberGeneration
	{
		/// <summary>
		/// Returns a random long from min (inclusive) to max (exclusive)
		/// </summary>
		/// <param name="min">The inclusive minimum bound</param>
		/// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
		public static long RandomLong(long min, long max)
		{
			if (max <= min)
				throw new ArgumentOutOfRangeException("max", "max must be > min!");

			//Working with ulong so that modulo works correctly with values > long.MaxValue
			ulong uRange = (ulong)(max - min);

			//Prevent a modolo bias; see http://stackoverflow.com/a/10984975/238419
			//for more information.
			//In the worst case, the expected number of calls is 2 (though usually it's
			//much closer to 1) so this loop doesn't really hurt performance at all.
			ulong ulongRand;
			do
			{
				byte[] buf = new byte[8];

				using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
				{
					rng.GetBytes(buf);
				}

				ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
			} while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

			return (long)(ulongRand % uRange) + min;
		}

		/// <summary>
		/// Returns a random long from 0 (inclusive) to max (exclusive)
		/// </summary>
		/// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
		public static long RandomLong(long max)
		{
			return RandomLong(0, max);
		}

		/// <summary>
		/// Returns a random long over all possible values of long (except long.MaxValue, similar to
		/// random.Next())
		/// </summary>
		public static long RandomLong()
		{
			return RandomLong(long.MinValue, long.MaxValue);
		}
	}
}
