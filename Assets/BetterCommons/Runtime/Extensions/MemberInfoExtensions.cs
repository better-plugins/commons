using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class MemberInfoExtensions
	{
		public static bool TryGetCustomAttribute<TAttribute>(this MemberInfo self, bool inherit, out TAttribute attribute)
			where TAttribute : Attribute
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				attribute = null;
				return false;
			}

			if (self.IsDefined<TAttribute>(inherit))
			{
				attribute = self.GetCustomAttribute<TAttribute>(inherit);
				return true;
			}

			attribute = null;
			return false;
		}

		public static bool TryGetCustomAttributes<TAttribute>(this MemberInfo self, bool inherit, out IEnumerable<TAttribute> attributes)
			where TAttribute : Attribute
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				attributes = null;
				return false;
			}

			if (self.IsDefined<TAttribute>(inherit))
			{
				attributes = self.GetCustomAttributes<TAttribute>(inherit);
				return true;
			}

			attributes = null;
			return false;
		}

		public static bool IsDefined<TAttribute>(this MemberInfo self, bool inherit)
			where TAttribute : Attribute
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var attributeMemberInfo = typeof(TAttribute);
			var isDefined = self.IsDefined(attributeMemberInfo, inherit);

			return isDefined;
		}

		public static bool TryGetValue(this MemberInfo self, object target, out object value)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				value = null;
				return false;
			}

			if (self is FieldInfo field)
			{
				value = field.GetValue(target);
				return true;
			}

			if (self is PropertyInfo property)
			{
				value = property.GetValue(target);
				return true;
			}

			if (self is MethodInfo memberInfo)
			{
				var parameters = memberInfo.GetParameters();

				if (parameters.Length == 0)
				{
					var arguments = Array.Empty<object>();
					value = memberInfo.Invoke(target, arguments);
					return true;
				}
			}

			value = null;
			return false;
		}

		public static bool TrySetValue(this MemberInfo self, object container, object value)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			Type valueType = null;

			if (value != null)
			{
				valueType = value.GetType();
			}

			if (valueType == null)
			{
				if (!self.TryGetMemberType(out var bufferType))
				{
					return false;
				}

				valueType = bufferType;
			}

			if (self is FieldInfo field)
			{
				var fieldType = field.FieldType;

				if (!fieldType.IsAssignableFrom(valueType))
				{
					return false;
				}

				if (!fieldType.IsValueType
					&& !fieldType.IsEnum)
				{
					field.SetValue(container, value);
				}
				else
				{
					field.SetValueDirect(__makeref(container), value);
				}

				return true;
			}

			if (self is PropertyInfo property)
			{
				var propertyType = property.PropertyType;

				if (!propertyType.IsAssignableFrom(valueType))
				{
					return false;
				}

				property.SetValue(container, value);
				return true;
			}

			if (self is MethodInfo memberInfo)
			{
				var parameter = memberInfo.GetParameters()
					.FirstOrDefault();

				if (parameter == null)
				{
					return false;
				}

				if (!parameter.ParameterType.IsAssignableFrom(valueType))
				{
					return false;
				}

				var arguments = new[]
				{
					value,
				};

				memberInfo.Invoke(container, arguments);
				return true;
			}

			return false;
		}

		public static bool TryGetMemberType(this MemberInfo self, out Type type)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				type = null;
				return false;
			}

			if (self is FieldInfo field)
			{
				type = field.FieldType;
				return true;
			}

			if (self is PropertyInfo property)
			{
				type = property.PropertyType;
				return true;
			}

			if (self is MethodInfo memberInfo)
			{
				type = memberInfo.ReturnType;
				return true;
			}

			type = null;
			return false;
		}

		public static bool IsStatic(this MemberInfo self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			if (self is MethodInfo memberInfo)
			{
				return memberInfo.IsStatic;
			}

			if (self is FieldInfo field)
			{
				return field.IsStatic;
			}

			if (self is PropertyInfo property)
			{
				var method = property.GetGetMethod();
				return method.IsStatic;
			}

			return false;
		}
	}
}