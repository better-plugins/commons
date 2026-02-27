using System;
using UnityEngine;

namespace Better.Commons.Runtime.Results
{
	[Serializable]
	public struct Result<TValue>
	{
		public static readonly Result<TValue> Unsuccessful = new();

		[SerializeField] private TValue _value;
		[SerializeField] private bool _succeed;

		public readonly TValue Value => _value;
		public readonly bool Succeed => _succeed;

		private Result(bool succeed, TValue value)
		{
			_succeed = succeed;
			_value = value;
		}

		public static Result<TValue> From(bool succeed, TValue value)
		{
			var result = new Result<TValue>(succeed, value);
			return result;
		}

		public static Result<TValue> FromSucceed(TValue value)
		{
			var result = From(true, value);
			return result;
		}

		public static Result<TValue> FromFailed(TValue value)
		{
			var result = From(false, value);
			return result;
		}
	}

	[Serializable]
	public struct Result<TState, TValue>
	{
		[SerializeField] private TValue _value;
		[SerializeField] private TState _state;

		public readonly TValue Value => _value;
		public readonly TState State => _state;

		private Result(TState state, TValue value)
		{
			_state = state;
			_value = value;
		}

		public static Result<TState, TValue> From(TState state, TValue value)
		{
			var result = new Result<TState, TValue>(state, value);
			return result;
		}
	}
}