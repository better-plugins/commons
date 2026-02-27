using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Interfaces;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class ICloneableExtensions
	{
		public static T[] CloneAll<T>(this IEnumerable<ICloneable<T>> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Array.Empty<T>();
			}

			var clones = self.Select(i => i.Clone())
				.ToArray();

			return clones;
		}
	}
}