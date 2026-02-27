using System;
using Better.Commons.EditorAddons.Utilities;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Interfaces;
using Better.Commons.Runtime.Utilities;
using UnityEditor;

namespace Better.Commons.EditorAddons.MenuItems
{
	public static class IDebugableMenuItems
	{
		private const string DebugInfoItemPrefix = MenuItemUtility.ObjectContextMenuPath + "DebugInfo/";
		private const string DetailedDebugInfoItemName = DebugInfoItemPrefix + "Log Detailed";
		private const string BriefDebugInfoItemName = DebugInfoItemPrefix + "Log Brief";

		[MenuItem(DetailedDebugInfoItemName, false)]
		private static void LogDetailedDebugInfo(MenuCommand command)
		{
			LogDebugInfo(command, DebugUtility.DetailedLogDepth);
		}

		[MenuItem(BriefDebugInfoItemName, false)]
		private static void LogBriefDebugInfo(MenuCommand command)
		{
			LogDebugInfo(command, DebugUtility.BriefLogDepth);
		}

		private static void LogDebugInfo(MenuCommand command, int depth)
		{
			if (command.context is IDebuggable debuggableContext)
			{
				debuggableContext.LogDebugInfo(depth);
				return;
			}

			DebugUtility.LogException<InvalidOperationException>(command.context);
		}

		[MenuItem(DetailedDebugInfoItemName, true)]
		[MenuItem(BriefDebugInfoItemName, true)]
		private static bool DebugActionValidate(MenuCommand command)
		{
			var debugable = command.context is IDebuggable;
			return debugable;
		}
	}
}