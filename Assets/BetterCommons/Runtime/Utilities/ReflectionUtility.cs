using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Better.Commons.Runtime.Comparers;
using Better.Commons.Runtime.Extensions;

namespace Better.Commons.Runtime.Utilities
{
	public static class ReflectionUtility
	{
		public static bool HasParameterlessConstructor(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (type.IsValueType)
			{
				return true;
			}

			var constructor = type.GetConstructor(BindingFlagsUtility.ConstructorFlags, null, Type.EmptyTypes, null);
			var hasConstructor = constructor != null;
			return hasConstructor;
		}

		public static bool IsArrayOrList(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (type.IsArray)
			{
				return true;
			}

			var listType = typeof(List<>);
			var isAssignable = IsAssignableFromRawGeneric(listType, type);
			return isAssignable;
		}

		public static bool IsEnumerable(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (type.IsArray)
			{
				return true;
			}

			var enumerableType = typeof(IEnumerable<>);
			var isAssignable = IsAssignableFromRawGeneric(enumerableType, type);
			return isAssignable;
		}

		public static bool IsAssignableFrom<T>(Type assignableType)
		{
			if (assignableType == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(assignableType));
				return false;
			}

			var type = typeof(T);
			var isAssignable = type.IsAssignableFrom(assignableType);
			return isAssignable;
		}

		public static bool IsAssignableFromRawGeneric(Type type, Type assignableType)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (assignableType == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(assignableType));
				return false;
			}

			var bufferType = assignableType;

			while (bufferType.BaseType != null)
			{
				if (IsGeneric(bufferType, type))
				{
					return true;
				}

				var interfaceTypes = bufferType.GetInterfaces();

				foreach (var interfaceType in interfaceTypes)
				{
					if (!IsGeneric(interfaceType, type))
					{
						continue;
					}

					return true;
				}

				bufferType = bufferType.BaseType;
			}

			return false;
		}

		public static bool IsAssignableFromRawGeneric<TAssignable>(Type type)
		{
			var isAssignable = IsAssignableFromRawGeneric(type, typeof(TAssignable));
			return isAssignable;
		}

		public static bool IsGeneric(Type type, Type genericType)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (type.IsGenericType)
			{
				var genericTypeDefinition = type.GetGenericTypeDefinition();

				if (genericTypeDefinition == genericType)
				{
					return true;
				}
			}

			return false;
		}

		public static bool IsGeneric<T>(Type self)
		{
			var isGeneric = IsGeneric(self, typeof(T));
			return isGeneric;
		}

		public static bool IsFlagsEnum(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (!type.IsEnum)
			{
				return false;
			}

			var flagsAttribute = type.GetCustomAttribute<FlagsAttribute>();
			var isFlags = flagsAttribute != null;
			return isFlags;
		}

		public static object GetDefault(Type type)
		{
			if (type == null)
			{
				return null;
			}

			if (type.IsValueType)
			{
				var activatedInstance = Activator.CreateInstance(type);
				return activatedInstance;
			}

			if (type == typeof(string))
			{
				return string.Empty;
			}

			return null;
		}

		public static bool IsInheritedType(Type baseType, Type derivedType)
		{
			if (baseType == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(baseType));
				return false;
			}

			if (derivedType == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(derivedType));
				return false;
			}

			if (!baseType.IsAssignableFrom(derivedType))
			{
				return false;
			}

			var isInherited = (derivedType.IsClass || derivedType.IsValueType) && !derivedType.IsAbstract;
			return isInherited;
		}

		public static IEnumerable<Type> GetAllInheritedTypes(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Array.Empty<Type>();
			}

			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(iterationType => IsInheritedType(type, iterationType));

			return types;
		}

		public static IEnumerable<Type> GetAllInheritedTypes<T>()
		{
			var types = GetAllInheritedTypes(typeof(T));
			return types;
		}

		public static IEnumerable<Type> GetAllInheritedTypes(Type type, params Type[] excludes)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Array.Empty<Type>();
			}

			foreach (var exclude in excludes)
			{
				if (type.IsSubclassOf(exclude))
				{
					return Enumerable.Empty<Type>();
				}
			}

			var allInheritedTypes = GetAllInheritedTypes(type);

			allInheritedTypes = allInheritedTypes.Where(inheritedType => !excludes.Any(exclude => exclude.IsAssignableFrom(inheritedType) || inheritedType.IsSubclassOf(exclude)))
				.ToArray();

			return allInheritedTypes;
		}

		public static IEnumerable<Type> GetAllInheritedTypes<T>(params Type[] excludes)
		{
			var types = GetAllInheritedTypes(typeof(T), excludes);
			return types;
		}

		public static IEnumerable<Type> GetAllInheritedTypesOfRawGeneric(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Array.Empty<Type>();
			}

			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(iterationType => IsSubclassOfRawGeneric(iterationType, type));

			return types;
		}

		public static IEnumerable<Type> GetAllInheritedTypesOfRawGeneric<T>()
		{
			var types = GetAllInheritedTypesOfRawGeneric(typeof(T));
			return types;
		}

		public static IEnumerable<Type> GetAllInheritedTypesOfRawGeneric(Type type, params Type[] excludes)
		{
			return GetAllInheritedTypesOfRawGeneric(type)
				.Except(excludes);
		}

		public static IEnumerable<Type> GetAllInheritedTypesOfRawGeneric<T>(params Type[] excludes)
		{
			var types = GetAllInheritedTypesOfRawGeneric(typeof(T), excludes);
			return types;
		}

		public static bool IsAnonymous(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (type.IsClass
				&& type.IsSealed
				&& type.Attributes.HasFlag(TypeAttributes.NotPublic))
			{
				var attributes = type.GetCustomAttribute<CompilerGeneratedAttribute>(false);

				if (attributes != null)
				{
					return true;
				}
			}

			return false;
		}

		public static bool IsAnonymous<T>()
		{
			var isAnonymous = IsAnonymous(typeof(T));
			return isAnonymous;
		}

		public static bool IsSubclassOf<T>(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			var genericType = typeof(T);
			var isSubclass = type.IsSubclassOf(genericType);
			return isSubclass;
		}

		public static bool IsSubclassOfAny(Type type, IEnumerable<Type> anyTypes)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (anyTypes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(anyTypes));
				return false;
			}

			foreach (var anyType in anyTypes)
			{
				if (anyType != null
					&& type.IsSubclassOf(anyType))
				{
					return true;
				}
			}

			return false;
		}

		public static bool IsSubclassOfRawGeneric(Type type, Type genericType)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (genericType == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(genericType));
				return false;
			}

			while (type != null
					&& type != typeof(object))
			{
				var definition = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

				if (genericType == definition
					&& type != genericType)
				{
					return true;
				}

				type = type.BaseType;
			}

			return false;
		}

		public static bool IsSubclassOfAnyRawGeneric(Type type, IEnumerable<Type> genericTypes)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return false;
			}

			if (genericTypes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(genericTypes));
				return false;
			}

			foreach (var anyType in genericTypes)
			{
				if (anyType != null
					&& IsSubclassOfRawGeneric(type, anyType))
				{
					return true;
				}
			}

			return false;
		}

		public static Type GetCollectionElementType(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return null;
			}

			if (type.IsArray)
			{
				var elementType = type.GetElementType();
				return elementType;
			}

			if (IsEnumerable(type))
			{
				var genericArgument = type.GetGenericArguments()[0];
				return genericArgument;
			}

			return null;
		}

		public static bool IsNullable(Type type)
		{
			if (type == null)
			{
				return true;
			}

			if (!type.IsValueType)
			{
				return true;
			}

			var underlyingType = Nullable.GetUnderlyingType(type);
			var hasUnderlyingType = underlyingType != null;
			return hasUnderlyingType;
		}

		public static bool IsNullable<T>()
		{
			var isNullable = IsNullable(typeof(T));

			return isNullable;
		}

		public static IEnumerable<MemberInfo> GetMembers(Type type)
		{
			const BindingFlags bindingFlags = BindingFlagsUtility.MethodFlags & ~BindingFlags.DeclaredOnly;

			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Enumerable.Empty<MemberInfo>();
			}

			var comparer = new MemberInfoComparer();
			var members = new HashSet<MemberInfo>(comparer);

			var isConstructedGenericType = type.IsGenericType && !type.IsGenericTypeDefinition;
			var typeToReflect = isConstructedGenericType ? type.GetGenericTypeDefinition() : type;

			foreach (var member in typeToReflect.GetMembers(bindingFlags))
			{
				if (!type.IsGenericType
					|| type.IsGenericTypeDefinition)
				{
					members.Add(member);
					continue;
				}

				if (TryConvertToConstructedGenericType(member, type, out var convertedMember))
				{
					members.Add(convertedMember);
				}
			}

			return members;
		}

		public static IEnumerable<MemberInfo> GetMembersRecursive(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Enumerable.Empty<MemberInfo>();
			}

			var comparer = new MemberInfoComparer();
			var members = new HashSet<MemberInfo>(comparer);

			do
			{
				var currentMembers = GetMembers(type);
				members.UnionWith(currentMembers);
				type = type.BaseType;
			} while (type != null);

			return members;
		}

		private static bool TryConvertToConstructedGenericType(MemberInfo memberInfo, Type constructedType, out MemberInfo convertedMember)
		{
			const BindingFlags bindingFlags = BindingFlagsUtility.MethodFlags & ~BindingFlags.DeclaredOnly;

			if (memberInfo.DeclaringType != null
				&& memberInfo.DeclaringType.IsGenericTypeDefinition)
			{
				var members = constructedType.GetMember(memberInfo.Name, bindingFlags);
				convertedMember = members.FirstOrDefault();
				return convertedMember != null;
			}

			convertedMember = memberInfo;
			return true;
		}

		public static IEnumerable<MemberInfo> GetMembersByName(Type type, string name)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Enumerable.Empty<MemberInfo>();
			}

			if (name == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(name));
				return Enumerable.Empty<MemberInfo>();
			}

			var members = GetMembers(type)
				.Where(member => member.Name == name);

			return members;
		}

		public static MemberInfo GetMemberByNameRecursive(Type type, string memberName)
		{
			var member = GetMembersByNameRecursive(type, memberName)
				.FirstOrDefault(member => member.Name == memberName);

			return member;
		}

		public static IEnumerable<MemberInfo> GetMembersByNameRecursive(Type type, string memberName)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Enumerable.Empty<MemberInfo>();
			}

			if (string.IsNullOrEmpty(memberName))
			{
				DebugUtility.LogException<ArgumentException>(nameof(memberName));
				return Enumerable.Empty<MemberInfo>();
			}

			var members = GetMembersRecursive(type)
				.Where(m => m.Name == memberName);

			return members;
		}

		public static bool TryGetCommonBaseType<T>(IEnumerable<T> objects, out Type baseType)
		{
			if (objects.IsNullOrEmpty())
			{
				baseType = null;
				return false;
			}

			var types = objects.Where(reference => reference != null)
				.Select(reference => reference.GetType());

			if (TryGetCommonBaseType(types, out var bufferType))
			{
				baseType = bufferType;
				return true;
			}

			baseType = null;
			return false;
		}

		public static bool TryGetCommonBaseType(IEnumerable<Type> types, out Type baseType)
		{
			if (types.IsNullOrEmpty())
			{
				baseType = null;
				return false;
			}

			var typeList = types.Distinct()
				.ToList();

			if (typeList.Count == 0)
			{
				baseType = null;
				return false;
			}

			var bufferType = typeList[0];

			while (bufferType != typeof(object)
					&& typeList.Any(type => !bufferType.IsAssignableFrom(type)))
			{
				bufferType = bufferType.BaseType;

				if (bufferType == null)
				{
					baseType = null;
					return false;
				}
			}

			baseType = bufferType;
			return true;
		}

		public static bool TryFindAssemblyByName(string name, out Assembly assembly)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			var foundAssembly = assemblies.SingleOrDefault(assembly =>
			{
				var assemblyInfo = assembly.GetName();

				var assemblyName = assemblyInfo.Name;

				return assemblyName == name;
			});

			if (foundAssembly == null)
			{
				assembly = null;
				return false;
			}

			assembly = foundAssembly;
			return true;
		}
	}
}