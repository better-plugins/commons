using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class BoundsExtensions
	{
		public static bool Approximately(this Bounds self, Bounds other)
		{
			var approximately = BoundsUtility.Approximately(self, other);
			return approximately;
		}
	}
}