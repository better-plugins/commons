using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public struct QuaternionUtility
	{
		public static bool IsNormalized(Quaternion quaternion)
		{
			var magnitude = quaternion.GetMagnitude();
			var normalized = Mathf.Approximately(magnitude, 1f);
			return normalized;
		}

		public static bool Approximately(Quaternion current, Quaternion other)
		{
			var dot = Quaternion.Dot(current, other);
			var approximately = Mathf.Approximately(dot, 1f);
			return approximately;
		}

		public static Quaternion Scale(Quaternion quaternion, Vector3 scale)
		{
			quaternion.eulerAngles = Vector3.Scale(quaternion.eulerAngles, scale);
			return quaternion;
		}

		public static Quaternion Scale(Quaternion quaternion, float scale)
		{
			var scaledQuaternion = Scale(quaternion, Vector3.one * scale);
			return scaledQuaternion;
		}
	}
}