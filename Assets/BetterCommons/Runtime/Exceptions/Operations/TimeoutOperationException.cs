using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class TimeoutOperationException : OperationException
	{
		private const string DefaultMessage = "Operation was timed out";
		private const string Description = "Timeout";

		public TimeoutOperationException(string message) : base(message)
		{
		}

		public TimeoutOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public TimeoutOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public TimeoutOperationException() : base(DefaultMessage)
		{
		}

		public TimeoutOperationException(string name, object context) : base(name, Description, context)
		{
		}
	}
}