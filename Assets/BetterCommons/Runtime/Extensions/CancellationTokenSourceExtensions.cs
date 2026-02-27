using System.Threading;

namespace Better.Commons.Runtime.Extensions
{
	public static class CancellationTokenSourceExtensions
	{
		public static bool TryCancel(this CancellationTokenSource self)
		{
			if (self == null)
			{
				return false;
			}

			if (self.IsCancellationRequested)
			{
				return false;
			}

			self.Cancel();
			return true;
		}

		public static bool TryCancel(this CancellationTokenSource self, bool throwOnFirstException)
		{
			if (self == null)
			{
				return false;
			}

			if (self.IsCancellationRequested)
			{
				return false;
			}

			self.Cancel(throwOnFirstException);
			return true;
		}
	}
}