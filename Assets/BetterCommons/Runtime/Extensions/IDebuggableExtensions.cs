using System;
using System.Text;
using Better.Commons.Runtime.Interfaces;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class IDebuggableExtensions
	{
		public static void CollectBriefDebugInfo(this IDebuggable self, ref StringBuilder builder)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CollectDebugInfo(DebugUtility.BriefLogDepth, ref builder);
		}

		public static void ToDetailedDebugInfo(this IDebuggable self, ref StringBuilder builder)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CollectDebugInfo(DebugUtility.DetailedLogDepth, ref builder);
		}

		public static string ToDebugInfo(this IDebuggable self, int depth)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return string.Empty;
			}

			var stringBuilder = new StringBuilder();
			self.CollectDebugInfo(depth, ref stringBuilder);
			var debugInfo = stringBuilder.ToString();

			return debugInfo;
		}

		public static string ToBriefDebugInfo(this IDebuggable self)
		{
			var debugInfo = self.ToDebugInfo(DebugUtility.BriefLogDepth);
			return debugInfo;
		}

		public static string ToDetailedDebugInfo(this IDebuggable self)
		{
			var debugInfo = self.ToDebugInfo(DebugUtility.DetailedLogDepth);
			return debugInfo;
		}

		public static void LogDebugInfo(this IDebuggable self, int depth)
		{
			var debugInfo = self.ToDebugInfo(depth);
			Debug.Log(debugInfo);
		}

		public static void LogBriefDebugInfo(this IDebuggable self)
		{
			var debugInfo = self.ToBriefDebugInfo();
			Debug.Log(debugInfo);
		}

		public static void LogDetailedDebugInfo(this IDebuggable self)
		{
			var debugInfo = self.ToDetailedDebugInfo();
			Debug.Log(debugInfo);
		}
	}
}