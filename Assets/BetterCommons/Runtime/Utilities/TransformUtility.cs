using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Better.Commons.Runtime.Extensions;
using UnityEngine.SceneManagement;

namespace Better.Commons.Runtime.Utilities
{
	public static class TransformUtility
	{
		public static Transform FindOrCreate(Transform root, string path)
		{
			if (root.IsNullOrDestroyed())
			{
				throw new ArgumentNullException(nameof(root));
			}

			if (path.IsNullOrWhiteSpace())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(path));
				return root;
			}

			var pathParts = path.Split(Path.PathSeparator);
			var current = root;

			foreach (var name in pathParts)
			{
				if (!current.TryFind(name, out current))
				{
					current = current.CreateChild(name);
				}
			}

			return current;
		}

		public static Transform FindOrCreateInScene(Scene scene, string path)
		{
			if (!scene.IsValid())
			{
				var sceneMessage = $"{nameof(scene)}({scene.name}) must be a valid";
				throw new InvalidOperationException(sceneMessage);
			}

			var parts = path.Split(Path.PathSeparator);
			var rootName = parts.First();
			path = path.Substring(rootName.Length);

			if (!TryFindRootInScene(scene, rootName, out var root))
			{
				root = CreateRootInScene(scene, rootName);
			}

			var found = FindOrCreate(root, path);
			return found;
		}

		public static Transform FindOrCreateInActiveScene(string path)
		{
			var activeScene = SceneManager.GetActiveScene();
			var found = FindOrCreateInScene(activeScene, path);
			return found;
		}

		public static bool TryFindRootInScene(Scene scene, string name, out Transform root)
		{
			if (!scene.IsValid())
			{
				var sceneMessage = $"{nameof(scene)}({scene.name}) must be a valid";
				DebugUtility.LogException<InvalidOperationException>(sceneMessage);

				root = null;
				return false;
			}

			var rootObjects = scene.GetRootGameObjects();

			foreach (var rootObject in rootObjects)
			{
				if (rootObject.name.Equals(name))
				{
					root = rootObject.transform;
					return true;
				}
			}

			root = null;
			return false;
		}

		public static bool TryFindRootInActiveScene(string name, out Transform root)
		{
			var activeScene = SceneManager.GetActiveScene();
			var found = TryFindRootInScene(activeScene, name, out root);

			return found;
		}

		public static Transform CreateRootInScene(Scene scene, string name)
		{
			if (!scene.IsValid())
			{
				var sceneMessage = $"{nameof(scene)}({scene.name}) must be a valid";
				throw new InvalidOperationException(sceneMessage);
			}

			var gameObject = new GameObject(name);
			SceneManager.MoveGameObjectToScene(gameObject, scene);

			return gameObject.transform;
		}

		public static Transform CreateRootInActiveScene(string name)
		{
			var activeScene = SceneManager.GetActiveScene();
			var root = CreateRootInScene(activeScene, name);

			return root;
		}
	}
}