using System;
using System.IO;

namespace Better.Commons.Runtime.Utilities
{
	public static class PathUtility
	{
		public static readonly char[] PathSeparators;

		static PathUtility()
		{
			PathSeparators = new[]
			{
				Path.PathSeparator,
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar,
			};
		}

		public static string GetFileDirectory(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(path));
				return path;
			}

			if (Path.HasExtension(path))
			{
				var directoryPath = Path.GetDirectoryName(path);
				return directoryPath;
			}

			return path;
		}

		public static string EnsureRoot(string path, string root)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(path));
				return path;
			}

			path = path.TrimStart(PathSeparators);
			root = root.Trim(PathSeparators);

			if (!path.StartsWith(root))
			{
				path = Path.Combine(root, path);
			}

			return path;
		}

		public static string StandardizeSeparators(string path, char separator)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(path));
				return path;
			}

			for (var i = 0; i < PathSeparators.Length; i++)
			{
				path = path.Replace(PathSeparators[i], separator);
			}

			return path;
		}

		public static string StandardizeSeparators(string path)
		{
			var standardizedPath = StandardizeSeparators(path, Path.PathSeparator);
			return standardizedPath;
		}

		public static string[] Split(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(path));
				return Array.Empty<string>();
			}

			var parts = path.Split(PathSeparators, StringSplitOptions.RemoveEmptyEntries);
			return parts;
		}
	}
}