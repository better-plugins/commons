using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class NotFoundResultException : ResultException
	{
		private const string DefaultMessage = "Result not found";
		private const string Description = "Not Found";

		public NotFoundResultException(string message) : base(message)
		{
		}

		public NotFoundResultException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public NotFoundResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public NotFoundResultException() : base(DefaultMessage)
		{
		}

		public NotFoundResultException(string name, object result) : base(name, result, Description)
		{
		}
	}
}