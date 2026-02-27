using System.Threading;

namespace Better.Commons.Runtime.Utilities
{
	public static class CancellationTokenSourceUtility
	{
		public static CancellationTokenSource CancelledSource { get; }

		static CancellationTokenSourceUtility()
		{
			CancelledSource = new CancellationTokenSource();
			CancelledSource.Cancel();
		}
	}
}