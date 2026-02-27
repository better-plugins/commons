using System;
using System.Reflection;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class ParameterInfoExtensions
	{
		public static object GetDefaultValue(this ParameterInfo self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}
            
			if (self.HasDefaultValue)
			{
				return self.DefaultValue;
			}

			var defaultValue = self.ParameterType.GetDefault();
			return defaultValue;
		}
	}
}