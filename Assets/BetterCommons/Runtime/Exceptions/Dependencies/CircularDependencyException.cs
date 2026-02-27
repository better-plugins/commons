using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class CircularDependencyException : DependencyException
	{
		private const string DefaultMessage = "Dependencies has circularity problem";
		private const string Description = "Problem of circularity";

		public CircularDependencyException(string message) : base(message)
		{
		}

		public CircularDependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public CircularDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public CircularDependencyException() : base(DefaultMessage)
		{
		}

		public CircularDependencyException(object context) : base(Description, context)
		{
		}
	}
}