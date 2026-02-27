using System;
using UnityEngine.UIElements;

namespace Better.Commons.Runtime.Utilities
{
	public static class HelpBoxUtility
	{
		public static HelpBox HelpBox(string message, HelpBoxMessageType type)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			return new HelpBox(message, type);
		}

		public static HelpBox HelpBox(string message)
		{
			var helpBox = HelpBox(message, HelpBoxMessageType.None);
			return helpBox;
		}
	}
}