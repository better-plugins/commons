using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class TransformExtensions
	{
		public static bool HasParent(this Transform self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var hasParent = !self.parent.IsNullOrDestroyed();
			return hasParent;
		}

		public static bool TryFind(this Transform self, string name, out Transform child)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				child = null;
				return false;
			}

			if (name.IsNullOrWhiteSpace())
			{
				var nameMessage = $"{nameof(name)}({name}) cannot be null or empty";
				DebugUtility.LogException<ArgumentException>(nameMessage);

				child = null;
				return false;
			}

			child = self.Find(name);

			if (child.IsNullOrDestroyed())
			{
				child = null;
				return false;
			}

			return true;
		}

		public static Transform CreateChild(this Transform self, string name)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}

			var createdGameObject = new GameObject(name);
			var createdTransform = createdGameObject.transform;
			createdTransform.parent = self;
			createdTransform.LocalReset();

			return createdTransform;
		}

		public static Transform CreateChild(this Transform self)
		{
			const string name = "GameObject (new)";
			var created = self.CreateChild(name);

			return created;
		}

		public static void Copy(this Transform self, Transform root)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (root.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(root));
				return;
			}

			self.position = root.position;
			self.rotation = root.rotation;
		}

		public static void LocalCopy(this Transform self, Transform root, bool scaleCopy = true)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (root.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(root));
				return;
			}

			self.localPosition = root.localPosition;
			self.localRotation = root.localRotation;

			if (scaleCopy)
			{
				self.localScale = root.localScale;
			}
		}

		public static void CopyPosition(this Transform self, Transform root)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (root.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(root));
				return;
			}

			self.position = root.position;
		}

		public static void CopyLocalPosition(this Transform self, Transform root)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (root.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(root));
				return;
			}

			self.localPosition = root.localPosition;
		}

		public static void CopyRotation(this Transform self, Transform root)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (root.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(root));
				return;
			}

			self.rotation = root.rotation;
		}

		public static void CopyLocalRotation(this Transform self, Transform root)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (root.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(root));
				return;
			}

			self.localRotation = root.localRotation;
		}

		public static void Reset(this Transform self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.position = Vector3.zero;
			self.rotation = Quaternion.identity;
		}

		public static void LocalReset(this Transform self, bool scaleReset = true)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.localPosition = Vector3.zero;
			self.localRotation = Quaternion.identity;

			if (scaleReset)
			{
				self.localScale = Vector3.one;
			}
		}

		public static IEnumerable<Transform> GetChildren(this Transform self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Enumerable.Empty<Transform>();
			}

			var result = new List<Transform>();

			for (var i = 0; i < self.childCount; i++)
			{
				result.Add(self.GetChild(i));
			}

			return result;
		}

		public static void DestroyChildren(this Transform self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.GetChildren()
				.GetGameObjects()
				.Destroy();
		}

		public static void DestroyChildren(this Transform self, float delay)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.GetChildren()
				.GetGameObjects()
				.Destroy(delay);
		}
	}
}