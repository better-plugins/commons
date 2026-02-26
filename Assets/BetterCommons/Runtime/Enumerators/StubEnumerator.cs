using System.Collections;
using System.Collections.Generic;

namespace Better.Commons.Runtime.Enumerators
{
	public struct StubEnumerator<T> : IEnumerator<T>
	{
		public static readonly StubEnumerator<T> Default = new();

		public T Current { get; }
		object IEnumerator.Current => Current;

		public bool MoveNext()
		{
			return false;
		}

		public void Reset()
		{
		}

		public void Dispose()
		{
		}
	}
}