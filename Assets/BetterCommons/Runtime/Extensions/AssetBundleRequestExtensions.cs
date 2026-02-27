using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utilities;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Better.Commons.Runtime.Extensions
{
	public static class AssetBundleRequestExtensions
	{
		public static async Task<UnityObject> AwaitAssetLoading(this AssetBundleRequest self, CancellationToken cancellationToken)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}

			await self.AwaitCompletion(cancellationToken);
			return self.asset;
		}

		public static async Task<UnityObject[]> AwaitAssetsLoading(this AssetBundleRequest self, CancellationToken cancellationToken)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return null;
			}

			await self.AwaitCompletion(cancellationToken);
			return self.allAssets;
		}
	}
}