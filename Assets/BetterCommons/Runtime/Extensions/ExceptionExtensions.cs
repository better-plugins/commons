using System;
using System.Reflection;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class ExceptionExtensions
	{
		private const string MessageFieldName = "_message";
		private static FieldInfo MessageField { get; }

		static ExceptionExtensions()
		{
			MessageField = GetExceptionMessageField();
		}

		public static void ReplaceMessageField(this Exception self, string message)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return;
			}

			if (self.Message.Equals(message))
			{
				return;
			}

			MessageField.SetValue(self, message);
		}

		public static void ReplaceMessageField(this Exception self, object message)
		{
			if (message == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(message));
				return;
			}

			var convertedMessage = Convert.ToString(message);
			self.ReplaceMessageField(convertedMessage);
		}

		private static FieldInfo GetExceptionMessageField()
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
			var type = typeof(Exception);
			var field = type.GetField(MessageFieldName, flags);

			return field;
		}
	}
}