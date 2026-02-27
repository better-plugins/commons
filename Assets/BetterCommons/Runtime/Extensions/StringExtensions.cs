using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string self)
		{
			var isNullOrEmpty = string.IsNullOrEmpty(self);
			return isNullOrEmpty;
		}

		public static bool IsNullOrWhiteSpace(this string self)
		{
			var isNullOrWhiteSpace = string.IsNullOrWhiteSpace(self);
			return isNullOrWhiteSpace;
		}

		public static bool HasAnySymbols(this string self)
		{
			if (self.IsNullOrEmpty())
			{
				return false;
			}

			if (self.IsNullOrWhiteSpace())
			{
				return false;
			}
			
			return true;
		}

		public static string Join(this IEnumerable<string> self, char separator)
		{
			var result = string.Join(separator, self);
			return result;
		}

		public static string Join(this IEnumerable<string> self, string separator)
		{
			var result = string.Join(separator, self);
			return result;
		}

		public static string Join(this IEnumerable<string> self)
		{
			var result = self.Join(string.Empty);
			return result;
		}

		public static string[] SplitWords(this string self)
		{
			var words = StringUtility.SplitWords(self);
			return words;
		}

		public static string ReplaceChar(this string self, int index, char value)
		{
			if (self.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return self;
			}

			var chars = self.ToCharArray();
			chars.ReplaceElement(index, value);

			var result = new string(chars);
			return result;
		}

		public static string ReplaceFirstChar(this string self, char value)
		{
			var index = 0;
			var result = self.ReplaceChar(index, value);

			return result;
		}

		public static string ReplaceLastChar(this string self, char value)
		{
			var index = self.Length - 1;
			var result = self.ReplaceChar(index, value);

			return result;
		}

		public static string FirstCharToUpper(this string self)
		{
			if (self.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return self;
			}

			var firstChar = self.First()
				.GetUpperCase();

			var result = self.ReplaceFirstChar(firstChar);
			return result;
		}

		public static bool CompareOrdinal(this string self, string other)
		{
			var ordinals = string.CompareOrdinal(self, other);
			var equal = ordinals == 0;

			return equal;
		}

		public static string ToPascalCase(this string self)
		{
			var result = StringUtility.ToPascalCase(self);
			return result;
		}

		public static string ToSnakeCase(this string self)
		{
			var result = StringUtility.ToSnakeCase(self);
			return result;
		}

		public static string ToKebabCase(this string self)
		{
			var result = StringUtility.ToKebabCase(self);
			return result;
		}

		public static bool InKebabCase(this string self)
		{
			var inCase = StringUtility.InKebabCase(self);
			return inCase;
		}

		public static bool InSnakeCase(this string self)
		{
			var inCase = StringUtility.InSnakeCase(self);
			return inCase;
		}

		public static bool InPascalCase(this string self)
		{
			var inCase = StringUtility.InPascalCase(self);
			return inCase;
		}

		public static bool EqualsFormatPlaceholders(this string self, string format)
		{
			var equals = StringUtility.EqualsFormatPlaceholders(self, format);
			return equals;
		}

		public static bool HasFormatPlaceholders(string self, int count)
		{
			var has = StringUtility.HasFormatPlaceholders(self, count);
			return has;
		}

		public static bool EqualsFormatPlaceholders(string self, int count)
		{
			var equals = StringUtility.EqualsFormatPlaceholders(self, count);
			return equals;
		}

		public static int GetFormatPlaceholdersCount(string self)
		{
			var count = StringUtility.GetFormatPlaceholdersCount(self);
			return count;
		}
	}
}