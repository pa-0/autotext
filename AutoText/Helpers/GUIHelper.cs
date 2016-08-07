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
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace AutoText.Helpers
{
	public static class GUIHelper
	{
		private delegate void SetPropertyThreadSafeDelegate<TResult>(Control @this, Expression<Func<TResult>> property, TResult value);

		public static void SetPropertyThreadSafe<TResult>(this Control @this, Expression<Func<TResult>> property, TResult value)
		{
			PropertyInfo propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;

			if (propertyInfo == null || 
				!@this.GetType().IsSubclassOf(propertyInfo.ReflectedType) ||
				@this.GetType().GetProperty(propertyInfo.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).PropertyType != propertyInfo.PropertyType)
			{
				throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
			}

			if (@this.InvokeRequired)
			{
				@this.Invoke(new SetPropertyThreadSafeDelegate<TResult>(SetPropertyThreadSafe), @this, property, value);
			}
			else
			{
				@this.GetType().InvokeMember( propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] { value });
			}
		}


		public static void CenterTo(this Control @this, Control centerToControl)
		{
			@this.Location = new Point(centerToControl.Location.X + (centerToControl.Width - @this.Width) / 2, centerToControl.Location.Y + (centerToControl.Height - @this.Height) / 2);
		}
	}
}
