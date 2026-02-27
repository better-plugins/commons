using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class FloatExtensions
	{
		public static bool InRange(this float self, float min, float max)
		{
			var inRange = FloatUtility.InRange(self, min, max);
			return inRange;
		}

		public static bool IsNormalized(this float self)
		{
			var inRange = self.InRange(0f, 1f);
			return inRange;
		}
	}
}