using Better.Commons.Runtime.Comparers;
using UnityEditor;

namespace Better.Commons.EditorAddons.Comparers
{
	public class SerializedPropertyComparer : EqualityComparer<SerializedProperty>
	{
		protected override bool EqualsValue(SerializedProperty x, SerializedProperty y)
		{
			// TODO: will be updated with #14
			/*
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
			*/

			return false;
		}

		public override int GetHashCode(SerializedProperty obj)
		{
			// TODO: will be updated with #14
			/*
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
			*/

			var pathHashCode = obj.propertyPath.GetHashCode();
			return pathHashCode;
		}
	}
}