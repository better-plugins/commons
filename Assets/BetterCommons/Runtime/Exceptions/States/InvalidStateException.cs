using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class InvalidStateException : StateException
	{
		private const string DefaultMessage = "State is invalid";
		private const string Description = "Invalid";

		public InvalidStateException(string message) : base(message)
		{
		}

		public InvalidStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public InvalidStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public InvalidStateException() : base(DefaultMessage)
		{
		}

		public InvalidStateException(object context) : base(Description, context)
		{
		}
	}
}