using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public static class CursorUtility
	{
		public static void SetCursorActive(bool state)
		{
			var lockState = state ? CursorLockMode.Confined : CursorLockMode.Locked;
			Cursor.lockState = lockState;
			Cursor.visible = state;
		}
	}
}