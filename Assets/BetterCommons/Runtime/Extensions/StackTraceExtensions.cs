using System;
using System.Collections.Generic;
using System.Diagnostics;
using Better.Commons.Runtime.Enumerators;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class StackTraceExtensions
	{
		public static IEnumerator<StackFrame> GetFramesEnumerator(this StackTrace self, int skipFrames = StackTraceEnumerator.DefaultSkipFrames)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return StubEnumerator<StackFrame>.Default;
			}

			var enumerator = new StackTraceEnumerator(self, skipFrames);
			return enumerator;
		}
	}
}