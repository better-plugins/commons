using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class UnexpectedOperationException : OperationException
	{
		private const string DefaultMessage = "Operation was unexpected";
		private const string Description = "Unexpected";

		public UnexpectedOperationException(string message) : base(message)
		{
		}

		public UnexpectedOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public UnexpectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public UnexpectedOperationException() : base(DefaultMessage)
		{
		}

		public UnexpectedOperationException(string name, object context) : base(name, Description, context)
		{
		}
	}
}