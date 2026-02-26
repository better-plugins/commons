using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class UnexpectedResultException : ResultException
	{
		private const string DefaultMessage = "Result is unexpected";
		private const string Description = "Unexpected";

		public UnexpectedResultException(string message) : base(message)
		{
		}

		public UnexpectedResultException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public UnexpectedResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public UnexpectedResultException() : base(DefaultMessage)
		{
		}

		public UnexpectedResultException(string name, object result) : base(name, result, Description)
		{
		}
	}
}