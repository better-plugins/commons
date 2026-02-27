namespace Better.Commons.Runtime.Utilities
{
	public struct IntUtility
	{
		public static bool InRange(int value, int min, int max)
		{
			var inRange = min <= value && value <= max;
			return inRange;
		}
	}
}