using System;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEditor.Animations;
using UnityEngine;

namespace Better.Commons.EditorAddons.Extensions
{
	public static class AnimatorControllerExtensions
	{
		private static AnimatorControllerParameter[] GetParametersOfType(this AnimatorController self, AnimatorControllerParameterType parameterType)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Array.Empty<AnimatorControllerParameter>();
			}

			var parameters = self.parameters.Where(p => p.type == parameterType)
				.ToArray();

			return parameters;
		}

		public static string[] GetParameterNamesOfType(this AnimatorController self, AnimatorControllerParameterType parameterType)
		{
			var names = self.GetParametersOfType(parameterType)
				.Select(p => p.name)
				.ToArray();

			return names;
		}

		public static string[] GetAllIntegerNames(this AnimatorController self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Int);
			return names;
		}

		public static string[] GetAllFloatNames(this AnimatorController self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Float);
			return names;
		}

		public static string[] GetAllBoolNames(this AnimatorController self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Bool);
			return names;
		}

		public static string[] GetAllTriggerNames(this AnimatorController self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Trigger);
			return names;
		}

		public static bool HasParameter(this AnimatorController self, string name)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var names = self.parameters.Select(p => p.name);
			var containsName = names.Contains(name);
			return containsName;
		}

		public static bool HasInteger(this AnimatorController self, string name)
		{
			var names = self.GetAllIntegerNames();
			var containsName = names.Contains(name);
			return containsName;
		}

		public static bool HasFloat(this AnimatorController self, string name)
		{
			var names = self.GetAllFloatNames();
			var containsName = names.Contains(name);
			return containsName;
		}

		public static bool HasBool(this AnimatorController self, string name)
		{
			var names = self.GetAllBoolNames();
			var containsName = names.Contains(name);
			return containsName;
		}

		public static bool HasTrigger(this AnimatorController self, string name)
		{
			var names = self.GetAllTriggerNames();
			var containsName = names.Contains(name);
			return containsName;
		}
	}
}