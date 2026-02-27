using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEditor;

namespace Better.Commons.EditorAddons.Utilities
{
	public static class TypeCacheUtility
	{
		public static IEnumerable<Type> GetAllInheritedTypes(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Array.Empty<Type>();
			}

			var types = TypeCache.GetTypesDerivedFrom(type)
				.Append(type)
				.Where(iterationType => ReflectionUtility.IsInheritedType(type, iterationType));

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
	}
}