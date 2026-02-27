using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class ExpiredOperationException : OperationException
	{
		private const string DefaultMessage = "Operation was expired";
		private const string Description = "Expired";

		public ExpiredOperationException(string message) : base(message)
		{
		}

		public ExpiredOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public ExpiredOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public ExpiredOperationException() : base(DefaultMessage)
		{
		}

		public ExpiredOperationException(string name, object context) : base(name, Description, context)
		{
		}
	}
}