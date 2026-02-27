using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class IDictionaryExtensions
	{
		public static bool TryGetKey<TKey, TValue>(this IDictionary<TKey, TValue> self, TValue value, out TKey key)
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

		public static bool TryGetKeys<TKey, TValue>(this IDictionary<TKey, TValue> self, TValue value, out TKey[] keys)
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

		public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var removedAny = false;

			foreach (var key in keys)
			{
				if (self.Remove(key))
				{
					removedAny = true;
				}
			}

			return removedAny;
		}

		public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> self, TValue value, out TKey key)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				key = default;
				return false;
			}

			if (self.TryGetKey(value, out key))
			{
				var removed = self.Remove(key);
				return removed;
			}

			return false;
		}

		public static bool RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> self, TValue value, out TKey[] keys)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				keys = Array.Empty<TKey>();
				return false;
			}

			if (self.TryGetKeys(value, out keys))
			{
				var removed = self.Remove(keys);
				return removed;
			}

			return false;
		}

		public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
			where TValue : new()
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return default;
			}

			if (!self.TryGetValue(key, out var value))
			{
				value = new TValue();
				self.Add(key, value);
			}

			return value;
		}
	}
}