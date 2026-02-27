using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class QuaternionExtensions
	{
		public static float GetMagnitude(this Quaternion self)
		{
			var sqrMagnitude = self.GetSqrMagnitude();
			var magnitude = Mathf.Sqrt(sqrMagnitude);
			return magnitude;
		}

		public static float GetSqrMagnitude(this Quaternion self)
		{
			var magnitude = self.x * self.x + self.y * self.y + self.z * self.z + self.w * self.w;
			return magnitude;
		}

		public static bool IsNormalized(this Quaternion self)
		{
			var isNormalized = QuaternionUtility.IsNormalized(self);
			return isNormalized;
		}

		public static bool Approximately(this Quaternion self, Quaternion other)
		{
			var approximately = QuaternionUtility.Approximately(self, other);
			return approximately;
		}

		public static Quaternion GetScaled(this Quaternion self, Vector3 scale)
		{
			var scaled = QuaternionUtility.Scale(self, scale);
			return scaled;
		}

		public static void Scale(this ref Quaternion self, Vector3 scale)
		{
			self = self.GetScaled(scale);
		}

		public static Quaternion GetScaled(this Quaternion self, float scale)
		{
			var scaled = QuaternionUtility.Scale(self, scale);
			return scaled;
		}

		public static void Scale(this ref Quaternion self, float scale)
		{
			self = self.GetScaled(scale);
		}
	}
}