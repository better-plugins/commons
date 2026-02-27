using System;

namespace Better.Commons.Runtime.Comparers
{
	[Serializable]
	public abstract class DirectableComparer<T> : EqualityComparer<T>
	{
		public const bool DefaultBothDirection = false;
		public bool BothDirection { get; }

		protected DirectableComparer(bool bothDirection)
		{
			BothDirection = bothDirection;
		}

		protected sealed override bool EqualsValue(T x, T y)
		{
			if (DirectedEqualsValue(x, y))
			{
				return true;
			}

			var equals = BothDirection && DirectedEqualsValue(y, x);
			return equals;
		}

		protected abstract bool DirectedEqualsValue(T x, T y);
	}
}