using System;
using System.Collections.Generic;
using System.Reflection;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class TypeExtensions
	{
		public static bool TryGetCustomAttribute<TAttribute>(this Type self, bool inherit, out TAttribute attribute)
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

		public static bool IsDefined<TAttribute>(this Type self, bool inherit)
			where TAttribute : Attribute
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var attributeType = typeof(TAttribute);
			var isDefined = self.IsDefined(attributeType, inherit);

			return isDefined;
		}

		public static bool HasParameterlessConstructor(this Type self)
		{
			var hasParameterlessConstructor = ReflectionUtility.HasParameterlessConstructor(self);
			return hasParameterlessConstructor;
		}

		public static bool IsArrayOrList(this Type self)
		{
			var isArrayOrList = ReflectionUtility.IsArrayOrList(self);
			return isArrayOrList;
		}

		public static bool IsEnumerable(this Type self)
		{
			var isEnumerable = ReflectionUtility.IsEnumerable(self);
			return isEnumerable;
		}

		public static bool IsAssignableFrom<T>(this Type self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var isAssignable = self.IsAssignableFrom(typeof(T));
			return isAssignable;
		}

		public static bool IsAssignableFromRawGeneric(this Type self, Type type)
		{
			var isAssignable = ReflectionUtility.IsAssignableFromRawGeneric(self, type);
			return isAssignable;
		}

		public static bool IsAssignableFromRawGeneric<T>(this Type self)
		{
			var isAssignable = ReflectionUtility.IsAssignableFromRawGeneric<T>(self);
			return isAssignable;
		}

		public static bool IsGeneric(this Type self, Type type)
		{
			var isGeneric = ReflectionUtility.IsGeneric(self, type);
			return isGeneric;
		}

		public static bool IsGeneric<T>(this Type self)
		{
			var isGeneric = ReflectionUtility.IsGeneric<T>(self);
			return isGeneric;
		}

		public static bool IsEnum(this Type self)
		{
			var isFlagsEnum = ReflectionUtility.IsFlagsEnum(self);
			return isFlagsEnum;
		}

		public static object GetDefault(this Type self)
		{
			var isDefault = ReflectionUtility.GetDefault(self);
			return isDefault;
		}

		public static IEnumerable<Type> GetAllInheritedTypes(this Type self)
		{
			var inheritedTypes = ReflectionUtility.GetAllInheritedTypes(self);
			return inheritedTypes;
		}

		public static IEnumerable<Type> GetAllInheritedTypes(this Type self, params Type[] excludes)
		{
			var inheritedTypes = ReflectionUtility.GetAllInheritedTypes(self, excludes);
			return inheritedTypes;
		}

		public static bool IsAnonymous(this Type self)
		{
			var isAnonymous = ReflectionUtility.IsAnonymous(self);
			return isAnonymous;
		}

		public static IEnumerable<Type> GetAllInheritedTypesOfRawGeneric(this Type self)
		{
			var inheritedTypes = ReflectionUtility.GetAllInheritedTypesOfRawGeneric(self);
			return inheritedTypes;
		}

		public static IEnumerable<Type> GetAllInheritedTypesOfRawGeneric(this Type self, params Type[] excludes)
		{
			var inheritedTypes = ReflectionUtility.GetAllInheritedTypesOfRawGeneric(self, excludes);
			return inheritedTypes;
		}

		public static bool IsSubclassOf<T>(this Type self)
		{
			var isSubclass = ReflectionUtility.IsSubclassOf<T>(self);
			return isSubclass;
		}

		public static bool IsSubclassOfAny(this Type self, IEnumerable<Type> anyTypes)
		{
			var isSubclass = ReflectionUtility.IsSubclassOfAny(self, anyTypes);
			return isSubclass;
		}

		public static bool IsSubclassOfRawGeneric(this Type self, Type genericType)
		{
			var isSubclass = ReflectionUtility.IsSubclassOfRawGeneric(self, genericType);
			return isSubclass;
		}

		public static bool IsSubclassOfAnyRawGeneric(this Type self, IEnumerable<Type> genericTypes)
		{
			var isSubclass = ReflectionUtility.IsSubclassOfAnyRawGeneric(self, genericTypes);
			return isSubclass;
		}

		public static Type GetCollectionElementType(this Type self)
		{
			var elementType = ReflectionUtility.GetCollectionElementType(self);
			return elementType;
		}

		public static bool IsNullable(this Type self)
		{
			var isNullable = ReflectionUtility.IsNullable(self);
			return isNullable;
		}

		public static IEnumerable<MemberInfo> GetMembersRecursive(this Type type)
		{
			var member = ReflectionUtility.GetMembersRecursive(type);
			return member;
		}

		public static MemberInfo GetMemberByNameRecursive(this Type type, string memberName)
		{
			var member = ReflectionUtility.GetMemberByNameRecursive(type, memberName);
			return member;
		}

		public static IEnumerable<MemberInfo> GetMembersByName(this Type type, string memberName)
		{
			var members =  ReflectionUtility.GetMembersByName(type, memberName);
			return members;
		}

		public static IEnumerable<MemberInfo> GetMembersByNameRecursive(this Type type, string memberName)
		{
			var member = ReflectionUtility.GetMembersByNameRecursive(type, memberName);
			return member;
		}
	}
}