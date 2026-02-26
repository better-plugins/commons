using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class InvalidResultException : ResultException
	{
		private const string DefaultMessage = "Result is invalid";
		private const string Description = "Invalid";

		public InvalidResultException(string message) : base(message)
		{
		}

		public InvalidResultException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public InvalidResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public InvalidResultException() : base(DefaultMessage)
		{
		}

		public InvalidResultException(string name, object result) : base(name, result, Description)
		{
		}
	}
}