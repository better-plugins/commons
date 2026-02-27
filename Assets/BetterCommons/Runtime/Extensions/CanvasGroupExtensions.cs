using System;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class CanvasGroupExtensions
	{
		public static void SetActive(this CanvasGroup self, bool isVisible)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.alpha = isVisible ? 1f : 0f;
			self.interactable = isVisible;
			self.blocksRaycasts = isVisible;
		}
	}
}