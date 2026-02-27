using System;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class RigidbodyExtensions
	{
		public static TCollider[] GetAttachedColliders<TCollider>(this Rigidbody self)
			where TCollider : Collider
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Array.Empty<TCollider>();
			}

			var colliders = self.GetComponentsInChildren<TCollider>()
				.Where(collider => collider.attachedRigidbody == self)
				.ToArray();

			return colliders;
		}

		public static Collider[] GetAttachedColliders(this Rigidbody self)
		{
			return self.GetAttachedColliders<Collider>();
		}
	}
}