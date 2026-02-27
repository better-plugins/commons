using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class GameObjectExtensions
	{
		public static void SetActive(this IEnumerable<GameObject> self, bool value)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			var cached = self.ToArray();

			for (var i = 0; i < cached.Length; i++)
			{
				cached[i].SetActive(value);
			}
		}

		public static T GetOrAddComponent<T>(this GameObject self)
			where T : Component
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}

			if (self.TryGetComponent(out T component))
			{
				return component;
			}

			component = self.AddComponent<T>();
			return component;
		}

		public static bool TryGetComponentInParent<T>(this GameObject self, out T component)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				component = default;
				return false;
			}

			component = self.GetComponentInParent<T>();
			var componentFound = component != null;
			return componentFound;
		}

		public static bool TryGetComponentInChildren<T>(this GameObject self, out T component)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				component = default;
				return false;
			}

			component = self.GetComponentInChildren<T>();
			var componentFound = component != null;
			return componentFound;
		}
		
		public static bool TryGetComponentInParent(this GameObject self, Type componentType, out Component component)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				component = default;
				return false;
			}

			component = self.GetComponentInParent(componentType);
			var componentFound = component != null;
			return componentFound;
		}

		public static bool TryGetComponentInChildren(this GameObject self, Type componentType, out Component component)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				component = default;
				return false;
			}

			component = self.GetComponentInChildren(componentType);
			var componentFound = component != null;
			return componentFound;
		}

		public static void RecursiveSetLayer(this GameObject self, int layer, bool validate = true)
		{
			if (validate && self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.layer = layer;

			foreach (Transform child in self.transform)
			{
				RecursiveSetLayer(child.gameObject, layer, false);
			}
		}
	}
}