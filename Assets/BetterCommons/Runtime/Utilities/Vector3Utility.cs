using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public struct Vector3Utility
	{
		public static Vector3 ClosestPointOnPlane(Vector3 planeCenter, Vector3 planeNormal, Vector3 point)
		{
			var distance = Vector3.Distance(planeCenter, point);
			var offset = distance * planeNormal;
			var closestPoint = point + offset;

			return closestPoint;
		}

		private static float DistanceFromPlane(Vector3 planeCenter, Vector3 planeNormal, Vector3 point)
		{
			var offset = planeCenter - point;
			var distance = Vector3.Dot(offset, planeNormal);

			return distance;
		}

		public static bool Approximately(Vector3 current, Vector3 other)
		{
			if (!Mathf.Approximately(current.x, other.x))
			{
				return false;
			}

			if (!Mathf.Approximately(current.y, other.y))
			{
				return false;
			}

			if (!Mathf.Approximately(current.z, other.z))
			{
				return false;
			}

			return true;
		}

		public static Vector3 MiddlePoint(Vector3 start, Vector3 end)
		{
			var total = start + end;
			var middle = total / 2f;

			return middle;
		}

		public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			a.Normalize();
			b.Normalize();

			var dot = Vector3.Dot(a, b);
			dot = Mathf.Clamp(dot, -1.0f, 1.0f);

			var theta = Mathf.Acos(dot) * t;
			var relativeVector = b - a * dot;
			relativeVector.Normalize();

			var value = a * Mathf.Cos(theta) + relativeVector * Mathf.Sin(theta);
			return value;
		}

		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			var value = SlerpUnclamped(a, b, t);

			return value;
		}

		public static Vector3 AxesInverseLerp(Vector3 a, Vector3 b, Vector3 value)
		{
			var x = Mathf.InverseLerp(a.x, b.x, value.x);
			var y = Mathf.InverseLerp(a.y, b.y, value.y);
			var z = Mathf.InverseLerp(a.z, b.z, value.z);

			var result = new Vector3(x, y, z);
			return result;
		}

		public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
		{
			if (a == b)
			{
				return 0f;
			}

			var ab = b - a;
			var av = value - a;
			var result = Vector3.Dot(av, ab) / Vector3.Dot(ab, ab);
			result = Mathf.Clamp01(result);

			return result;
		}

		public static Vector3 Direction(Vector3 from, Vector3 to)
		{
			var difference = to - from;
			var direction = difference.normalized;

			return direction;
		}

		public static float SqrDistance(Vector3 from, Vector3 to)
		{
			var difference = to - from;
			var sqrDistance = difference.sqrMagnitude;

			return sqrDistance;
		}

		public static Vector3 Abs(Vector3 source)
		{
			source.x = Mathf.Abs(source.x);
			source.y = Mathf.Abs(source.y);
			source.z = Mathf.Abs(source.z);

			return source;
		}
		
		public static Vector3 AxesSign(Vector3 source)
		{
			source.x = Mathf.Sign(source.x);
			source.y = Mathf.Sign(source.y);
			source.z = Mathf.Sign(source.z);

			return source;
		}
		
		public static Vector3 AxesMin(Vector3 source, Vector3 min)
		{
			source.x = Mathf.Min(source.x, min.x);
			source.y = Mathf.Min(source.y, min.y);
			source.z = Mathf.Min(source.z, min.z);

			return source;
		}
		
		public static Vector3 AxesMax(Vector3 source, Vector3 max)
		{
			source.x = Mathf.Max(source.x, max.x);
			source.y = Mathf.Max(source.y, max.y);
			source.z = Mathf.Max(source.z, max.z);

			return source;
		}

		public static Vector3 Sum(NativeArray<Vector3> vectors)
		{
			var sum = Vector3.zero;

			for (var i = 0; i < vectors.Length; i++)
			{
				var vector = vectors[i];
				sum += vector;
			}

			return sum;
		}

		public static Vector3 Sum(IEnumerable<Vector3> vectors)
		{
			var sum = Vector3.zero;

			foreach (var vector in vectors)
			{
				sum += vector;
			}

			return sum;
		}

		public static Vector3 Sum(params Vector3[] vectors)
		{
			var vectorList = vectors.ToList();
			return Sum(vectorList);
		}

		public static Vector3 Average(NativeArray<Vector3> vectors)
		{
			var sum = Sum(vectors);
			var average = sum / vectors.Length;

			return average;
		}

		public static Vector3 Average(IEnumerable<Vector3> vectors)
		{
			var sum = Sum(vectors);
			var count = vectors.Count();
			var average = sum / count;

			return average;
		}

		public static Vector3 Average(params Vector3[] vectors)
		{
			var sum = Sum(vectors);
			var average = sum / vectors.Length;

			return average;
		}
	}
}