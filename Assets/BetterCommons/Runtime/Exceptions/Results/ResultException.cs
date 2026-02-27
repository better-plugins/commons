using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Better.Commons.Runtime.Exceptions
{
	public class ResultException : UnityException
	{
		private const string DefaultMessage = "Result error occurred";
		private const string MessageFormat = "Result of {0}({1}) is a {2}";

		public ResultException(string message) : base(message)
		{
		}

		public ResultException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public ResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public ResultException() : this(DefaultMessage)
		{
		}

		protected ResultException(string name, object result, string description) : this(string.Format(MessageFormat, name, result, description))
		{
		}
	}
}