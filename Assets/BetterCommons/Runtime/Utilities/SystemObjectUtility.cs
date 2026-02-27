using System;

namespace Better.Commons.Runtime.Utilities
{
	public struct SystemObjectUtility
	{
		public static bool CompareTypes(object a, object b)
		{
			if (a == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(a));
				return false;
			}

			if (b == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(b));
				return false;
			}

			var typeA = a.GetType();
			var typeB = b.GetType();
			var equal = typeA == typeB;

			return equal;
		}
	}
}