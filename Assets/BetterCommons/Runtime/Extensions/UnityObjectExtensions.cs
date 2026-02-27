using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using Object = UnityEngine.Object;

namespace Better.Commons.Runtime.Extensions
{
	public static class UnityObjectExtensions
	{
		public static bool IsNullOrDestroyed(this Object self)
		{
			if (ReferenceEquals(self, null))
			{
				return true;
			}

			var isNull = self == null;
			return isNull;
		}

		public static void Destroy(this Object self)
		{
			if (self.IsNullOrDestroyed())
			{
				var message = $"{nameof(self)} already null or destroyed";
				DebugUtility.LogException<ArgumentNullException>(message);
				return;
			}

			Object.Destroy(self);
		}

		public static void Destroy(this Object self, float delay)
		{
			if (self.IsNullOrDestroyed())
			{
				var message = $"{nameof(self)} already null or destroyed";
				DebugUtility.LogException<ArgumentNullException>(message);
				return;
			}

			delay = MathF.Max(delay, 0f);
			Object.Destroy(self, delay);
		}

		public static void Destroy(this IEnumerable<Object> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			var cached = self.ToArray();

			for (var i = 0; i < cached.Length; i++)
			{
				cached[i]
					.Destroy();
			}
		}

		public static void Destroy(this IEnumerable<Object> self, float delay)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			var cached = self.ToArray();

			for (var i = 0; i < cached.Length; i++)
			{
				cached[i]
					.Destroy(delay);
			}
		}
	}
}