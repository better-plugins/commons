using System;
using System.Collections.Generic;

namespace Better.Commons.Runtime.Comparers
{
	[Serializable]
	public abstract class EqualityComparer<T> : IEqualityComparer<T>
	{
		public bool Equals(T x, T y)
		{
			if (ReferenceEquals(x, y))
			{
				return true;
			}

			if (ReferenceEquals(x, null))
			{
				return false;
			}

			if (ReferenceEquals(y, null))
			{
				return false;
			}

			var equaled = EqualsValue(x, y);
			return equaled;
		}

		protected abstract bool EqualsValue(T x, T y);
		
		public abstract int GetHashCode(T obj);
	}
}