using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class ArrayExtensions
	{
		public static IEnumerable<TElement> ToEnumerable<TElement>(this Array self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Enumerable.Empty<TElement>();
			}

			var list = new List<TElement>(self.Length);

			for (var i = 0; i < self.Length; i++)
			{
				var segment = self.GetValue(i);

				if (segment is TElement element)
				{
					list.Add(element);
					continue;
				}

				var message = $"{nameof(segment)} cannot be cast to {nameof(TElement)}, was skipped";
				Debug.LogWarning(message);
			}

			return list;
		}

		public static void ReplaceElement<T>(this T[] self, int index, T value)
		{
			if (self.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (index < 0)
			{
				DebugUtility.LogException<ArgumentOutOfRangeException>(nameof(index));
				return;
			}

			if (index >= self.Length)
			{
				DebugUtility.LogException<ArgumentOutOfRangeException>(nameof(index));
				return;
			}

			self[index] = value;
		}
	}
}