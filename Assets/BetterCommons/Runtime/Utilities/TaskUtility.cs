using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public static class TaskUtility
	{
		public static async Task WaitForUnscaledSeconds(float seconds, CancellationToken cancellationToken = default)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}

			if (seconds <= 0f)
			{
				return;
			}

			while (seconds > 0f
					&& !cancellationToken.IsCancellationRequested)
			{
				await Task.Yield();
				seconds -= Time.unscaledDeltaTime;
			}
		}

		public static async Task WaitForScaledSeconds(float seconds, CancellationToken cancellationToken = default)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}

			if (seconds <= 0f)
			{
				return;
			}

			while (seconds > 0f
					&& !cancellationToken.IsCancellationRequested)
			{
				await Task.Yield();
				seconds -= Time.deltaTime;
			}
		}

		public static async Task WaitWhile(Func<bool> condition, CancellationToken cancellationToken = default)
		{
			if (condition == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(condition));
				return;
			}

			while (!cancellationToken.IsCancellationRequested
					&& condition.Invoke())
			{
				await Task.Yield();
			}
		}

		public static async Task WaitUntil(Func<bool> condition, CancellationToken cancellationToken = default)
		{
			if (condition == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(condition));
				return;
			}

			while (!cancellationToken.IsCancellationRequested
					&& !condition.Invoke())
			{
				await Task.Yield();
			}
		}

		public static async Task WaitFrame(int count, CancellationToken cancellationToken = default)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}

			if (count <= 0)
			{
				return;
			}

			for (var i = 0; i < count; i++)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return;
				}

				await Task.Yield();
			}
		}
	}
}