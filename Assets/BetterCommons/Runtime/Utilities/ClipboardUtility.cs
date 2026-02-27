using System;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public static class ClipboardUtility
	{
		public static void CopyToClipboard(string value)
		{
			GUIUtility.systemCopyBuffer = value;
		}

		public static void CopyToClipboard(object value)
		{
			var convertedValue = Convert.ToString(value);
			CopyToClipboard(convertedValue);
		}

		public static string GetFromClipboard()
		{
			return GUIUtility.systemCopyBuffer;
		}

		public static bool IsEmptyClipboard()
		{
			var value = GetFromClipboard();
			var isEmpty = value.IsNullOrEmpty();

			return isEmpty;
		}

		public static void ClearClipboard()
		{
			var value = string.Empty;
			CopyToClipboard(value);
		}
	}
}