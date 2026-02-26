using System;
using System.Collections.Generic;
using System.Threading;
using Better.Commons.Runtime.Enumerators;

namespace Better.Commons.Runtime.Events
{
	public class FlatEvent<TCollection, TDelegate> : Event<TCollection, TDelegate>
		where TCollection : ICollection<TDelegate>, new()
		where TDelegate : Delegate
	{
		private CancellationTokenSource _enumeratorTokenSource;

		protected override IEnumerator<TDelegate> CreateEnumerator()
		{
			var source = base.CreateEnumerator();

			_enumeratorTokenSource?.Cancel();
			_enumeratorTokenSource = new CancellationTokenSource();

			var enumerator = new CancellableEnumerator<TDelegate>(source, _enumeratorTokenSource.Token);
			return enumerator;
		}
	}

	public class FlatEvent<TDelegate> : FlatEvent<HashSet<TDelegate>, TDelegate>
		where TDelegate : Delegate
	{
	}
}