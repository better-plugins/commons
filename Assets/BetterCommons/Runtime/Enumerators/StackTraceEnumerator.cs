using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Enumerators
{
	public struct StackTraceEnumerator : IEnumerator<StackFrame>
	{
		public const int DefaultSkipFrames = 0;

		private readonly StackTrace _stackTrace;
		private readonly int _skipFrames;

		private int _index;

		public StackFrame Current { get; private set; }
		object IEnumerator.Current => Current;

		public StackTraceEnumerator(StackTrace stackTrace, int skipFrames = DefaultSkipFrames)
		{
			if (skipFrames < 0)
			{
				var skipRangeMessage = $"{nameof(skipFrames)}({skipFrames}) cannot be less than zero, will be clamped to {nameof(DefaultSkipFrames)}({DefaultSkipFrames})";
				DebugUtility.LogException<ArgumentOutOfRangeException>(skipRangeMessage);
				skipFrames = DefaultSkipFrames;
			}

			_stackTrace = stackTrace;
			_skipFrames = skipFrames;

			_index = -1;
			Current = null;
			Reset();
		}

		public bool MoveNext()
		{
			var maxIndex = _stackTrace.FrameCount - 1;

			if (_index >= maxIndex)
			{
				return false;
			}

			_index++;
			Current = _stackTrace.GetFrame(_index);
			return true;
		}

		public void Reset()
		{
			_index = _skipFrames - 1;
			Current = null;
		}

		public void Dispose()
		{
		}
	}
}