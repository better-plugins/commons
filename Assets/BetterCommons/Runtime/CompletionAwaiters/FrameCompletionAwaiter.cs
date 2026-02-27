using System.Threading;
using Better.Commons.Runtime.Extensions;
using ThreadingTask = System.Threading.Tasks.Task;

namespace Better.Commons.Runtime.CompletionAwaiters
{
	public abstract class FrameCompletionAwaiter<TResult> : CompletionAwaiter<TResult>
	{
		protected FrameCompletionAwaiter(CancellationToken cancellationToken) : base(cancellationToken)
		{
			ProcessKeepingAsync(cancellationToken)
				.Forget();
		}

		private async ThreadingTask ProcessKeepingAsync(CancellationToken cancellationToken)
		{
			do
			{
				await ThreadingTask.Yield();

				if (cancellationToken.IsCancellationRequested)
				{
					break;
				}
			} while (KeepAwaiting());

			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}

			var result = GetResult();
			SetResult(result);
		}

		protected abstract bool KeepAwaiting();

		protected abstract TResult GetResult();

		protected override TResult GetCancellationResult()
		{
			var result = GetResult();
			return result;
		}
	}

	public abstract class FrameCompletionAwaiter : FrameCompletionAwaiter<bool>
	{
		protected FrameCompletionAwaiter(CancellationToken cancellationToken) : base(cancellationToken)
		{
		}

		protected override bool GetResult()
		{
			var resultByAwaiting = !KeepAwaiting();
			return resultByAwaiting;
		}
	}
}