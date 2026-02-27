using System;
using System.Linq;
using System.Text.RegularExpressions;
using Better.Commons.Runtime.Extensions;

namespace Better.Commons.Runtime.Utilities
{
	public static class StringUtility
	{
		private const string WordMatchRegexPattern = "[A-Z]+(?=[A-Z][a-z0-9])|[A-Z]?[a-z0-9]+";
		private const string PascalCaseMatchRegexPattern = "^[A-Z][a-z0-9]+(?:[A-Z][a-z0-9]+)*$";
		private const string SnakeCaseMatchRegexPattern = "^[a-z0-9]+(?:_[a-z0-9]+)*$";
		private const string KebabCaseMatchRegexPattern = "^[a-z0-9]+(?:-[a-z0-9]+)*$";
		private const string FormatPlaceholdersRegexPattern = @"\{(\d+)\}";

		public static Regex WordMatchRegex { get; }
		public static Regex PascalCaseMatchRegex { get; }
		public static Regex SnakeCaseMatchRegex { get; }
		public static Regex KebabCaseMatchRegex { get; }
		public static Regex FormatPlaceholdersRegex { get; }

		static StringUtility()
		{
			WordMatchRegex = new Regex(WordMatchRegexPattern, RegexOptions.Compiled);
			PascalCaseMatchRegex = new Regex(PascalCaseMatchRegexPattern, RegexOptions.Compiled);
			SnakeCaseMatchRegex = new Regex(SnakeCaseMatchRegexPattern, RegexOptions.Compiled);
			KebabCaseMatchRegex = new Regex(KebabCaseMatchRegexPattern, RegexOptions.Compiled);
			FormatPlaceholdersRegex = new Regex(FormatPlaceholdersRegexPattern, RegexOptions.Compiled);
		}

		public static string[] SplitWords(string input)
		{
			if (input.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return Array.Empty<string>();
			}

			if (input.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return Array.Empty<string>();
			}

			var words = WordMatchRegex.Matches(input)
				.Select(m => m.Value)
				.ToArray();

			return words;
		}

		public static string ToPascalCase(string input)
		{
			if (input.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return input;
			}

			if (input.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return input;
			}

			if (InPascalCase(input))
			{
				return input;
			}

			var result = SplitWords(input)
				.Select(w => w.ToLowerInvariant())
				.Select(w => w.FirstCharToUpper())
				.Join();

			return result;
		}

		public static string ToSnakeCase(string input)
		{
			if (input.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return input;
			}

			if (input.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return input;
			}

			if (InSnakeCase(input))
			{
				return input;
			}

			var result = SplitWords(input)
				.Select(w => w.ToLowerInvariant())
				.Join(CharUtility.Underscore);

			return result;
		}

		public static string ToKebabCase(string input)
		{
			if (input.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return input;
			}

			if (input.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return input;
			}

			if (InKebabCase(input))
			{
				return input;
			}

			var result = SplitWords(input)
				.Select(w => w.ToLowerInvariant())
				.Join(CharUtility.Hyphen);

			return result;
		}

		public static bool InKebabCase(string input)
		{
			var inCase = InCase(input, KebabCaseMatchRegex);
			return inCase;
		}

		public static bool InSnakeCase(string input)
		{
			var inCase = InCase(input, SnakeCaseMatchRegex);
			return inCase;
		}

		public static bool InPascalCase(string input)
		{
			var inCase = InCase(input, PascalCaseMatchRegex);
			return inCase;
		}

		private static bool InCase(string input, Regex matchRegex)
		{
			if (input.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return false;
			}

			if (input.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(input));
				return false;
			}

			var isMatch = matchRegex.IsMatch(input);
			return isMatch;
		}

		public static bool EqualsFormatPlaceholders(string source, string format)
		{
			if (source.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(source));
				return false;
			}

			if (format.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(source));
				return false;
			}

			var sourceCount = GetFormatPlaceholdersCount(source);

			if (sourceCount < 0)
			{
				return false;
			}

			var formatPlaceholders = EqualsFormatPlaceholders(format, sourceCount);

			return formatPlaceholders;
		}

		public static bool HasFormatPlaceholders(string source, int count)
		{
			if (source.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(source));
				return false;
			}

			var foundCount = GetFormatPlaceholdersCount(source);

			if (foundCount < 0)
			{
				return false;
			}

			var has = foundCount >= count;

			return has;
		}

		public static bool EqualsFormatPlaceholders(string source, int count)
		{
			if (source.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(source));
				return false;
			}

			var foundCount = GetFormatPlaceholdersCount(source);

			if (foundCount < 0)
			{
				return false;
			}

			var equals = foundCount == count;

			return equals;
		}

		public static int GetFormatPlaceholdersCount(string source)
		{
			if (source.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(source));
				return -1;
			}

			var sourcePlaceholders = FormatPlaceholdersRegex.Matches(source);
			return sourcePlaceholders.Count;
		}
	}
}