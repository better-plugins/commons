using UnityEngine;

namespace Better.Commons.Runtime.Components
{
	[RequireComponent(typeof(RectTransform))]
	public class UIMonoBehaviour : ExtendedMonoBehaviour
	{
		private RectTransform _rectTransform;

		public RectTransform RectTransform
		{
			get
			{
				// TODO: will be updated with #14
				// if (_rectTransform.IsNullOrDestroyed())
				// {
					_rectTransform = GetComponent<RectTransform>();
				// }

				return _rectTransform;
			}
		}
	}
}