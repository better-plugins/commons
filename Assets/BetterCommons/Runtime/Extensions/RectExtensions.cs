using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class RectExtensions
	{
		public static float GetRatio(this Rect self)
		{
			var ratio = self.size.y / self.size.y;
			return ratio;
		}
	}
}