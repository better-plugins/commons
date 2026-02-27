using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.CompletionAwaiters;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.Extensions
{
	public static class AsyncOperationExtensions
	{
		public static Task<bool> AwaitProgress(this AsyncOperation self, float progress, CancellationToken cancellationToken = default)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Task.FromResult(false);
			}

			var awaiter = new ProgressAsyncOperationAwaiter(cancellationToken, self, progress);
			return awaiter.Task;
		}

		public static Task<bool> AwaitActivationProgress(this AsyncOperation self, CancellationToken cancellationToken = default)
		{
			var task = self.AwaitProgress(AsyncOperationUtility.ActivationReadyProgress, cancellationToken);
			return task;
		}

		public static Task<bool> AwaitCompletion(this AsyncOperation self, CancellationToken cancellationToken = default)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return Task.FromResult(false);
			}

			var awaiter = new CompletedAsyncOperationAwaiter(cancellationToken, self);
			return awaiter.Task;
		}
	}
}