using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class IReadOnlyDictionaryExtensions
	{
		public static bool TryGetKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TValue value, out TKey key)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				key = default;
				return false;
			}

			foreach (var keyValuePair in self)
			{
				if (keyValuePair.Value.Equals(value))
				{
					key = keyValuePair.Key;
					return true;
				}
			}

			key = default;
			return false;
		}

		public static bool TryGetKeys<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TValue value, out TKey[] keys)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				keys = Array.Empty<TKey>();
				return false;
			}

			var rawKeys = new List<TKey>();

			foreach (var keyValuePair in self)
			{
				if (keyValuePair.Value.Equals(value))
				{
					rawKeys.Add(keyValuePair.Key);
				}
			}

			keys = rawKeys.ToArray();
			var hasKeys = !keys.IsEmpty();
			return hasKeys;
		}
	}
}