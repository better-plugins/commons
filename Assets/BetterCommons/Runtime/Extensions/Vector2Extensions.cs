using System.Collections.Generic;
using Better.Commons.Runtime.Utilities;
using Unity.Collections;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class Vector2Extensions
	{
		public static bool Approximately(this Vector2 current, Vector2 other)
		{
			var approximately = Vector2Utility.Approximately(current, other);
			return approximately;
		}

		public static Vector2 GetDirectionTo(this Vector2 self, Vector2 to)
		{
			var direction = Vector2Utility.Direction(self, to);
			return direction;
		}

		public static float GetSqrDistanceTo(this Vector2 self, Vector2 to)
		{
			var sqrDistance = Vector2Utility.SqrDistance(self, to);
			return sqrDistance;
		}

		public static Vector2 GetAbs(this Vector2 self)
		{
			var value = Vector2Utility.Abs(self);
			return value;
		}

		public static void Abs(this ref Vector2 self)
		{
			self = self.GetAbs();
		}

		public static Vector2 Sum(this NativeArray<Vector2> self)
		{
			var sum = Vector2Utility.Sum(self);
			return sum;
		}

		public static Vector2 Sum(this IEnumerable<Vector2> self)
		{
			var sum = Vector2Utility.Sum(self);
			return sum;
		}

		public static Vector2 Sum(this Vector2[] self)
		{
			var sum = Vector2Utility.Sum(self);
			return sum;
		}

		public static Vector2 GetAverage(NativeArray<Vector2> self)
		{
			var average = Vector2Utility.Average(self);
			return average;
		}

		public static Vector2 GetAverage(IEnumerable<Vector2> self)
		{
			var average = Vector2Utility.Average(self);
			return average;
		}

		public static Vector2 GetAverage(this Vector2[] self)
		{
			var average = Vector2Utility.Average(self);
			return average;
		}
	}
}