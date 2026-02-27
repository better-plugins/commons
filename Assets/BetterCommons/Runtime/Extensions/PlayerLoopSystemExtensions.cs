using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Better.Commons.Runtime.Utilities;
using UnityEngine.LowLevel;

namespace Better.Commons.Runtime.Extensions
{
	public static class PlayerLoopSystemExtensions
	{
		public static bool HasSubSystemOf(this ref PlayerLoopSystem self, Type subSystemType)
		{
			var index = self.FindSubSystemIndex(subSystemType);
			var hasSubSystem = index != -1;

			return hasSubSystem;
		}

		public static bool HasSubSystemOf<T>(this ref PlayerLoopSystem self)
		{
			var subSystemType = typeof(T);
			var hasSubSystem = self.HasSubSystemOf(subSystemType);

			return hasSubSystem;
		}

		public static int FindSubSystemIndex(this ref PlayerLoopSystem self, Type subSystemType)
		{
			var subSystems = self.subSystemList;

			if (subSystems == null)
			{
				return -1;
			}

			for (var i = 0; i < subSystems.Length; i++)
			{
				if (subSystems[i].type == subSystemType)
				{
					return i;
				}
			}

			return -1;
		}

		public static int FindSubSystemIndex<T>(this ref PlayerLoopSystem self)
		{
			var subSystemType = typeof(T);
			var subSystemIndex = self.FindSubSystemIndex(subSystemType);

			return subSystemIndex;
		}

		public static ref PlayerLoopSystem GetSubSystem(this ref PlayerLoopSystem self, Type subSystemType)
		{
			var index = self.FindSubSystemIndex(subSystemType);

			if (index == -1)
			{
				return ref self;
			}

			ref var subSystem = ref self.subSystemList[index];
			return ref subSystem;
		}

		public static ref PlayerLoopSystem GetSubSystem<TSubSystem>(this ref PlayerLoopSystem self)
		{
			var subSystemType = typeof(TSubSystem);
			ref var subSystem = ref self.GetSubSystem(subSystemType);

			return ref subSystem;
		}

		public static ref PlayerLoopSystem GetSubSystemRecursive(this ref PlayerLoopSystem self, Type subSystemType)
		{
			var subSystems = self.subSystemList;

			if (subSystems == null)
			{
				return ref self;
			}

			ref var subSystem = ref self.GetSubSystem(subSystemType);

			if (subSystem.type == subSystemType)
			{
				return ref subSystem;
			}

			for (var i = 0; i < subSystems.Length; i++)
			{
				ref var buffer = ref GetSubSystemRecursive(ref subSystems[i], subSystemType);

				if (buffer.type == subSystemType)
				{
					return ref buffer;
				}
			}

			return ref self;
		}

		public static ref PlayerLoopSystem GetSubSystemRecursive<TSubSystem>(this ref PlayerLoopSystem self)
		{
			var subSystemType = typeof(TSubSystem);
			ref var subSystem = ref self.GetSubSystemRecursive(subSystemType);

			return ref subSystem;
		}

		public static ref PlayerLoopSystem GetParentSystemRecursiveOf(this ref PlayerLoopSystem self, Type subSystemType)
		{
			if (self.type == subSystemType)
			{
				return ref self;
			}

			var subSystems = self.subSystemList;

			if (subSystems == null)
			{
				return ref self;
			}

			if (self.HasSubSystemOf(subSystemType))
			{
				return ref self;
			}

			for (var i = 0; i < subSystems.Length; i++)
			{
				ref var buffer = ref subSystems[i];
				buffer = ref buffer.GetParentSystemRecursiveOf(subSystemType);

				if (buffer.HasSubSystemOf(subSystemType))
				{
					return ref buffer;
				}
			}

			return ref self;
		}

		public static ref PlayerLoopSystem GetParentSystemRecursiveOf<TSubSystem>(this ref PlayerLoopSystem self)
		{
			var subSystemType = typeof(TSubSystem);
			ref var subSystem = ref self.GetParentSystemRecursiveOf(subSystemType);

			return ref subSystem;
		}

		public static bool RemoveSubSystem(this ref PlayerLoopSystem self, Type subSystemType)
		{
			if (self.subSystemList == null)
			{
				return false;
			}

			var anyRemoved = false;
			var subSystems = self.subSystemList.ToList();

			for (var i = subSystems.Count - 1; i >= 0; i--)
			{
				if (subSystems[i].type == subSystemType)
				{
					subSystems.RemoveAt(i);
					anyRemoved = true;
				}
			}

			if (anyRemoved)
			{
				self.subSystemList = subSystems.ToArray();
			}

			return anyRemoved;
		}

		public static bool RemoveSubSystem<TSubSystem>(this ref PlayerLoopSystem self)
		{
			var subSystemType = typeof(TSubSystem);
			var removed = self.RemoveSubSystem(subSystemType);

			return removed;
		}

		public static bool RemoveSubSystemRecursive(this ref PlayerLoopSystem self, Type subSystemType)
		{
			var removeAny = self.RemoveSubSystem(subSystemType);
			var subSystems = self.subSystemList;

			if (subSystems != null)
			{
				for (var i = 0; i < subSystems.Length; i++)
				{
					if (subSystems[i]
						.RemoveSubSystemRecursive(subSystemType))
					{
						removeAny = true;
					}
				}
			}

			return removeAny;
		}

		public static bool RemoveSubSystemRecursive<TSubSystem>(this ref PlayerLoopSystem self)
		{
			var subSystemType = typeof(TSubSystem);
			var removed = self.RemoveSubSystemRecursive(subSystemType);

			return removed;
		}

		public static bool UnsubscribeRecursive(this ref PlayerLoopSystem self, PlayerLoopSystem.UpdateFunction updateFunction)
		{
			if (updateFunction == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(updateFunction));
				return false;
			}

			var result = false;
#if UNITY_EDITOR || DEBUG
			if (self.updateDelegate != null)
			{
				var invocationList = self.updateDelegate.GetInvocationList();

				if (invocationList.Contains(updateFunction))
				{
					self.updateDelegate -= updateFunction;
					result = true;
				}
			}
#else
            self.updateDelegate -= updateFunction;
            result = true;
#endif

			var subSystems = self.subSystemList;

			if (subSystems != null)
			{
				for (var i = 0; i < subSystems.Length; i++)
				{
					if (subSystems[i]
						.UnsubscribeRecursive(updateFunction))
					{
						result = true;
					}
				}
			}

			return result;
		}

		public static bool UnsubscribeRecursive(this ref PlayerLoopSystem self, Type loopType, PlayerLoopSystem.UpdateFunction updateFunction)
		{
			var result = false;

			if (self.type == loopType)
			{
#if UNITY_EDITOR || DEBUG
				var invocationList = self.updateDelegate.GetInvocationList();

				if (invocationList.Contains(updateFunction))
				{
					self.updateDelegate -= updateFunction;
					result = true;
				}
#else
                self.updateDelegate -= updateFunction;
                result = true;
#endif
			}

			var subSystems = self.subSystemList;

			if (subSystems != null)
			{
				for (var i = 0; i < subSystems.Length; i++)
				{
					if (subSystems[i]
						.UnsubscribeRecursive(loopType, updateFunction))
					{
						result = true;
					}
				}
			}

			return result;
		}

		public static bool UnsubscribeRecursive<TLoop>(this ref PlayerLoopSystem self, PlayerLoopSystem.UpdateFunction updateFunction)
		{
			var loopType = typeof(TLoop);
			var unsubscribed = self.UnsubscribeRecursive(loopType, updateFunction);

			return unsubscribed;
		}

		public static Type[] GetTypes(this ref PlayerLoopSystem self)
		{
			var loopTypes = new List<Type>();
			self.CollectTypesRecursive(ref loopTypes);

			return loopTypes.ToArray();
		}

		private static void CollectTypesRecursive(this ref PlayerLoopSystem self, ref List<Type> loopTypes)
		{
			loopTypes.Add(self.type);

			var subSystems = self.subSystemList;

			if (subSystems == null)
			{
				return;
			}

			for (var i = 0; i < subSystems.Length; i++)
			{
				subSystems[i]
					.CollectTypesRecursive(ref loopTypes);
			}
		}

		public static void CollectDebugInfo(this PlayerLoopSystem self, int sourceDepth, ref StringBuilder builder)
		{
			var depth = sourceDepth - 1;
			builder.AppendLine(self.type.Name);

			var types = self.GetTypes();
			builder.AppendFieldLine(nameof(types), types.Length);

			if (depth < 2)
			{
				return;
			}

			for (var i = 0; i < types.Length; i++)
			{
				var subName = Convert.ToString(i);
				var loopType = types[i];

				if (loopType == null)
				{
					builder.AppendFieldLine(subName, null);
					continue;
				}

				builder.AppendFieldLine(subName, loopType.Name);
			}
		}
	}
}