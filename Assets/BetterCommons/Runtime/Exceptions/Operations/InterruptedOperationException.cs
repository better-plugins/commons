using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class InterruptedOperationException : OperationException
	{
		private const string DefaultMessage = "Operation was interrupted";
		private const string Description = "Interrupted";

		public InterruptedOperationException(string message) : base(message)
		{
		}

		public InterruptedOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public InterruptedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public InterruptedOperationException() : base(DefaultMessage)
		{
		}

		public InterruptedOperationException(string name, object context) : base(name, Description, context)
		{
		}
	}
}