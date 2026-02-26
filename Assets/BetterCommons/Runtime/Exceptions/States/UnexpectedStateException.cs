using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class UnexpectedStateException : StateException
	{
		private const string DefaultMessage = "State is unexpected";
		private const string Description = "Unexpected";

		public UnexpectedStateException(string message) : base(message)
		{
		}

		public UnexpectedStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public UnexpectedStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public UnexpectedStateException() : base(DefaultMessage)
		{
		}

		public UnexpectedStateException(object context) : base(Description, context)
		{
		}
	}
}