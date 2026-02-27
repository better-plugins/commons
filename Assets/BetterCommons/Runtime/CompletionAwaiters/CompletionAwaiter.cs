using System.Threading;
using System.Threading.Tasks;

namespace Better.Commons.Runtime.CompletionAwaiters
{
	public abstract class CompletionAwaiter<TResult>
	{
		private readonly TaskCompletionSource<TResult> _completionSource;
		private CancellationTokenRegistration _cancellationTokenRegistration;
		
		public Task<TResult> Task => _completionSource.Task;

		protected CompletionAwaiter(CancellationToken cancellationToken)
		{
			_completionSource = new TaskCompletionSource<TResult>();
			cancellationToken.Register(OnTokenCancelled);
		}

		protected void SetResult(TResult result)
		{
			if (!_completionSource.TrySetResult(result))
			{
				return;
			}

			OnResulted(result);
		}

		protected virtual void OnResulted(TResult result)
		{
			_cancellationTokenRegistration.Dispose();
		}

		private void OnTokenCancelled()
		{
			Cancel();
		}

		protected void Cancel()
		{
			var cancellationResult = GetCancellationResult();
			SetResult(cancellationResult);
		}

		protected virtual TResult GetCancellationResult()
		{
			var defaultResult = default(TResult);
			return defaultResult;
		}
	}

	public abstract class CompletionAwaiter : CompletionAwaiter<bool>
	{
		protected CompletionAwaiter(CancellationToken cancellationToken) : base(cancellationToken)
		{
		}
	}
}