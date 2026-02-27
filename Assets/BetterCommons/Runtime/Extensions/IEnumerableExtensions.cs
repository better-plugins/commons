using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;
using UnityEngine.Scripting;
using Random = UnityEngine.Random;

namespace Better.Commons.Runtime.Extensions
{
	public static class IEnumerableExtensions
	{
		[Preserve]
		public static void ForceEnumerate<T>(this IEnumerable<T> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			using var enumerator = self.GetEnumerator();

			while (enumerator.MoveNext())
			{
			}
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}

			var shuffled = self.OrderBy(_ => Guid.NewGuid());
			return shuffled;
		}

		public static T Find<T>(this IEnumerable<object> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return default;
			}

			if (!self.TryFind(out T item))
			{
				DebugUtility.LogException<InvalidOperationException>();
			}

			return item;
		}

		public static T FindOrDefault<T>(this IEnumerable<object> self)
		{
			self.TryFind(out T item);
			return item;
		}

		public static bool TryFind<T>(this IEnumerable<object> self, out T item)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				item = default;

				return false;
			}

			foreach (var x in self)
			{
				if (x is T element)
				{
					item = element;
					return true;
				}
			}

			item = default;
			return false;
		}

		public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> self, Func<T, TKey> keySelector)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				yield break;
			}

			if (keySelector == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(keySelector));
				yield break;
			}

			var seenKeys = new HashSet<TKey>();

			foreach (var element in self)
			{
				if (seenKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}

		public static bool IsEmpty<T>(this IEnumerable<T> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return true;
			}

			var isEmpty = !self.Any();
			return isEmpty;
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
		{
			if (self == null)
			{
				return true;
			}

			var isEmpty = self.IsEmpty();
			return isEmpty;
		}

		public static ulong Sum<T>(this IEnumerable<T> self, Func<T, ulong> selector)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return default;
			}

			if (selector == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(selector));
				return 0ul;
			}

			var sum = 0ul;

			foreach (var element in self)
			{
				sum += selector.Invoke(element);
			}

			return sum;
		}

		public static T GetRandom<T>(this IEnumerable<T> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return default;
			}

			var valuesArray = self.ToArray();
			var index = Random.Range(0, valuesArray.Length);
			var element = valuesArray.ElementAt(index);

			return element;
		}

		public static T GetRandom<T>(this IEnumerable<T> self, params T[] excludes)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return default;
			}

			self = self.Except(excludes);
			var element = self.GetRandom();

			return element;
		}

		public static T GetRandomWithWeights<T>(this IEnumerable<T> self, Func<T, float> weightSelector)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return default;
			}

			if (weightSelector == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(weightSelector));
				return default;
			}

			float weight;

			var weightsModels = self.Select(value =>
			{
				weight = weightSelector.Invoke(value);
				var model = new Tuple<T, float>(value, weight);
				return model;
			});

			var element = GetRandomWithWeights(weightsModels);
			return element;
		}

		private static T GetRandomWithWeights<T>(this IEnumerable<Tuple<T, float>> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return default;
			}

			var valuesArray = self.ToArray();

			if (valuesArray.IsEmpty())
			{
				var message = $"{nameof(valuesArray)} cannot be empty";
				DebugUtility.LogException<InvalidOperationException>(message);

				return default;
			}

			var totalWeight = valuesArray.Sum(v => v.Item2);

			if (totalWeight <= 0f)
			{
				var message = $"{nameof(totalWeight)}({totalWeight}) not valid, returned first item";
				Debug.LogWarning(message);

				var element = valuesArray[0].Item1;
				return element;
			}

			var cumulativeWeight = Random.Range(0f, totalWeight);

			for (var i = 0; i < valuesArray.Length; i++)
			{
				cumulativeWeight -= valuesArray[i].Item2;

				if (cumulativeWeight <= 0)
				{
					var element = valuesArray[i].Item1;
					return element;
				}
			}

			var fallbackElement = valuesArray[0].Item1;
			var operationMessage = $"Unexpected error occurred while selecting a weighted random item, returns {fallbackElement}";
			DebugUtility.LogException<InvalidOperationException>(operationMessage);

			return fallbackElement;
		}

		public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> self, int count)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Enumerable.Empty<T>();
			}

			if (count < 0)
			{
				var message = $"{nameof(count)} cannot be less 0";
				DebugUtility.LogException<ArgumentOutOfRangeException>(message);
				return Enumerable.Empty<T>();
			}

			var valuesList = self.ToList();
			valuesList.Shuffle();

			if (count >= valuesList.Count)
			{
				return valuesList;
			}

			var results = valuesList.Take(count);
			return results;
		}

		public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> self, int count, params T[] excludes)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Enumerable.Empty<T>();
			}

			self = self.Except(excludes);
			var element = self.GetRandom(count);
			return element;
		}
	}
}