using Better.Commons.Runtime.Extensions;
using UnityEngine.UIElements;

namespace Better.Commons.Runtime.Utilities
{
	public static class VisualElementUtility
	{
		public static VisualElement CreateHorizontalElement()
		{
			var element = new VisualElement();
			element.style.FlexDirection(FlexDirection.Row);
			return element;
		}

		public static VisualElement CreateVerticalElement()
		{
			var element = new VisualElement();
			element.style.FlexDirection(FlexDirection.Column);
			return element;
		}
	}
}