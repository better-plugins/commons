using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Better.Commons.Runtime.Enumerators
{
	public struct CancellableEnumerator<T> : IEnumerator<T>
	{
		private readonly IEnumerator<T> _source;
		private readonly CancellationToken _cancellationToken;

		T IEnumerator<T>.Current => _source.Current;
		object IEnumerator.Current => _source.Current;

		public CancellableEnumerator(IEnumerator<T> source, CancellationToken cancellationToken)
		{
			_source = source;
			_cancellationToken = cancellationToken;
		}

		bool IEnumerator.MoveNext()
		{
			if (_cancellationToken.IsCancellationRequested)
			{
				return false;
			}

			var moveNext = _source.MoveNext();
			return moveNext;
		}

		void IEnumerator.Reset()
		{
			_source.Reset();
		}

		void IDisposable.Dispose()
		{
			_source.Dispose();
		}
	}
}