using UnityEditor;
using UnityEngine;

namespace Better.Commons.EditorAddons.Utilities
{
	public static class ScriptableObjectUtility
	{
		public static T CreateAsset<T>(string relativePath, string name)
			where T : ScriptableObject
		{
			var asset = ScriptableObject.CreateInstance<T>();
			asset.name = name;

			var assetPath = AssetDatabaseUtility.GetAssetPath(relativePath, name);
			AssetDatabase.CreateAsset(asset, assetPath);

			return asset;
		}

		public static T CreateAsset<T>(string relativePath)
			where T : ScriptableObject
		{
			var name = typeof(T).Name;
			var asset = CreateAsset<T>(relativePath, name);
			return asset;
		}
	}
}