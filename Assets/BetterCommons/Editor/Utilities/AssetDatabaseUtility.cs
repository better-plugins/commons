using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Better.Commons.EditorAddons.Utilities
{
	public static class AssetDatabaseUtility
	{
		public const string AssetsFolder = "Assets";
		public const string AssetExtension = ".asset";

		public const string PrefabFilter = TypeFilterPrefix + "prefab";

		private const string TypeFilterPrefix = "t:";
		private const string LabelFilterPrefix = "l:";

		public static Object[] FindAssetsOfType(Type type, string filter)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Array.Empty<Object>();
			}

			var assets = new List<Object>();
			var guids = AssetDatabase.FindAssets(filter);

			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

				if (!asset.IsNullOrDestroyed())
				{
					assets.Add(asset);
				}
			}

			return assets.ToArray();
		}

		public static T[] FindAssetsOfType<T>(string filter)
			where T : Object
		{
			if (filter.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(filter));
				return Array.Empty<T>();
			}

			var assets = FindAssetsOfType(typeof(T), filter)
				.Cast<T>()
				.ToArray();

			return assets;
		}

		public static T[] FindAssetsOfType<T>()
			where T : Object
		{
			var filter = GetTypeFilter<T>();
			var assets = FindAssetsOfType<T>(filter);
			return assets;
		}

		public static GameObject[] FindPrefabsOfType(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return Array.Empty<GameObject>();
			}

			var prefabs = new List<GameObject>();
			var gameObjects = FindAssetsOfType<GameObject>(PrefabFilter);

			foreach (var gameObject in gameObjects)
			{
				if (gameObject.TryGetComponent(type, out _))
				{
					prefabs.Add(gameObject);
				}
			}

			return prefabs.ToArray();
		}

		public static T[] FindPrefabsOfType<T>()
		{
			var prefabs = FindPrefabsOfType(typeof(T))
				.Select(g => g.GetComponent<T>())
				.ToArray();

			return prefabs;
		}

		public static bool EnsureFolder(string relativePath)
		{
			if (relativePath.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(relativePath));
				return false;
			}

			relativePath = PathUtility.EnsureRoot(relativePath, AssetsFolder)
				.Substring(AssetsFolder.Length)
				.TrimStart(PathUtility.PathSeparators);

			var absolutePath = Path.Combine(Application.dataPath, relativePath);
			absolutePath = PathUtility.GetFileDirectory(absolutePath);

			Directory.CreateDirectory(absolutePath);
			AssetDatabase.Refresh(ImportAssetOptions.Default);
			return true;
		}

		public static string GetAssetPath(string relativePath, string assetName)
		{
			if (relativePath.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(relativePath));
			}

			if (assetName.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(assetName));
			}

			relativePath = PathUtility.EnsureRoot(relativePath, AssetsFolder);

			if (!relativePath.EndsWith(AssetExtension))
			{
				var assetPath = $"{assetName}{AssetExtension}";
				relativePath = Path.Combine(relativePath, assetPath);
			}

			return relativePath;
		}

		public static string GetTypeFilter(Type type)
		{
			if (type == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(type));
				return string.Empty;
			}

			var filter = string.Concat(TypeFilterPrefix, type.Name);
			return filter;
		}

		public static string GetTypeFilter<T>()
		{
			var filter = GetTypeFilter(typeof(T));
			return filter;
		}

		public static string GetLabelFilter(string label)
		{
			if (label.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(label));
				return string.Empty;
			}

			var filter = string.Concat(LabelFilterPrefix, label);
			return filter;
		}
	}
}