using System;
using System.ComponentModel;

namespace Better.Commons.EditorAddons.Utilities
{
	public static class MenuItemUtility
	{
		public const string ToolsMenuPath = "Tools/";
		public const string WindowMenuPath = "Window/";
		public const string EditMenuPath = "Edit/";
		public const string AssetsMenuPath = "Assets/";
		public const string ContextMenuPath = "CONTEXT/";
		public const string ComponentContextMenuPath = ContextMenuPath + nameof(Component) + "/";
		public const string ObjectContextMenuPath = ContextMenuPath + nameof(Object) + "/";
		public const string GameObjectMenuPath = "GameObject/";
		public const string SceneViewMenuPath = "SceneView/";
	}
}