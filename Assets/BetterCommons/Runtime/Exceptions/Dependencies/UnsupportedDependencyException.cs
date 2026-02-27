using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class UnsupportedDependencyException : DependencyException
	{
		private const string DefaultMessage = "Dependencies are unsupported";
		private const string Description = "Unsupported";

		public UnsupportedDependencyException(string message) : base(message)
		{
		}

		public UnsupportedDependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public UnsupportedDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public UnsupportedDependencyException() : base(DefaultMessage)
		{
		}

		public UnsupportedDependencyException(object context) : base(Description, context)
		{
		}
	}
}