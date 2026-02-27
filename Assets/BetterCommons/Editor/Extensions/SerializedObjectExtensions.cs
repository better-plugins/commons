using System;
using System.Linq;
using System.Reflection;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEditor;
using UnityEngine;

namespace Better.Commons.EditorAddons.Extensions
{
	public static class SerializedObjectExtensions
	{
		private const string NativeObjectPtrName = "m_NativeObjectPtr";
		private static readonly FieldInfo ObjectPrtInfo;

		static SerializedObjectExtensions()
		{
			ObjectPrtInfo = typeof(SerializedObject).GetField(NativeObjectPtrName, BindingFlagsUtility.FieldsFlags);
		}

		public static bool ValidateTargets(this SerializedObject self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			try
			{
				if (self.IsDisposed())
				{
					return false;
				}

				var targets = self.targetObjects;

				var anyTargetInvalid = targets.Any(targetObject => targetObject.IsNullOrDestroyed());

				if (anyTargetInvalid)
				{
					return false;
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		public static bool IsDisposed(this SerializedObject self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return true;
			}

			try
			{
				if (ObjectPrtInfo != null)
				{
					var objectPrt = (IntPtr)ObjectPrtInfo.GetValue(self);
					return objectPrt == IntPtr.Zero;
				}
			}
			catch
			{
				return true;
			}

			return true;
		}

		public static bool IsScriptPropertyValid(this SerializedObject self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var scriptProperty = self.FindProperty(SerializedPropertyExtensions.ScriptPropertyName);

			if (scriptProperty == null)
			{
				return false;
			}

			if (scriptProperty.IsDisposed())
			{
				return false;
			}
			
			if (!scriptProperty.IsMonoScriptProperty())
			{
				return false;
			}

			return true;
		}

		public static bool IsAnyMonoBehaviourTargetPartOfPrefabInstance(this SerializedObject self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			if (self.targetObject is not MonoBehaviour)
			{
				return false;
			}

			foreach (var target in self.targetObjects)
			{
				if (PrefabUtility.IsPartOfNonAssetPrefabInstance(target))
				{
					return true;
				}
			}

			return false;
		}
	}
}