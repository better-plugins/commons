using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class MissingDependencyException : DependencyException
	{
		private const string DefaultMessage = "Dependencies are missed";
		private const string Description = "Missing";

		public MissingDependencyException(string message) : base(message)
		{
		}

		public MissingDependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public MissingDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public MissingDependencyException() : base(DefaultMessage)
		{
		}

		public MissingDependencyException(object context) : base(Description, context)
		{
		}
	}
}