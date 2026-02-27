using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class UnexpectedDependencyException : DependencyException
	{
		private const string DefaultMessage = "Dependencies are unexpected";
		private const string Description = "Unexpected";

		public UnexpectedDependencyException(string message) : base(message)
		{
		}

		public UnexpectedDependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public UnexpectedDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public UnexpectedDependencyException() : base(DefaultMessage)
		{
		}

		public UnexpectedDependencyException(object context) : base(Description, context)
		{
		}
	}
}