#if UNITY_PHYSICS_2D

using System;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class Rigidbody2DExtensions
	{
		public static TCollider[] GetAttachedColliders<TCollider>(this Rigidbody2D self)
			where TCollider : Collider2D
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

		public static Collider2D[] GetAttachedColliders(this Rigidbody2D self)
		{
			return self.GetAttachedColliders<Collider2D>();
		}
	}
}

#endif