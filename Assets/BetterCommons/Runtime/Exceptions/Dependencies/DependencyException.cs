using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Better.Commons.Runtime.Exceptions
{
	public class DependencyException : UnityException
	{
		private const string DefaultMessage = "Dependencies error occurred";
		private const string MessageFormat = "{0} dependencies detected in {1}";

		public DependencyException(string message) : base(message)
		{
		}

		public DependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public DependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public DependencyException() : this(DefaultMessage)
		{
		}

		protected DependencyException(string description, object context) : this(string.Format(MessageFormat, description, context))
		{
		}
	}
}