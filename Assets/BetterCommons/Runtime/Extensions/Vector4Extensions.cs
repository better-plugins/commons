using System.Collections.Generic;
using Better.Commons.Runtime.Utilities;
using Unity.Collections;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class Vector4Extensions
	{
		public static bool Approximately(this Vector4 current, Vector4 other)
		{
			var approximately = Vector4Utility.Approximately(current, other);
			return approximately;
		}

		public static Vector4 GetDirectionTo(this Vector4 self, Vector4 to)
		{
			var direction = Vector4Utility.Direction(self, to);
			return direction;
		}

		public static float GetSqrDistanceTo(this Vector4 self, Vector4 to)
		{
			var sqrDistance = Vector4Utility.SqrDistance(self, to);
			return sqrDistance;
		}

		public static Vector4 GetAbs(this Vector4 self)
		{
			var value = Vector4Utility.Abs(self);
			return value;
		}

		public static void Abs(this ref Vector4 self)
		{
			self = self.GetAbs();
		}

		public static Vector4 Sum(this NativeArray<Vector4> self)
		{
			var sum = Vector4Utility.Sum(self);
			return sum;
		}

		public static Vector4 Sum(this IEnumerable<Vector4> self)
		{
			var sum = Vector4Utility.Sum(self);
			return sum;
		}

		public static Vector4 Sum(this Vector4[] self)
		{
			var sum = Vector4Utility.Sum(self);
			return sum;
		}

		public static Vector4 GetAverage(NativeArray<Vector4> self)
		{
			var average = Vector4Utility.Average(self);
			return average;
		}

		public static Vector4 GetAverage(IEnumerable<Vector4> self)
		{
			var average = Vector4Utility.Average(self);
			return average;
		}

		public static Vector4 GetAverage(this Vector4[] self)
		{
			var average = Vector4Utility.Average(self);
			return average;
		}
	}
}