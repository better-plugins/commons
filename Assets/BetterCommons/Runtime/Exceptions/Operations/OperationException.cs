using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Better.Commons.Runtime.Exceptions
{
	public class OperationException : UnityException
	{
		private const string DefaultMessage = "Operation error occurred";
		private const string MessageFormat = "{0} was occurred as {1} Operation in {2}";

		public OperationException(string message) : base(message)
		{
		}

		public OperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public OperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public OperationException() : this(DefaultMessage)
		{
		}

		protected OperationException(string name, string description, object context) : this(string.Format(MessageFormat, name, description, context))
		{
		}
	}
}