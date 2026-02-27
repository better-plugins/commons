using System.Collections.Generic;
using Better.Commons.Runtime.Utilities;
using Unity.Collections;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class Vector3Extensions
	{
		public static bool Approximately(this Vector3 current, Vector3 other)
		{
			var approximately = Vector3Utility.Approximately(current, other);
			return approximately;
		}

		public static Vector3 GetDirectionTo(this Vector3 self, Vector3 to)
		{
			var direction = Vector3Utility.Direction(self, to);
			return direction;
		}

		public static float GetSqrDistanceTo(this Vector3 self, Vector3 to)
		{
			var sqrDistance = Vector3Utility.SqrDistance(self, to);
			return sqrDistance;
		}

		public static Vector3 GetAbs(this Vector3 self)
		{
			var value = Vector3Utility.Abs(self);
			return value;
		}
		
		public static Vector3 GetAxisSign(this Vector3 self)
		{
			var value = Vector3Utility.AxesSign(self);
			return value;
		}

		public static Vector3 GetAxisMin(this Vector3 self, Vector3 min)
		{
			var value = Vector3Utility.AxesMin(self, min);
			return value;
		}

		public static Vector3 GetAxisMax(this Vector3 self, Vector3 max)
		{
			var value = Vector3Utility.AxesMax(self, max);
			return value;
		}

		public static void Abs(this ref Vector3 self)
		{
			self = self.GetAbs();
		}

		public static Vector3 Sum(this NativeArray<Vector3> self)
		{
			var sum = Vector3Utility.Sum(self);
			return sum;
		}

		public static Vector3 Sum(this IEnumerable<Vector3> self)
		{
			var sum = Vector3Utility.Sum(self);
			return sum;
		}

		public static Vector3 Sum(this Vector3[] self)
		{
			var sum = Vector3Utility.Sum(self);
			return sum;
		}

		public static Vector3 GetAverage(NativeArray<Vector3> self)
		{
			var average = Vector3Utility.Average(self);
			return average;
		}

		public static Vector3 GetAverage(IEnumerable<Vector3> self)
		{
			var average = Vector3Utility.Average(self);
			return average;
		}

		public static Vector3 GetAverage(this Vector3[] self)
		{
			var average = Vector3Utility.Average(self);
			return average;
		}
	}
}