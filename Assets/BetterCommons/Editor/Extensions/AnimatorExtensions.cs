using System;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEditor.Animations;
using UnityEngine;

namespace Better.Commons.EditorAddons.Extensions
{
	public static class AnimatorExtensions
	{
		public static bool TryGetAnimatorController(this Animator self, out AnimatorController animatorController)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				animatorController = null;
				return false;
			}

			if (self.runtimeAnimatorController is AnimatorController specificController)
			{
				animatorController = specificController;
				return true;
			}

			if (self.runtimeAnimatorController is AnimatorOverrideController runtimeOverrideController
				&& runtimeOverrideController.runtimeAnimatorController is AnimatorController overrideController)
			{
				animatorController = overrideController;
				return true;
			}

			animatorController = null;
			return false;
		}
	}
}