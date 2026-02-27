namespace Better.Commons.Runtime.Utilities
{
	public struct FloatUtility
	{
		public static bool InRange(float value, float min, float max)
		{
			var inRange = min <= value && value <= max;
			return inRange;
		}
	}
}