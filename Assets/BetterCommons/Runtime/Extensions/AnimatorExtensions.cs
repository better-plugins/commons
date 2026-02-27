using System;
using System.Linq;
using Better.Commons.Runtime.Utilities;
using UnityEngine;
using Parameter = UnityEngine.AnimatorControllerParameter;
using ParameterType = UnityEngine.AnimatorControllerParameterType;

namespace Better.Commons.Runtime.Extensions
{
	public static class AnimatorExtensions
	{
		private const int UndefinedLayer = -1;

		#region Play

		public static void Play(this Animator self, int stateHash, float normalizedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.Play(stateHash, UndefinedLayer, normalizedTime);
		}

		public static void Play(this Animator self, int stateHash)
		{
			self.Play(stateHash, 0f);
		}

		public static void Play(this Animator self, string stateName)
		{
			self.Play(stateName, 0f);
		}

		public static void Play(this Animator self, string stateName, float normalizedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.Play(stateName, UndefinedLayer, normalizedTime);
		}

		#endregion

		#region PlayInFixedTime

		public static void PlayInFixedTime(this Animator self, int stateHash, float fixedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.PlayInFixedTime(stateHash, UndefinedLayer, fixedTime);
		}

		public static void PlayInFixedTime(this Animator self, int stateHash)
		{
			self.PlayInFixedTime(stateHash, 0f);
		}

		public static void PlayInFixedTime(this Animator self, string stateName)
		{
			self.PlayInFixedTime(stateName, 0f);
		}

		public static void PlayInFixedTime(this Animator self, string stateName, float fixedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.PlayInFixedTime(stateName, UndefinedLayer, fixedTime);
		}

		#endregion

		#region CrossFade

		public static void CrossFade(this Animator self, int stateHash, float normalizedTransitionDuration, float normalizedTimeOffset)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFade(stateHash, normalizedTransitionDuration, UndefinedLayer, normalizedTimeOffset);
		}

		public static void CrossFade(
			this Animator self,
			int stateHash,
			float normalizedTransitionDuration,
			float normalizedTimeOffset,
			float normalizedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFade(
				stateHash,
				normalizedTransitionDuration,
				UndefinedLayer,
				normalizedTimeOffset,
				normalizedTime);
		}

		public static void CrossFade(this Animator self, int stateHash, float normalizedTransitionDuration)
		{
			self.CrossFade(stateHash, normalizedTransitionDuration, 0f);
		}

		public static void CrossFade(this Animator self, string stateName, float normalizedTransitionDuration)
		{
			self.CrossFade(stateName, normalizedTransitionDuration, 0f);
		}

		public static void CrossFade(this Animator self, string stateName, float normalizedTransitionDuration, float normalizedTimeOffset)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFade(stateName, normalizedTransitionDuration, UndefinedLayer, normalizedTimeOffset);
		}

		public static void CrossFade(
			this Animator self,
			string stateName,
			float normalizedTransitionDuration,
			float normalizedTimeOffset,
			float normalizedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFade(
				stateName,
				normalizedTransitionDuration,
				UndefinedLayer,
				normalizedTimeOffset,
				normalizedTime);
		}

		#endregion

		#region CrossFadeInFixedTime

		public static void CrossFadeInFixedTime(
			this Animator self,
			int stateHash,
			float fixedTransitionDuration,
			float fixedTimeOffset,
			float normalizedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFadeInFixedTime(
				stateHash,
				fixedTransitionDuration,
				UndefinedLayer,
				fixedTimeOffset,
				normalizedTime);
		}

		public static void CrossFadeInFixedTime(this Animator self, int stateHash, float fixedTransitionDuration, float fixedTimeOffset)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFadeInFixedTime(stateHash, fixedTransitionDuration, UndefinedLayer, fixedTimeOffset);
		}

		public static void CrossFadeInFixedTime(this Animator self, int stateHash, float fixedTransitionDuration)
		{
			self.CrossFadeInFixedTime(stateHash, fixedTransitionDuration, 0f);
		}

		public static void CrossFadeInFixedTime(this Animator self, string stateName, float fixedTransitionDuration)
		{
			self.CrossFadeInFixedTime(stateName, fixedTransitionDuration, 0f);
		}

		public static void CrossFadeInFixedTime(this Animator self, string stateName, float fixedTransitionDuration, float fixedTimeOffset)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFadeInFixedTime(stateName, fixedTransitionDuration, UndefinedLayer, fixedTimeOffset);
		}

		public static void CrossFadeInFixedTime(
			this Animator self,
			string stateName,
			float fixedTransitionDuration,
			float fixedTimeOffset,
			float normalizedTime)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			self.CrossFadeInFixedTime(
				stateName,
				fixedTransitionDuration,
				UndefinedLayer,
				fixedTimeOffset,
				normalizedTime);
		}

		#endregion

		private static Parameter[] GetParametersOfType(this Animator self, ParameterType parameterType)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Array.Empty<Parameter>();
			}

			var result = self.parameters.Where(p => p.type == parameterType)
				.ToArray();

			return result;
		}

		public static string[] GetAllParameterNames(this Animator self)
		{
			if (self.IsNullOrDestroyed())
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Array.Empty<string>();
			}

			var names = self.parameters.Select(p => p.name)
				.ToArray();

			return names;
		}

		public static string[] GetAllIntegerNames(this Animator self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Int);
			return names;
		}

		public static string[] GetAllFloatNames(this Animator self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Float);
			return names;
		}

		public static string[] GetAllBoolNames(this Animator self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Bool);
			return names;
		}

		public static string[] GetAllTriggerNames(this Animator self)
		{
			var names = self.GetParameterNamesOfType(AnimatorControllerParameterType.Trigger);
			return names;
		}

		public static string[] GetParameterNamesOfType(this Animator self, ParameterType parameterType)
		{
			var names = self.GetParametersOfType(parameterType)
				.Select(p => p.name)
				.ToArray();

			return names;
		}

		public static bool HasParameter(this Animator self, string name)
		{
			var names = self.GetAllParameterNames();
			var contains = names.Contains(name);

			return contains;
		}

		public static bool HasInteger(this Animator self, string name)
		{
			var names = self.GetAllIntegerNames();
			var contains = names.Contains(name);

			return contains;
		}

		public static bool HasFloat(this Animator self, string name)
		{
			var names = self.GetAllFloatNames();
			var contains = names.Contains(name);

			return contains;
		}

		public static bool HasBool(this Animator self, string name)
		{
			var names = self.GetAllBoolNames();
			var contains = names.Contains(name);

			return contains;
		}

		public static bool HasTrigger(this Animator self, string name)
		{
			var names = self.GetAllTriggerNames();
			var contains = names.Contains(name);

			return contains;
		}

		public static int ResetAllTriggers(this Animator self)
		{
			var names = self.GetAllTriggerNames();

			for (var i = 0; i < names.Length; i++)
			{
				self.ResetTrigger(names[i]);
			}

			return names.Length;
		}

		public static int SetAllBools(this Animator self, bool value)
		{
			var names = self.GetAllBoolNames();

			for (var i = 0; i < names.Length; i++)
			{
				self.SetBool(names[i], value);
			}

			return names.Length;
		}

		public static int SetAllIntegers(this Animator self, int value)
		{
			var names = self.GetAllIntegerNames();

			for (var i = 0; i < names.Length; i++)
			{
				self.SetInteger(names[i], value);
			}

			return names.Length;
		}

		public static int SetAllFloats(this Animator self, float value)
		{
			var names = self.GetAllFloatNames();

			for (var i = 0; i < names.Length; i++)
			{
				self.SetFloat(names[i], value);
			}

			return names.Length;
		}
	}
}