using System;
using Better.Commons.Runtime.Utilities;
using UnityEngine.UIElements;

namespace Better.Commons.Runtime.Extensions
{
	public static class VisualElementExtensions
	{
		public static bool TryFindChildRecursive<TElement>(this VisualElement self, Func<TElement, bool> selector, int maxDepth, out TElement child)
			where TElement : VisualElement
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				child = null;
				return false;
			}

			if (selector == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(selector));
				child = null;
				return false;
			}

			if (maxDepth <= 0)
			{
				child = null;
				return false;
			}

			if (self is TElement specificSelf
				&& selector.Invoke(specificSelf))
			{
				child = specificSelf;
				return true;
			}

			maxDepth--;
			var hierarchyChildren = self.hierarchy.Children();

			foreach (var hierarchyChild in hierarchyChildren)
			{
				if (TryFindAncestorRecursive(hierarchyChild, selector, maxDepth, out child))
				{
					return true;
				}
			}

			child = null;
			return false;
		}

		public static bool TryFindAncestorRecursive<TElement>(this VisualElement self, Func<TElement, bool> selector, int maxDepth, out TElement ancestor)
			where TElement : VisualElement
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				ancestor = null;
				return false;
			}

			if (selector == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(selector));
				ancestor = null;
				return false;
			}

			if (maxDepth <= 0)
			{
				ancestor = null;
				return false;
			}

			var hierarchyAncestor = self.hierarchy.parent;

			if (hierarchyAncestor == null)
			{
				ancestor = null;
				return false;
			}

			if (hierarchyAncestor is TElement specificHierarchyAncestor
				&& selector.Invoke(specificHierarchyAncestor))
			{
				ancestor = specificHierarchyAncestor;
				return true;
			}

			maxDepth--;
			return TryFindAncestorRecursive(hierarchyAncestor, selector, maxDepth, out ancestor);
		}

		public static VisualElement AddManipulator<TManipulator>(this VisualElement self)
			where TManipulator : IManipulator, new()
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}

			var manipulator = new TManipulator();
			self.AddManipulator(manipulator);
			return self;
		}

		public static void SetEnabledAndFocusable(this VisualElement self, bool value)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.SetEnabled(value);
			self.focusable = value;
		}

		public static void ForceEnabled(this VisualElement self, bool value)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.style.SetVisible(value);
			self.SetEnabledAndFocusable(value);
		}
	}
}