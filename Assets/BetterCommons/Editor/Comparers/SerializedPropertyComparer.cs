using Better.Commons.EditorAddons.Extensions;
using Better.Commons.Runtime.Comparers;
using Better.Commons.Runtime.Extensions;
using UnityEditor;

namespace Better.Commons.EditorAddons.Comparers
{
	public class SerializedPropertyComparer : EqualityComparer<SerializedProperty>
	{
		protected override bool EqualsValue(SerializedProperty x, SerializedProperty y)
		{
			if (x.IsDisposed())
			{
				return false;
			}

			if (y.IsDisposed())
			{
				return false;
			}

			if (!x.CompareTypes(y))
			{
				return false;
			}

			var pathEquals = x.IsPathEquals(y);
			return pathEquals;

			return false;
		}

		public override int GetHashCode(SerializedProperty obj)
		{
			if (!obj.Verify())
			{
				return 0;
			}

			if (obj.IsDisposed())
			{
				return 0;
			}

			if (obj.propertyPath.IsNullOrEmpty())
			{
				return 0;
			}

			var pathHashCode = obj.propertyPath.GetHashCode();
			return pathHashCode;
		}
	}
}