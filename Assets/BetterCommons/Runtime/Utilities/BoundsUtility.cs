using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public static class BoundsUtility
	{
		public static bool Approximately(Bounds current, Bounds other)
		{
			if (!current.center.Approximately(other.center))
			{
				return false;
			}

			if (!current.size.Approximately(other.size))
			{
				return false;
			}

			return true;
		}
	}
}