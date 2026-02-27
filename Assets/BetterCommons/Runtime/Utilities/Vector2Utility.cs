using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public struct Vector2Utility
	{
		public static Vector2 SlerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			var dot = Vector2.Dot(a.normalized, b.normalized);
			dot = Mathf.Clamp(dot, -1.0f, 1.0f);
			var theta = Mathf.Acos(dot) * t;
			var relative = b - a * dot;
			relative.Normalize();
			var result = a * Mathf.Cos(theta) + relative * Mathf.Sin(theta);

			return result;
		}

		public static Vector2 Slerp(Vector2 a, Vector2 b, float t)
		{
			t = Mathf.Clamp01(t);
			return SlerpUnclamped(a, b, t);
		}

		public static bool Approximately(Vector2 current, Vector2 other)
		{
			if (!Mathf.Approximately(current.x, other.x))
			{
				return false;
			}

			if (!Mathf.Approximately(current.y, other.y))
			{
				return false;
			}

			return true;
		}

		public static Vector2 MiddlePoint(Vector2 start, Vector2 end)
		{
			var total = start + end;
			var middle = total / 2f;

			return middle;
		}

		public static Vector2 AxesInverseLerp(Vector2 a, Vector2 b, Vector2 value)
		{
			var x = Mathf.InverseLerp(a.x, b.x, value.x);
			var y = Mathf.InverseLerp(a.y, b.y, value.y);

			var result = new Vector2(x, y);
			return result;
		}

		public static float InverseLerp(Vector2 a, Vector2 b, Vector2 value)
		{
			if (a == b)
			{
				return 0f;
			}

			var ab = b - a;
			var av = value - a;
			var result = Vector2.Dot(av, ab) / Vector2.Dot(ab, ab);
			result = Mathf.Clamp01(result);

			return result;
		}

		public static Vector2 Direction(Vector2 from, Vector2 to)
		{
			var difference = to - from;
			var direction = difference.normalized;

			return direction;
		}

		public static float SqrDistance(Vector2 from, Vector2 to)
		{
			var difference = to - from;
			var sqrDistance = difference.sqrMagnitude;

			return sqrDistance;
		}

		public static Vector2 Abs(Vector2 source)
		{
			source.x = Mathf.Abs(source.x);
			source.y = Mathf.Abs(source.y);

			return source;
		}

		public static Vector2 Sum(NativeArray<Vector2> vectors)
		{
			var sum = Vector2.zero;

			for (var i = 0; i < vectors.Length; i++)
			{
				var vector = vectors[i];
				sum += vector;
			}

			return sum;
		}

		public static Vector2 Sum(IEnumerable<Vector2> vectors)
		{
			var sum = Vector2.zero;

			foreach (var vector in vectors)
			{
				sum += vector;
			}

			return sum;
		}

		public static Vector2 Sum(params Vector2[] vectors)
		{
			var vectorList = vectors.ToList();
			return Sum(vectorList);
		}

		public static Vector2 Average(NativeArray<Vector2> vectors)
		{
			var sum = Sum(vectors);
			var average = sum / vectors.Length;

			return average;
		}

		public static Vector2 Average(IEnumerable<Vector2> vectors)
		{
			var sum = Sum(vectors);
			var count = vectors.Count();
			var average = sum / count;

			return average;
		}

		public static Vector2 Average(params Vector2[] vectors)
		{
			var sum = Sum(vectors);
			var average = sum / vectors.Length;

			return average;
		}
	}
}