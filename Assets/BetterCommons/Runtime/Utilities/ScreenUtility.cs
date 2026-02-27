using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public static class ScreenUtility
	{
		public static Rect GetScreenRect()
		{
			var rect = new Rect(0f, 0f, Screen.width, Screen.height);
			return rect;
		}
	}
}