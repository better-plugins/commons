using System;
using Better.Commons.Runtime.Extensions;

namespace Better.Commons.Runtime.Utilities
{
	public static class TypeUtility
	{
		public static bool TryGetType(string name, out Type type)
		{
			if (name.IsNullOrEmpty())
			{
				type = null;
				return false;
			}

			type = Type.GetType(name, false);
			var found = type != null;
			return found;
		}
	}
}