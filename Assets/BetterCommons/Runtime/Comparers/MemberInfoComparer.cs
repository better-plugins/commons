using System.Reflection;

namespace Better.Commons.Runtime.Comparers
{
	public class MemberInfoComparer : EqualityComparer<MemberInfo>
	{
		protected override bool EqualsValue(MemberInfo x, MemberInfo y)
		{
			var equals = x.Name == y.Name && x.DeclaringType == y.DeclaringType;
			return equals;
		}

		public override int GetHashCode(MemberInfo obj)
		{
			unchecked
			{
				var hashCode = obj.Name.GetHashCode();

				if (obj.DeclaringType != null)
				{
					var declaringHashCode = obj.DeclaringType.GetHashCode();
					hashCode = hashCode * 397 ^ declaringHashCode;
				}

				return hashCode;
			}
		}
	}
}