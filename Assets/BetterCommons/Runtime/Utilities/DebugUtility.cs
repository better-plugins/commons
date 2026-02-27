using System;
using System.Diagnostics;
using Better.Commons.Runtime.Extensions;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Better.Commons.Runtime.Utilities
{
	[DebuggerNonUserCode]
	public static class DebugUtility
	{
		public const int BriefLogDepth = 1;
		public const int DetailedLogDepth = int.MaxValue;
		public const float DefaultDrawDuration = 0.1f;
		public const bool DefaultDrawDepth = true;
		public const float DefaultCrosshairSize = 0.25f;

		private const string OperationStartMessage = "started...";
		private const string OperationFinishMessage = "finished!";

		public static void LogOperationStart()
		{
			Debug.Log(OperationStartMessage);
		}

		public static void LogOperationFinish()
		{
			Debug.Log(OperationFinishMessage);
		}

		public static void LogException<T>()
			where T : Exception, new()
		{
			var exception = new T();
			Debug.LogException(exception);
		}

		public static void LogException<T>(Object context)
			where T : Exception, new()
		{
			var exception = new T();
			Debug.LogException(exception, context);
		}

		public static void LogException<T>(string message)
			where T : Exception, new()
		{
			var exception = new T();
			exception.ReplaceMessageField(message);
			Debug.LogException(exception);
		}

		public static void LogException<T>(object message)
			where T : Exception, new()
		{
			var convertedMessage = Convert.ToString(message);
			LogException<T>(convertedMessage);
		}

		public static void LogException<T>(string message, Object context)
			where T : Exception, new()
		{
			var exception = new T();
			exception.ReplaceMessageField(message);
			Debug.LogException(exception, context);
		}

		public static void LogException<T>(object message, Object context)
			where T : Exception, new()
		{
			var convertedMessage = Convert.ToString(message);
			LogException<T>(convertedMessage, context);
		}

		public static void LogException(string message)
		{
			var exception = new Exception(message);
			Debug.LogException(exception);
		}

		public static void LogException(object message)
		{
			var convertedMessage = Convert.ToString(message);
			LogException(convertedMessage);
		}

		public static void LogException(string message, Object context)
		{
			var exception = new Exception(message);
			Debug.LogException(exception, context);
		}

		public static void LogException(object message, Object context)
		{
			var convertedMessage = Convert.ToString(message);
			LogException(convertedMessage, context);
		}

		public static void DrawCrosshair(
			Vector3 position,
			Quaternion rotation = default,
			float size = DefaultCrosshairSize,
			float duration = DefaultDrawDuration,
			bool depthTest = DefaultDrawDepth)
		{
			var axes = new (Vector3 direction, Color color)[]
			{
				(rotation * Vector3.right, Color.red),
				(rotation * Vector3.up, Color.green),
				(rotation * Vector3.forward, Color.blue),
			};

			foreach (var (direction, color) in axes)
			{
				Debug.DrawLine(
					position,
					position + direction * size,
					color,
					duration,
					depthTest);

				Debug.DrawLine(
					position,
					position - direction * size,
					Color.gray,
					duration,
					depthTest);
			}
		}
	}
}