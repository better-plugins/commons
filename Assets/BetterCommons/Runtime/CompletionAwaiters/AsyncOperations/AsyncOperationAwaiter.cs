using System;
using System.Threading;
using UnityEngine;

namespace Better.Commons.Runtime.CompletionAwaiters
{
	public abstract class AsyncOperationAwaiter<TOperation, TValue> : FrameCompletionAwaiter<TValue>
		where TOperation : AsyncOperation
	{
		protected TOperation Operation { get; }

		protected AsyncOperationAwaiter(CancellationToken cancellationToken, TOperation operation) : base(cancellationToken)
		{
			if (operation == null)
			{
				throw new ArgumentNullException(nameof(operation));
			}

			Operation = operation;
		}
	}

	public abstract class AsyncOperationAwaiter<TOperation> : AsyncOperationAwaiter<TOperation, bool>
		where TOperation : AsyncOperation
	{
		protected AsyncOperationAwaiter(CancellationToken cancellationToken, TOperation operation) : base(cancellationToken, operation)
		{
		}
	}
}