using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEditor;
using UnityEngine;

namespace Better.Commons.EditorAddons.Extensions
{
	public static class SerializedPropertyExtensions
	{
		public const string ScriptPropertyName = "m_Script";
		private const string NativePropertyPtrName = "m_NativePropertyPtr";
		private const int IteratorNotAtEnd = 2;
		private const string VerifyMethodName = "Verify";

		private const string ArrayIndexRegexPattern = @"\[([^\[\]]*)\](?!\.)";
		private const string ArrayRegexPattern = @"\.Array\.data(?:\[(?<index>\d+)\])+(?!\.)";
		private const string PropertiesSplitRegexPattern = @"\.Array\.data\[[0-9]+\]";
		private const string ArrayDataWithIndexRegexPattern = @"(Array\.data\[\d+\]|[^.]+)";

		public static MethodInfo VerifyMethod { get; }
		public static FieldInfo PropertyPrtInfo { get; }
		public static Regex ArrayIndexRegex { get; }
		public static Regex ArrayRegex { get; }
		public static Regex PropertiesSplitRegex { get; }
		public static Regex ArrayDataWithIndexRegex { get; }

		static SerializedPropertyExtensions()
		{
			var serializedPropertyType = typeof(SerializedProperty);
			VerifyMethod = serializedPropertyType.GetMethod(VerifyMethodName, BindingFlagsUtility.FieldsFlags);
			PropertyPrtInfo = serializedPropertyType.GetField(NativePropertyPtrName, BindingFlagsUtility.FieldsFlags);
			ArrayIndexRegex = new Regex(ArrayIndexRegexPattern, RegexOptions.Compiled);
			ArrayRegex = new Regex(ArrayRegexPattern, RegexOptions.Compiled);
			ArrayDataWithIndexRegex = new Regex(PropertiesSplitRegexPattern, RegexOptions.Compiled);
			PropertiesSplitRegex = new Regex(ArrayDataWithIndexRegexPattern, RegexOptions.Compiled);
		}

		public static bool TryGetPropertyType(this SerializedProperty self, out Type type)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				type = null;
				return false;
			}

			if (self.IsScriptProperty())
			{
				type = typeof(MonoScript);
				return true;
			}

			if (!self.TryGetParent(out var container))
			{
				type = null;
				return false;
			}

			if (!TryGetMember(self, container, out var member))
			{
				type = null;
				return false;
			}

			if (!member.TryGetMemberType(out var memberType))
			{
				type = null;
				return false;
			}

			if (!memberType.IsArrayOrList())
			{
				type = memberType;
				return true;
			}

			type = memberType.GetCollectionElementType();
			return true;
		}

		public static bool TryGetManagedType(this SerializedProperty self, out Type type)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				type = null;
				return false;
			}

			if (self.propertyType != SerializedPropertyType.ManagedReference)
			{
				type = null;
				return false;
			}

			var managedReferenceValue = self.managedReferenceValue;

			if (managedReferenceValue != null)
			{
				type = managedReferenceValue.GetType();
				return true;
			}

			var typename = self.managedReferenceFullTypename;

			if (typename.IsNullOrEmpty())
			{
				type = null;
				return false;
			}

			var split = typename.Split(CharUtility.WhiteSpace);

			if (split.Length < 2)
			{
				type = null;
				return false;
			}

			var assemblyName = split[0];

			if (!ReflectionUtility.TryFindAssemblyByName(assemblyName, out var assembly))
			{
				type = null;
				return false;
			}

			var typeName = split[1];
			var bufferType = assembly.GetType(typeName);

			if (bufferType == null)
			{
				type = null;
				return false;
			}

			type = bufferType;
			return true;
		}

		public static bool IsArrayElement(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return false;
			}

			if (ArrayIndexRegex.IsMatch(self.propertyPath))
			{
				return true;
			}

			return false;
		}

		public static bool TryGetArrayIndex(this SerializedProperty self, out int index)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				index = -1;
				return false;
			}

			if (!self.IsArrayElement())
			{
				index = -1;
				return false;
			}

			var matches = ArrayIndexRegex.Matches(self.propertyPath);

			if (matches.Count > 0)
			{
				var lastMatch = matches[^1];

				if (int.TryParse(lastMatch.Name, out var result))
				{
					index = result;
					return true;
				}
			}

			index = -1;
			return false;
		}

		public static TAttribute[] GetAttributes<TAttribute>(this SerializedProperty self, bool inherit = false)
			where TAttribute : Attribute
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return Array.Empty<TAttribute>();
			}

			if (!self.TryGetMember<FieldInfo>(out var fieldInfo))
			{
				return Array.Empty<TAttribute>();
			}

			var attributes = fieldInfo.GetCustomAttributes<TAttribute>(inherit);
			return attributes.ToArray();
		}

		public static string GetArrayNameFromPath(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return string.Empty;
			}

			var propertyPath = self.propertyPath;
			var arrayNameFromPath = ArrayDataWithIndexRegex.Replace(propertyPath, "");
			return arrayNameFromPath;
		}

		public static string GetArrayPath(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return string.Empty;
			}

			var propertyPath = self.propertyPath;
			var arrayNameFromPath = ArrayRegex.Replace(propertyPath, "");

			return arrayNameFromPath;
		}

		public static bool IsDisposed(this SerializedProperty self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return true;
			}

			if (self.serializedObject == null)
			{
				return false;
			}

			try
			{
				if (PropertyPrtInfo != null)
				{
					var propertyPrt = (IntPtr)PropertyPrtInfo.GetValue(self);
					return propertyPrt == IntPtr.Zero || self.serializedObject.IsDisposed();
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		public static bool IsNullOrDisposed(this SerializedProperty self)
		{
			if (self == null)
			{
				return true;
			}

			if (self.IsDisposed())
			{
				return true;
			}

			return false;
		}

		public static bool Verify(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return false;
			}

			if (self.serializedObject == null)
			{
				return false;
			}

			try
			{
				if (VerifyMethod != null)
				{
					VerifyMethod.Invoke(
						self,
						new object[]
						{
							IteratorNotAtEnd,
						});

					return true;
				}
			}
			catch
			{
				return false;
			}

			return false;
		}

		public static bool IsScriptProperty(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				throw new ArgumentException(nameof(self));
			}

			if (self.propertyPath == ScriptPropertyName)
			{
				return true;
			}

			return false;
		}

		public static bool IsMonoScriptProperty(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				throw new ArgumentException(nameof(self));
			}

			if (self.TryGetMonoScript(out _))
			{
				return true;
			}

			return false;
		}

		public static bool TryGetMonoScript(this SerializedProperty self, out MonoScript monoScript)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				monoScript = null;
				return false;
			}

			if (!self.IsScriptProperty())
			{
				monoScript = null;
				return false;
			}

			if (self.objectReferenceValue is MonoScript buffer)
			{
				monoScript = buffer;
				return false;
			}

			monoScript = null;
			return false;
		}

		public static bool IsValueSettable(this SerializedProperty self, object value)
		{
			if (self.IsNullOrDisposed())
			{
				throw new ArgumentException(nameof(self));
			}

			var valueType = value.GetType();
			var isSettable = self.IsValueSettable(valueType);
			return isSettable;
		}

		public static bool IsValueSettable(this SerializedProperty self, Type valueType)
		{
			if (self.IsNullOrDisposed())
			{
				throw new ArgumentException(nameof(self));
			}

			if (!self.TryGetPropertyType(out var type))
			{
				return false;
			}

			var isAssignable = type.IsAssignableFrom(valueType);
			return isAssignable;
		}

		public static bool IsValueSettable<TType>(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				throw new ArgumentException(nameof(self));
			}

			var isSettable = self.IsValueSettable(typeof(TType));
			return isSettable;
		}

		public static SerializedProperty GetPropertyParent(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return null;
			}

			var parents = self.GetPropertyParents();
			var parent = parents.LastOrDefault();
			return parent;
		}

		public static SerializedProperty GetNonCollectionParent(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return null;
			}

			var parents = self.GetPropertyParents()
				.ToList();

			for (var index = parents.Count - 1; index >= 0; index--)
			{
				var parent = parents[index];

				if (parent.IsArrayElement())
				{
					continue;
				}

				return parent;
			}

			return null;
		}

		public static bool TryGetPropertyValue(this SerializedProperty self, out object value)
		{
			object bufferValue = null;
			var valueFound = false;

			switch (self.propertyType)
			{
				case SerializedPropertyType.Generic:
					bufferValue = self.boxedValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Integer:
					bufferValue = self.intValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Boolean:
					bufferValue = self.boolValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Float:
					bufferValue = self.floatValue;
					valueFound = true;
					break;
				case SerializedPropertyType.String:
					bufferValue = self.stringValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Color:
					bufferValue = self.colorValue;
					valueFound = true;
					break;
				case SerializedPropertyType.ObjectReference:
					bufferValue = self.objectReferenceValue;
					valueFound = true;
					break;
				case SerializedPropertyType.LayerMask:
					bufferValue = self.intValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Enum:
					bufferValue = self.intValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Vector2:
					bufferValue = self.vector2Value;
					valueFound = true;
					break;
				case SerializedPropertyType.Vector3:
					bufferValue = self.vector3Value;
					valueFound = true;
					break;
				case SerializedPropertyType.Vector4:
					bufferValue = self.vector4Value;
					valueFound = true;
					break;
				case SerializedPropertyType.Rect:
					bufferValue = self.rectValue;
					valueFound = true;
					break;
				case SerializedPropertyType.ArraySize:
					bufferValue = self.arraySize;
					valueFound = true;
					break;
				case SerializedPropertyType.Character:
					bufferValue = self.stringValue;
					valueFound = true;
					break;
				case SerializedPropertyType.AnimationCurve:
					bufferValue = self.animationCurveValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Bounds:
					bufferValue = self.boundsValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Gradient:
					bufferValue = self.gradientValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Quaternion:
					bufferValue = self.quaternionValue;
					valueFound = true;
					break;
				case SerializedPropertyType.ExposedReference:
					bufferValue = self.exposedReferenceValue;
					valueFound = true;
					break;
				case SerializedPropertyType.FixedBufferSize:
					bufferValue = self.fixedBufferSize;
					valueFound = true;
					break;
				case SerializedPropertyType.Vector2Int:
					bufferValue = self.vector2IntValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Vector3Int:
					bufferValue = self.vector3IntValue;
					valueFound = true;
					break;
				case SerializedPropertyType.RectInt:
					bufferValue = self.rectIntValue;
					valueFound = true;
					break;
				case SerializedPropertyType.BoundsInt:
					bufferValue = self.boundsIntValue;
					valueFound = true;
					break;
				case SerializedPropertyType.ManagedReference:
					bufferValue = self.managedReferenceValue;
					valueFound = true;
					break;
				case SerializedPropertyType.Hash128:
					bufferValue = self.hash128Value;
					valueFound = true;
					break;
				case SerializedPropertyType.RenderingLayerMask:
					bufferValue = self.intValue;
					valueFound = true;
					break;
				case SerializedPropertyType.EntityId:
					bufferValue = self.entityIdValue;
					valueFound = true;
					break;
				default: throw new ArgumentOutOfRangeException();
			}

			if (valueFound)
			{
				value = bufferValue;
				return true;
			}

			value = null;
			return false;
		}

		public static bool TryGetValue(this SerializedProperty self, out object value)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				value = null;
				return true;
			}

			object container = self.serializedObject.targetObject;
			var parents = self.GetPropertyParents();

			foreach (var property in parents)
			{
				if (TryGetValue(property, container, out var bufferValue))
				{
					container = bufferValue;
				}
			}

			if (TryGetValue(self, container, out var finalValue))
			{
				value = finalValue;
				return true;
			}

			value = null;
			return false;
		}

		public static bool TrySetValue(this SerializedProperty self, object value)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return false;
			}

			Undo.RecordObject(self.serializedObject.targetObject, $"Set {self.name}");

			if (!self.TrySetValueNoRecord(value))
			{
				return false;
			}

			EditorUtility.SetDirty(self.serializedObject.targetObject);
			self.serializedObject.ApplyModifiedProperties();
			return true;
		}

		public static bool TrySetValueNoRecord(this SerializedProperty self, object value)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return false;
			}

			if (!self.TryGetParent(out var parent))
			{
				return false;
			}

			if (TrySetValue(self, parent, value))
			{
				return true;
			}

			return false;
		}

		private static bool TryGetValue(SerializedProperty property, object parent, out object value)
		{
			if (property.IsArrayElement())
			{
				if (!property.TryGetArrayIndex(out var index))
				{
					value = null;
					return false;
				}

				if (parent is IList list)
				{
					value = list[index];
					return true;
				}

				value = null;
				return false;
			}

			if (!TryGetMember(property, parent, out var bufferMember))
			{
				value = null;
				return false;
			}

			if (!bufferMember.TryGetValue(parent, out var memberValue))
			{
				value = null;
				return false;
			}

			value = memberValue;
			return true;
		}

		private static bool TrySetValue(SerializedProperty property, object parent, object value)
		{
			if (property.IsArrayElement())
			{
				if (!property.TryGetArrayIndex(out var index))
				{
					return false;
				}

				if (parent is IList list)
				{
					list[index] = value;
					return true;
				}

				return false;
			}

			if (!TryGetMember(property, parent, out var member))
			{
				return false;
			}

			if (member.TrySetValue(parent, value))
			{
				return true;
			}

			return false;
		}

		public static bool TryGetMember(this SerializedProperty self, out MemberInfo member)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				member = null;
				return false;
			}

			if (!self.TryGetParent(out var parent))
			{
				member = null;
				return false;
			}

			if (!TryGetMember(self, parent, out var bufferMember))
			{
				member = null;
				return false;
			}

			member = bufferMember;
			return true;
		}

		private static bool TryGetMember(SerializedProperty property, object parent, out MemberInfo member)
		{
			var propertyName = property.name;

			var containerType = parent.GetType();
			var bufferMember = containerType.GetMemberByNameRecursive(propertyName);

			if (bufferMember == null)
			{
				member = null;
				return false;
			}

			member = bufferMember;
			return true;
		}

		public static bool TryGetMember<TMember>(this SerializedProperty self, out TMember member)
			where TMember : MemberInfo
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				member = null;
				return false;
			}

			if (self.TryGetMember(out var bufferMember)
				&& bufferMember is TMember targetMember)
			{
				member = targetMember;
				return true;
			}

			member = null;
			return false;
		}

		public static bool TryGetParent(this SerializedProperty self, out object parent)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				parent = null;
				return false;
			}

			object bufferContainer = self.serializedObject.targetObject;
			var parents = self.GetPropertyParents();

			foreach (var property in parents)
			{
				if (property.IsPathEquals(self))
				{
					continue;
				}

				if (TryGetValue(property, bufferContainer, out var bufferValue))
				{
					bufferContainer = bufferValue;
				}
			}

			parent = bufferContainer;
			return true;
		}

		public static IEnumerable<SerializedProperty> GetPropertyParents(this SerializedProperty self)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return Enumerable.Empty<SerializedProperty>();
			}

			var path = self.propertyPath;

			var tokens = PropertiesSplitRegex.Matches(path)
				.Where(match => match.Success)
				.Select(match => match.Value)
				.ToList();

			if (tokens.Count <= 0)
			{
				return Enumerable.Empty<SerializedProperty>();
			}

			var serializedObject = self.serializedObject;
			var current = tokens[0];
			var currentProperty = serializedObject.FindProperty(current);

			if (currentProperty == null)
			{
				return Enumerable.Empty<SerializedProperty>();
			}

			if (currentProperty.IsPathEquals(self))
			{
				return Enumerable.Empty<SerializedProperty>();
			}

			var result = new List<SerializedProperty>();
			var copyBuffer = currentProperty.Copy();
			result.Add(copyBuffer);

			for (var i = 1; i < tokens.Count - 1; i++)
			{
				current = tokens[i];
				currentProperty = currentProperty.FindPropertyRelative(current);

				if (currentProperty == null)
				{
					break;
				}

				copyBuffer = currentProperty.Copy();
				result.Add(copyBuffer);
			}

			return result;
		}

		public static bool TryGetTargetObjectType(this SerializedProperty self, out Type type)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				type = null;
				return false;
			}

			var serializedObject = self.serializedObject;
			var targetObject = serializedObject.targetObject;

			if (targetObject != null)
			{
				type = targetObject.GetType();
				return false;
			}

			var scriptProperty = serializedObject.FindProperty(ScriptPropertyName);

			if (!scriptProperty.TryGetMonoScript(out var monoScript))
			{
				type = null;
				return false;
			}

			type = monoScript.GetClass();
			return false;
		}

		public static bool TryGetTargetObjectComponent(this SerializedProperty self, out Component component)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				component = null;
				return false;
			}

			if (self.serializedObject.targetObject is Component inner)
			{
				component = inner;
				return true;
			}

			component = null;
			return false;
		}

		public static bool IsPathEquals(this SerializedProperty self, SerializedProperty other)
		{
			if (self.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return false;
			}

			if (!self.Verify())
			{
				DebugUtility.LogException<ArgumentException>(nameof(self));
				return false;
			}

			if (other.IsNullOrDisposed())
			{
				DebugUtility.LogException<ArgumentException>(nameof(other));
				return false;
			}

			if (!other.Verify())
			{
				DebugUtility.LogException<ArgumentException>(nameof(other));
				return false;
			}

			var isEquals = self.propertyPath.Equals(other.propertyPath);
			return isEquals;
		}

		public static Editor FindOrDefaultRootEditor(this SerializedProperty self)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			var serializedObject = self.serializedObject;
			var activeEditors = ActiveEditorTracker.sharedTracker.activeEditors;
			var firstActive = activeEditors.FirstOrDefault(editor => editor.serializedObject == serializedObject);
			return firstActive;
		}
	}
}