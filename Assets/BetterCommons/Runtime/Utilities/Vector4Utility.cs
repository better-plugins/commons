using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public struct Vector4Utility
	{
		public static bool Approximately(Vector4 current, Vector4 other)
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

			if (!Mathf.Approximately(current.w, other.w))
			{
				return false;
			}

			return true;
		}

		public static Vector4 MiddlePoint(Vector4 start, Vector4 end)
		{
			var total = start + end;
			var middle = total / 2f;

			return middle;
		}

		public static Vector4 SlerpUnclamped(Vector4 a, Vector4 b, float t)
		{
			a.Normalize();
			b.Normalize();

			var dot = Vector4.Dot(a, b);
			dot = Mathf.Clamp(dot, -1.0f, 1.0f);

			var theta = Mathf.Acos(dot) * t;
			var relativeVector = b - a * dot;
			relativeVector.Normalize();

			var value = a * Mathf.Cos(theta) + relativeVector * Mathf.Sin(theta);
			return value;
		}

		public static Vector4 Slerp(Vector4 a, Vector4 b, float t)
		{
			t = Mathf.Clamp01(t);
			var value = SlerpUnclamped(a, b, t);

			return value;
		}

		public static Vector4 AxesInverseLerp(Vector4 a, Vector4 b, Vector4 value)
		{
			var x = Mathf.InverseLerp(a.x, b.x, value.x);
			var y = Mathf.InverseLerp(a.y, b.y, value.y);
			var z = Mathf.InverseLerp(a.z, b.z, value.z);
			var w = Mathf.InverseLerp(a.w, b.w, value.w);

			var result = new Vector4(x, y, z, w);
			return result;
		}

		public static float InverseLerp(Vector4 a, Vector4 b, Vector4 value)
		{
			if (a == b)
			{
				return 0f;
			}

			var ab = b - a;
			var av = value - a;
			var result = Vector4.Dot(av, ab) / Vector4.Dot(ab, ab);
			result = Mathf.Clamp01(result);

			return result;
		}

		public static Vector4 Direction(Vector4 from, Vector4 to)
		{
			var difference = to - from;
			var direction = difference.normalized;

			return direction;
		}

		public static float SqrDistance(Vector4 from, Vector4 to)
		{
			var difference = to - from;
			var sqrDistance = difference.sqrMagnitude;

			return sqrDistance;
		}

		public static Vector4 Abs(Vector4 source)
		{
			source.x = Mathf.Abs(source.x);
			source.y = Mathf.Abs(source.y);
			source.z = Mathf.Abs(source.z);
			source.w = Mathf.Abs(source.w);

			return source;
		}

		public static Vector4 Sum(NativeArray<Vector4> vectors)
		{
			var sum = Vector4.zero;

			for (var i = 0; i < vectors.Length; i++)
			{
				var vector = vectors[i];
				sum += vector;
			}

			return sum;
		}

		public static Vector4 Sum(IEnumerable<Vector4> vectors)
		{
			var sum = Vector4.zero;

			foreach (var vector in vectors)
			{
				sum += vector;
			}

			return sum;
		}

		public static Vector4 Sum(params Vector4[] vectors)
		{
			var vectorList = vectors.ToList();
			return Sum(vectorList);
		}

		public static Vector4 Average(NativeArray<Vector4> vectors)
		{
			var sum = Sum(vectors);
			var average = sum / vectors.Length;

			return average;
		}

		public static Vector4 Average(IEnumerable<Vector4> vectors)
		{
			var sum = Sum(vectors);
			var count = vectors.Count();
			var average = sum / count;

			return average;
		}

		public static Vector4 Average(params Vector4[] vectors)
		{
			var sum = Sum(vectors);
			var average = sum / vectors.Length;

			return average;
		}
	}
}