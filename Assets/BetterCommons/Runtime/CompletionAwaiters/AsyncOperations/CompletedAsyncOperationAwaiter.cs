using System.Threading;
using UnityEngine;

namespace Better.Commons.Runtime.CompletionAwaiters
{
	public abstract class CompletedAsyncOperationAwaiter<TOperation, TValue> : AsyncOperationAwaiter<TOperation, TValue>
		where TOperation : AsyncOperation
	{
		protected CompletedAsyncOperationAwaiter(CancellationToken cancellationToken, TOperation operation) : base(cancellationToken, operation)
		{
		}

		protected override bool KeepAwaiting()
		{
			var keepByDone = !Operation.isDone;
			return keepByDone;
		}
	}

	public abstract class CompletedAsyncOperationAwaiter<TValue> : CompletedAsyncOperationAwaiter<AsyncOperation, TValue>
	{
		protected CompletedAsyncOperationAwaiter(CancellationToken cancellationToken, AsyncOperation operation) : base(cancellationToken, operation)
		{
		}
	}

	public class CompletedAsyncOperationAwaiter : CompletedAsyncOperationAwaiter<bool>
	{
		public CompletedAsyncOperationAwaiter(CancellationToken cancellationToken, AsyncOperation operation) : base(cancellationToken, operation)
		{
		}

		public CompletedAsyncOperationAwaiter(AsyncOperation operation) : this(CancellationToken.None, operation)
		{
		}

		protected override bool GetResult()
		{
			return Operation.isDone;
		}
	}
}