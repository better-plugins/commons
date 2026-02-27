using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Extensions;

namespace Better.Commons.Runtime.Utilities
{
	public static class EnumUtility
	{
		public const int DefaultIntFlag = 0;

		public static IEnumerable<Enum> GetAllValues(Type enumType)
		{
			if (enumType == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(enumType));
				return Enumerable.Empty<Enum>();
			}

			if (!enumType.IsEnum())
			{
				var message = $"{nameof(enumType)} must be provided {typeof(Enum)}";
				DebugUtility.LogException<ArgumentException>(message);
				return Enumerable.Empty<Enum>();
			}

			var values = Enum.GetValues(enumType)
				.ToEnumerable<Enum>();

			return values;
		}

		public static IEnumerable<TEnum> GetAllValues<TEnum>()
			where TEnum : Enum
		{
			var enumType = typeof(TEnum);

			var values = GetAllValues(enumType)
				.Cast<TEnum>();

			return values;
		}

		public static Enum EverythingFlag(Type enumType)
		{
			if (enumType == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(enumType));
				return null;
			}

			if (!enumType.IsEnum())
			{
				var message = $"{nameof(enumType)} must be provided {typeof(Enum)}";
				DebugUtility.LogException<ArgumentException>(message);
				return null;
			}

			long newValue = 0;
			var values = GetAllValues(enumType);

			foreach (var value in values)
			{
				var v = (long)Convert.ChangeType(value, TypeCode.Int64);

				if (v == 1
					|| v % 2 == 0)
				{
					newValue |= v;
				}
			}

			var everythingFlag = (Enum)Enum.ToObject(enumType, newValue);
			return everythingFlag;
		}

		public static TEnum EverythingFlag<TEnum>()
			where TEnum : Enum
		{
			var enumType = typeof(TEnum);
			var everythingFlag = (TEnum)EverythingFlag(enumType);
			return everythingFlag;
		}

		public static Enum Add(Enum a, Enum b)
		{
			if (a == null)
			{
				throw new ArgumentNullException(nameof(a));
			}

			if (b == null)
			{
				throw new ArgumentNullException(nameof(b));
			}

			if (!a.CompareTypes(b))
			{
				var message = $"Type of {a.ToString()} and {b.ToString()} is different";
				throw new ArgumentException(message);
			}

			var type = a.GetType();
			var aFlagValue = a.GetFlagInt();
			var bFlagValue = b.GetFlagInt();
			var flagValue = aFlagValue | bFlagValue;
			var result = (Enum)Enum.ToObject(type, flagValue);

			return result;
		}

		public static Enum Remove(Enum a, Enum b)
		{
			if (a == null)
			{
				throw new ArgumentNullException(nameof(a));
			}

			if (b == null)
			{
				throw new ArgumentNullException(nameof(b));
			}

			if (!a.CompareTypes(b))
			{
				var message = $"Type of {a.ToString()} and {b.ToString()} is different";
				throw new ArgumentException(message);
			}

			var type = a.GetType();
			var aFlagValue = a.GetFlagInt();
			var bFlagValue = b.GetFlagInt();
			var flagValue = aFlagValue & ~bFlagValue;
			var result = (Enum)Enum.ToObject(type, flagValue);

			return result;
		}

		public static Enum ToEnum(Type enumType, int value)
		{
			var result = (Enum)Enum.ToObject(enumType, value);
			return result;
		}

		public static bool HasValue(Enum currentValue, Enum value, bool isFlag)
		{
			if (isFlag)
			{
				var hasFlag = currentValue.HasFlag(value);
				return hasFlag;
			}

			var equal = Equals(currentValue, value);
			return equal;
		}
	}
}