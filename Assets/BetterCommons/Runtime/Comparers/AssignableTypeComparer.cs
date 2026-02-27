using System;

namespace Better.Commons.Runtime.Comparers
{
	public class AssignableTypeComparer : DirectableComparer<Type>
	{
		public AssignableTypeComparer(bool bothDirection) : base(bothDirection)
		{
		}

		public AssignableTypeComparer() : this(DefaultBothDirection)
		{
		}

		protected override bool DirectedEqualsValue(Type x, Type y)
		{
			var isAssignable = x.IsAssignableFrom(y);
			return isAssignable;
		}

		public override int GetHashCode(Type type)
		{
			return 0;
		}
	}
}