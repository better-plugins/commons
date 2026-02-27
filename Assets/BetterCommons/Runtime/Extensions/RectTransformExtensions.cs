using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class RectTransformExtensions
	{
		public static IEnumerable<Vector3> CornersVisible(this RectTransform self, Camera camera)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Array.Empty<Vector3>();
			}

			if (camera.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(camera));
				return Array.Empty<Vector3>();
			}

			var screenBounds = ScreenUtility.GetScreenRect();
			var corners = self.GetScreenCorners(camera);
			var visibleCorners = corners.Where(corner => screenBounds.Contains(corner));

			return visibleCorners;
		}

		public static int CountCornersVisible(this RectTransform self, Camera camera)
		{
			var visibleCorners = self.CornersVisible(camera);
			var cornersCount = visibleCorners.Count();

			return cornersCount;
		}

		public static IEnumerable<Vector3> CornersInvisible(this RectTransform self, Camera camera)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Array.Empty<Vector3>();
			}

			if (camera.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(camera));
				return Array.Empty<Vector3>();
			}

			var screenBounds = ScreenUtility.GetScreenRect();
			var corners = self.GetScreenCorners(camera);
			var invisibleCorners = corners.Where(corner => !screenBounds.Contains(corner));

			return invisibleCorners;
		}

		public static int CountCornersInvisible(this RectTransform self, Camera camera)
		{
			var invisibleCorners = self.CornersVisible(camera);
			var cornersCount = invisibleCorners.Count();

			return cornersCount;
		}

		public static IEnumerable<Vector3> GetScreenCorners(this RectTransform self, Camera camera)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Enumerable.Empty<Vector3>();
			}

			if (camera.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(camera));
				return Enumerable.Empty<Vector3>();
			}

			var objectCorners = new Vector3[4];
			self.GetWorldCorners(objectCorners);
			var screenCorners = objectCorners.Select(camera.WorldToScreenPoint);

			return screenCorners;
		}

		public static bool IsFullyVisibleFrom(this RectTransform self, Camera camera)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			if (!self.gameObject.activeInHierarchy)
			{
				return false;
			}

			var visibleCornersCount = CountCornersVisible(self, camera);
			var isFullyVisible = visibleCornersCount == 4;
			return isFullyVisible;
		}

		public static bool IsVisibleFrom(this RectTransform self, Camera camera)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			if (!self.gameObject.activeInHierarchy)
			{
				return false;
			}

			var visibleCornersCount = CountCornersVisible(self, camera);
			var isVisible = visibleCornersCount > 0;
			return isVisible;
		}

		public static bool IsFullyStretched(this RectTransform self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			if (self.localScale != Vector3.one)
			{
				return false;
			}

			if (self.anchorMin != Vector2.zero)
			{
				return false;
			}

			if (self.anchorMax != Vector2.one)
			{
				return false;
			}

			var isZeroDelta = self.sizeDelta == Vector2.zero;
			return isZeroDelta;
		}

		public static void FullyStretch(this RectTransform self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.LocalReset();
			self.anchorMin = Vector2.zero;
			self.anchorMax = Vector2.one;
			self.sizeDelta = Vector2.zero;
		}
	}
}