using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class ComponentExtensions
	{
		public static void DestroyGameObject(this Component self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.gameObject.Destroy();
		}

		public static void DestroyGameObject(this Component self, float delay)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.gameObject.Destroy(delay);
		}

		public static void DestroyGameObject(this IEnumerable<Component> self)
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
					.DestroyGameObject();
			}
		}

		public static void DestroyGameObject(this IEnumerable<Component> self, float delay)
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
					.DestroyGameObject(delay);
			}
		}

		public static IEnumerable<GameObject> GetGameObjects(this IEnumerable<Component> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Enumerable.Empty<GameObject>();
			}

			var gameObjects = self.Select(c => c.gameObject);
			return gameObjects;
		}

		public static IEnumerable<Transform> GetTransforms(this IEnumerable<Component> self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Enumerable.Empty<Transform>();
			}

			var transforms = self.Select(c => c.transform);
			return transforms;
		}

		public static T GetOrAddComponent<T>(this Component self)
			where T : Component
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}
			
			var component = self.gameObject.GetOrAddComponent<T>();
			return component;
		}

		public static bool TryGetComponentInParent<T>(this Component self, out T component)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				component = default;
				return false;
			}

			var got = self.gameObject.TryGetComponentInParent(out component);
			return got;
		}

		public static bool TryGetComponentInChildren<T>(this Component self, out T component)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				component = default;

				return false;
			}

			var got = self.gameObject.TryGetComponentInChildren(out component);
			return got;
		}
	}
}