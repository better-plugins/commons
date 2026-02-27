using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Better.Commons.Runtime.Exceptions
{
	public class StateException : UnityException
	{
		private const string DefaultMessage = "State error occurred";
		private const string MessageFormat = "{0}State for {1}";

		public StateException(string message) : base(message)
		{
		}

		public StateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public StateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public StateException() : this(DefaultMessage)
		{
		}

		protected StateException(string description, object context) : this(string.Format(MessageFormat, description, context))
		{
		}
	}
}