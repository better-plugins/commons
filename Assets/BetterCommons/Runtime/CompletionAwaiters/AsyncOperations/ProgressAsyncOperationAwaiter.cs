using System;
using System.Threading;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utilities;
using UnityEngine;

namespace Better.Commons.Runtime.CompletionAwaiters
{
	public class ProgressAsyncOperationAwaiter : AsyncOperationAwaiter<AsyncOperation, bool>
	{
		protected float ProgressThreshold { get; }
		protected bool ProgressDone => Operation.progress >= ProgressThreshold;

		public ProgressAsyncOperationAwaiter(CancellationToken cancellationToken, AsyncOperation operation, float progressThreshold) : base(cancellationToken, operation)
		{
			if (!progressThreshold.IsNormalized())
			{
				var progressRangeMessage = $"{nameof(progressThreshold)}({progressThreshold}) not normalized, will be forcibly normalized";
				DebugUtility.LogException<ArgumentOutOfRangeException>(progressRangeMessage);

				progressThreshold = Mathf.Clamp01(progressThreshold);
			}

			ProgressThreshold = progressThreshold;
		}

		protected override bool KeepAwaiting()
		{
			var keepByDone = !ProgressDone;
			return keepByDone;
		}

		protected override bool GetResult()
		{
			return ProgressDone;
		}
	}
}