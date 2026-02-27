using System.Threading;
using UnityEngine;

namespace Better.Commons.Runtime.CompletionAwaiters
{
	public class ProgressAsyncOperationAwaiter : AsyncOperationAwaiter<AsyncOperation, bool>
	{
		protected float ProgressThreshold { get; }
		protected bool ProgressDone => Operation.progress >= ProgressThreshold;

		public ProgressAsyncOperationAwaiter(CancellationToken cancellationToken, AsyncOperation operation, float progressThreshold) : base(cancellationToken, operation)
		{
			// TODO: will be updated with #14
			/*
			if (!progressThreshold.IsNormalized())
			{
				var progressRangeMessage = $"{nameof(progressThreshold)}({progressThreshold}) not normalized, will be forcibly normalized";
				DebugUtility.LogException<ArgumentOutOfRangeException>(progressRangeMessage);

				progressThreshold = Mathf.Clamp01(progressThreshold);
			}
			*/

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