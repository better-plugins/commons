using System;

namespace Better.Commons.Runtime.Comparers
{
	public class SubClassComparer : DirectableComparer<Type>
	{
		public SubClassComparer(bool bothDirection) : base(bothDirection)
		{
		}

		public SubClassComparer() : this(DefaultBothDirection)
		{
		}

		protected override bool DirectedEqualsValue(Type x, Type y)
		{
			var isSubclass = x.IsSubclassOf(y);
			return isSubclass;
		}

		public override int GetHashCode(Type type)
		{
			return 0;
		}
	}
}