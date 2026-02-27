using System;
using System.Runtime.Serialization;

namespace Better.Commons.Runtime.Exceptions
{
	public class DeprecatedDependencyException : DependencyException
	{
		private const string DefaultMessage = "Dependencies was deprecated";
		private const string Description = "Deprecated";

		public DeprecatedDependencyException(string message) : base(message)
		{
		}

		public DeprecatedDependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public DeprecatedDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public DeprecatedDependencyException() : base(DefaultMessage)
		{
		}

		public DeprecatedDependencyException(object context) : base(Description, context)
		{
		}
	}
}