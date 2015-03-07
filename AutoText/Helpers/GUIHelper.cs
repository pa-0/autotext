using System;
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
	}
}
