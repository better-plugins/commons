using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class InvalidDependencyException : DependencyException
	{
		private const string DefaultMessage = "Dependencies are invalid";
		private const string Description = "Invalid";

		public InvalidDependencyException(string message) : base(message)
		{
		}

		public InvalidDependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public InvalidDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public InvalidDependencyException() : base(DefaultMessage)
		{
		}

		public InvalidDependencyException(object context) : base(Description, context)
		{
		}
	}
}