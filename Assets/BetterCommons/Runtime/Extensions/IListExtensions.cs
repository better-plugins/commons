using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class IListExtensions
	{
		public static bool TryIndexOf<T>(this List<T> self, T item, out int index)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				index = -1;
				return false;
			}

			index = self.IndexOf(item);
			var found = index >= 0;
			return found;
		}
	}
}