using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class IntExtensions
	{
		public static bool InRange(this int self, int min, int max)
		{
			var inRange = IntUtility.InRange(self, min, max);
			return inRange;
		}
	}
}