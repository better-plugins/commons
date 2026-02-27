using System;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class EnumExtensions
	{
		public static int GetFlagInt(this Enum self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return -1;
			}

			var type = self.GetType();
			var underlyingType = Enum.GetUnderlyingType(type);

			if (underlyingType == typeof(ulong))
			{
				var uIntResult = (int)Convert.ToUInt64(self);
				return uIntResult;
			}

			var intType = (int)Convert.ToInt64(self);
			return intType;
		}

		public static bool IsFlagAll(this Enum self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var everythingFlag = EnumUtility.EverythingFlag(self.GetType());
			var isFlagAll = Equals(self, everythingFlag);
			return isFlagAll;
		}

		public static bool IsDefaultFlag(this Enum self)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var flagValue = self.GetFlagInt();
			var isDefaultFlag = flagValue == EnumUtility.DefaultIntFlag;
			return isDefaultFlag;
		}

		public static TEnum Add<TEnum>(this TEnum self, TEnum value)
			where TEnum : Enum
		{
			var result = (TEnum)EnumUtility.Add(self, value);
			return result;
		}

		public static TEnum Remove<TEnum>(this TEnum self, TEnum value)
			where TEnum : Enum
		{
			var result = (TEnum)EnumUtility.Remove(self, value);
			return result;
		}
	}
}