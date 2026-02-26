using System;
using System.Collections;
using System.Collections.Generic;

namespace Better.Commons.Runtime.Events
{
	public abstract class Event<TCollection, TDelegate> : IEnumerable<TDelegate>
		where TCollection : ICollection<TDelegate>, new()
		where TDelegate : Delegate
	{
		protected TCollection Delegates { get; }

		protected Event()
		{
			Delegates = new TCollection();
		}

		public bool Subscribe(TDelegate value)
		{
			if (Delegates.Contains(value))
			{
				return false;
			}

			Delegates.Add(value);
			OnDelegateSubscribed(value);
			return true;
		}

		protected virtual void OnDelegateSubscribed(TDelegate value)
		{
		}

		public bool Unsubscribe(TDelegate value)
		{
			var unsubscribed = Delegates.Remove(value);

			if (unsubscribed)
			{
				OnDelegateUnsubscribed(value);
			}

			return unsubscribed;
		}

		protected virtual void OnDelegateUnsubscribed(TDelegate value)
		{
		}

		IEnumerator<TDelegate> IEnumerable<TDelegate>.GetEnumerator()
		{
			var enumerator = CreateEnumerator();
			return enumerator;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			var enumerator = CreateEnumerator();
			return enumerator;
		}

		protected virtual IEnumerator<TDelegate> CreateEnumerator()
		{
			var enumerator = Delegates.GetEnumerator();
			return enumerator;
		}

		public void Clear()
		{
			Delegates.Clear();
			OnCleared();
		}

		protected virtual void OnCleared()
		{
		}

		public override string ToString()
		{
			var value = $"{nameof(Delegates)}({Delegates.Count})";
			return value;
		}
	}

	public abstract class Event<TDelegate> : Event<HashSet<TDelegate>, TDelegate>
		where TDelegate : Delegate
	{
	}
}