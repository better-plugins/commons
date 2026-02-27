using System;
using Better.Commons.EditorAddons.Utilities;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEditor;
using UnityEngine;

namespace Better.Commons.EditorAddons.MenuItems
{
	public static class RigidbodyMenuItems
	{
		private const string ItemNamePrefix = MenuItemUtility.ContextMenuPath + nameof(Rigidbody) + "/";
		private const string ContactsProvidingItemPrefix = ItemNamePrefix + "Contacts Providing/";
		private const string EnableContactsItemName = ContactsProvidingItemPrefix + "Enable All";
		private const string DisableContactsItemName = ContactsProvidingItemPrefix + "Disable All";

		[MenuItem(DisableContactsItemName, false)]
		private static void DisableContactsByPolicy(MenuCommand command)
		{
			ChangeContactsPolicy(command, false);
		}
		
		[MenuItem(EnableContactsItemName, false)]
		private static void EnableContactsByPolicy(MenuCommand command)
		{
			ChangeContactsPolicy(command, true);
		}

		private static void ChangeContactsPolicy(MenuCommand command, bool active)
		{
			if (command.context is Rigidbody rigidbodyContext)
			{
				ChangeContactsPolicy(rigidbodyContext, active);
				return;
			}

			DebugUtility.LogException<InvalidOperationException>(command.context);
		}

		private static void ChangeContactsPolicy(Rigidbody rigidbody, bool active)
		{
			var colliders = rigidbody.GetAttachedColliders();

			foreach (var collider in colliders)
			{
				collider.providesContacts = active;
			}
		}
	}
}