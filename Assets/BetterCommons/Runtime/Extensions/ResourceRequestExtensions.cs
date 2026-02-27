using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utilities;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Better.Commons.Runtime.Extensions
{
	public static class ResourceRequestExtensions
	{
		public static async Task<UnityObject> AwaitAssetLoading(this ResourceRequest self, CancellationToken cancellationToken)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}

			await self.AwaitCompletion(cancellationToken);
			return self.asset;
		}
	}
}