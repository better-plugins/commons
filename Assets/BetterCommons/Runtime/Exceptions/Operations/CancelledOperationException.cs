using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class CancelledOperationException : OperationException
	{
		private const string DefaultMessage = "Operation was cancelled";
		private const string Description = "Cancelled";

		public CancelledOperationException(string message) : base(message)
		{
		}

		public CancelledOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public CancelledOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public CancelledOperationException() : base(DefaultMessage)
		{
		}

		public CancelledOperationException(string name, object context) : base(name, Description, context)
		{
		}
	}
}