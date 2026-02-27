using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class AnimatorStateInfoExtensions
	{
		public static float GetLoopedNormalizedTime(this AnimatorStateInfo self)
		{
			var loopedNormalizedTime = self.normalizedTime % 1f;
			return loopedNormalizedTime;
		}

		public static int GetLoopCount(this AnimatorStateInfo self)
		{
			var loopCount = Mathf.FloorToInt(self.normalizedTime);
			return loopCount;
		}
	}
}