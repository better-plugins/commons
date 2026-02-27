using System;
using Better.Commons.Runtime.Utilities;
using UnityEditor;

namespace Better.Commons.EditorAddons.Utilities
{
	public static class SettingsServiceUtility
	{
		public const char PathSeparator = CharUtility.Slash;
		
		public const string ProjectCategory = "Project/";
		public const string PreferencesCategory = "Preferences/";

		public static EditorWindow OpenProjectSettings(string relativeSettingsPath)
		{
			if (string.IsNullOrWhiteSpace(relativeSettingsPath))
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(relativeSettingsPath));
				return null;
			}

			relativeSettingsPath = PathUtility.EnsureRoot(relativeSettingsPath, ProjectCategory);
			relativeSettingsPath = PathUtility.StandardizeSeparators(relativeSettingsPath, PathSeparator);

			var window = SettingsService.OpenProjectSettings(relativeSettingsPath);
			return window;
		}

		public static EditorWindow OpenUserPreferences(string relativeSettingsPath)
		{
			if (string.IsNullOrWhiteSpace(relativeSettingsPath))
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(relativeSettingsPath));
				return null;
			}

			relativeSettingsPath = PathUtility.EnsureRoot(relativeSettingsPath, PreferencesCategory);
			var window = SettingsService.OpenUserPreferences(relativeSettingsPath);
			return window;
		}
	}
}