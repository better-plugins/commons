using System;
using System.Collections.Generic;

namespace Better.Commons.Runtime.Extensions
{
	public static class IndexExtensions
	{
		public static int GetOffset<T>(this Index self, IReadOnlyCollection<T> readOnlyCollection)
		{
			if (readOnlyCollection == null)
			{
				throw new ArgumentNullException(nameof(readOnlyCollection));
			}

			var offset = self.GetOffset(readOnlyCollection.Count);
			return offset;
		}

		public static int GetOffset<T>(this Index self, ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			var offset = self.GetOffset(collection.Count);
			return offset;
		}
	}
}