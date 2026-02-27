using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class SystemObjectExtensions
	{
		public static bool CompareTypes(this object self, object other)
		{
			var typeEqual = SystemObjectUtility.CompareTypes(self, other);
			return typeEqual;
		}

		public static bool IsNullable<T>(this T self)
		{
			var isNullable = ReflectionUtility.IsNullable<T>();
			return isNullable;
		}
	}
}