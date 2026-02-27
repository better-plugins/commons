using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEditor;

namespace Better.Commons.EditorAddons.Metadata
{
	public class SerializedObjectMetadata
	{
		private readonly SerializedObject _serializedObject;
		private readonly Dictionary<Type, MemberInfo[]> _memberInfosMap;

		public Editor RootEditor { get; }
		public Type TargetType { get; }

		public bool IsValid { get; private set; }

		public SerializedObjectMetadata(SerializedObject serializedObject, Editor rootEditor)
		{
			_serializedObject = serializedObject;
			_memberInfosMap = new Dictionary<Type, MemberInfo[]>();
			RootEditor = rootEditor;

			if (_serializedObject == null)
			{
				return;
			}

			if (!_serializedObject.ValidateTargets())
			{
				return;
			}

			foreach (var target in _serializedObject.targetObjects)
			{
				var targetType = target.GetType();
				var allMembers = targetType.GetMembers(BindingFlagsUtility.MethodFlags);

				if (_memberInfosMap.TryGetValue(targetType, out var members))
				{
					allMembers = members.Intersect(allMembers)
						.ToArray();

					_memberInfosMap[targetType] = allMembers;
				}
				else
				{
					_memberInfosMap.Add(targetType, allMembers);
				}
			}

			if (!ReflectionUtility.TryGetCommonBaseType(_serializedObject.targetObjects, out var commonBaseType))
			{
				return;
			}

			TargetType = commonBaseType;
			IsValid = true;
		}

		public SerializedObjectMetadata(SerializedObject serializedObject) : this(serializedObject, null)
		{
		}

		public bool TryGetAttributes(out Attribute[] attributes)
		{
			if (!Validation(true))
			{
				attributes = null;
				return false;
			}

			if (!_serializedObject.ValidateTargets())
			{
				attributes = null;
				return false;
			}

			if (ReflectionUtility.TryGetCommonBaseType(_serializedObject.targetObjects, out var baseType))
			{
				attributes = baseType.GetCustomAttributes()
					.ToArray();

				return true;
			}

			attributes = null;
			return false;
		}

		public bool TryGetSerializedProperties(out SerializedProperty[] properties, bool enterChildren = false)
		{
			if (!Validation(true))
			{
				properties = null;
				return false;
			}

			if (!_serializedObject.ValidateTargets())
			{
				properties = null;
				return false;
			}

			var serializedProperties = new List<SerializedProperty>();
			var iterator = _serializedObject.GetIterator();

			if (iterator.NextVisible(true))
			{
				do
				{
					var copy = iterator.Copy();
					serializedProperties.Add(copy);
				} while (iterator.NextVisible(enterChildren));
			}

			properties = serializedProperties.ToArray();
			return true;
		}

		public bool TryGetMembers<TMember>(out TMember[] memberInfos)
			where TMember : MemberInfo
		{
			if (!Validation(true))
			{
				memberInfos = null;
				return false;
			}

			if (!_serializedObject.ValidateTargets())
			{
				memberInfos = null;
				return false;
			}

			var bufferMembers = Enumerable.Empty<TMember>();

			foreach (var target in _serializedObject.targetObjects)
			{
				var targetType = target.GetType();

				if (!_memberInfosMap.TryGetValue(targetType, out var allMembers))
				{
					continue;
				}

				var members = allMembers.OfType<TMember>();
				bufferMembers = bufferMembers.Concat(members);
			}

			memberInfos = bufferMembers.Distinct()
				.ToArray();

			return true;
		}

		private bool Validation(bool targetState, bool logException = true)
		{
			var isValid = IsValid == targetState;

			if (!isValid && logException)
			{
				var reason = targetState ? "must be valid" : "must be invalid";
				var message = "Improper state, " + reason;
				DebugUtility.LogException<InvalidOperationException>(message);
			}

			return isValid;
		}
	}
}