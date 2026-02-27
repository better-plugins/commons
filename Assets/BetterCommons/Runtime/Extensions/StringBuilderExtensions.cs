using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Better.Commons.Runtime.Interfaces;
using Better.Commons.Runtime.Utilities;

namespace Better.Commons.Runtime.Extensions
{
	public static class StringBuilderExtensions
	{
		private const string FieldSeparator = ": ";
		private const string InlineElementsSeparator = ", ";

		public static bool EndsWith(this StringBuilder self, string value)
		{
			if (self == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(self));
				return false;
			}

			var endsWith = self.ToString()
				.EndsWith(value);

			return endsWith;
		}

		public static StringBuilder Append(this StringBuilder self, IDebuggable debuggable, int sourceDepth)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (debuggable == null)
			{
				self.Append((IDebuggable)null);
				return self;
			}

			var depth = sourceDepth - 1;

			if (depth <= 0)
			{
				self.Append(debuggable);
			}
			else
			{
				debuggable.CollectDebugInfo(depth, ref self);
			}

			return self;
		}

		public static StringBuilder AppendType(this StringBuilder self, Type type)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (type == null)
			{
				self.Append((Type)null);
			}
			else
			{
				self.Append(type.Name);
			}

			return self;
		}

		public static StringBuilder AppendType(this StringBuilder self, object source)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (source == null)
			{
				self.Append((object)null);
			}
			else
			{
				var type = source.GetType();
				self.Append(type.Name);
			}

			return self;
		}

		public static StringBuilder AppendLine(this StringBuilder self, object value)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (value == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(value));
				return self;
			}

			var convertedValue = Convert.ToString(value);
			self.AppendLine(convertedValue);
			return self;
		}

		public static StringBuilder AppendLine(this StringBuilder self, IDebuggable debuggable, int sourceDepth)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.Append(debuggable, sourceDepth);

			if (!self.EndsWith(Environment.NewLine))
			{
				self.AppendLine();
			}

			return self;
		}

		public static StringBuilder AppendField(this StringBuilder self, string name, object value)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendJoin(FieldSeparator, name, value);
			return self;
		}

		public static StringBuilder AppendField(this StringBuilder self, string name, string value)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendJoin(FieldSeparator, name, value);
			return self;
		}

		public static StringBuilder AppendField<T>(this StringBuilder self, string name, IEnumerable<T> values, bool each = false)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				self.AppendField(name, null);
				return self;
			}

			var count = values.Count();
			self.AppendField(name, count);

			if (each && count > 0)
			{
				self.Append(CharUtility.WhiteSpace)
					.Append(CharUtility.LeftBracket)
					.AppendJoin(InlineElementsSeparator, values)
					.Append(CharUtility.RightBracket);
			}

			return self;
		}

		public static StringBuilder AppendField<T>(this StringBuilder self, string name, IEnumerable<T> values, int sourceDepth)
			where T : IDebuggable
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				self.AppendField(name, null);
				return self;
			}

			var depth = sourceDepth - 1;
			var hasDepth = depth > 0;
			var inlineEach = hasDepth && depth == 1;
			var valueArray = values.ToArray();
			self.AppendField(name, valueArray, inlineEach);

			if (!hasDepth || inlineEach)
			{
				return self;
			}

			for (var i = 0; i < valueArray.Length; i++)
			{
				var subName = Convert.ToString(i);

				self.AppendLine()
					.AppendField(subName, valueArray[i], depth);
			}

			return self;
		}

		public static StringBuilder AppendField(this StringBuilder self, string name, IDebuggable debuggable, int sourceDepth)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.Append(name)
				.Append(FieldSeparator)
				.Append(debuggable, sourceDepth);

			return self;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder self, IFormatProvider provider, string format, object arg0)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (provider == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(provider));
				return self;
			}

			self.AppendFormat(provider, format, arg0)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFormatLine(
			this StringBuilder self,
			IFormatProvider provider,
			string format,
			object arg0,
			object arg1)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (provider == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(provider));
				return self;
			}

			self.AppendFormat(provider, format, arg0, arg1)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFormatLine(
			this StringBuilder self,
			IFormatProvider provider,
			string format,
			object arg0,
			object arg1,
			object arg2)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (provider == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(provider));
				return self;
			}

			self.AppendFormat(
				provider,
				format,
				arg0,
				arg1,
				arg2);

			self.AppendLine();
			return self;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder self, IFormatProvider provider, string format, params object[] args)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (provider == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(provider));
				return self;
			}

			self.AppendFormat(provider, format, args)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder self, string format, object arg0)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendFormat(format, arg0)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder self, string format, object arg0, object arg1)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendFormat(format, arg0, arg1)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFormatLine(
			this StringBuilder self,
			string format,
			object arg0,
			object arg1,
			object arg2)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendFormat(format, arg0, arg1, arg2)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFormatLine(this StringBuilder self, string format, params object[] args)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (args == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(args));
				return self;
			}

			self.AppendFormat(format, args)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendJoinLine(this StringBuilder self, char separator, params object[] values)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(values));
				return self;
			}

			self.AppendJoin(separator, values)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendJoinLine(this StringBuilder self, char separator, params string[] values)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(values));
				return self;
			}

			self.AppendJoin(separator, values)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendJoinLine(this StringBuilder self, string separator, params object[] values)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(values));
				return self;
			}

			self.AppendJoin(separator, values)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendJoinLine(this StringBuilder self, string separator, params string[] values)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(values));
				return self;
			}

			self.AppendJoin(separator, values)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendJoinLine<T>(this StringBuilder self, char separator, IEnumerable<T> values)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(values));
				return self;
			}

			self.AppendJoin(separator, values)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendJoinLine<T>(this StringBuilder self, string separator, IEnumerable<T> values)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			if (values == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(values));
				return self;
			}

			self.AppendJoin(separator, values)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendTypeLine(this StringBuilder self, Type type)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendType(type)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendTypeLine(this StringBuilder self, object source)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendType(source)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFieldLine(this StringBuilder self, string name, object value)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendField(name, value)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFieldLine(this StringBuilder self, string name, string value)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendField(name, value)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFieldLine(this StringBuilder self, string name, IDebuggable debuggable, int sourceDepth)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.Append(name)
				.Append(FieldSeparator)
				.AppendLine(debuggable, sourceDepth);

			return self;
		}

		public static StringBuilder AppendFieldLine<T>(this StringBuilder self, string name, IEnumerable<T> values, bool each = false)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendField(name, values, each)
				.AppendLine();

			return self;
		}

		public static StringBuilder AppendFieldLine<T>(this StringBuilder self, string name, IEnumerable<T> values, int sourceDepth)
			where T : IDebuggable
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			self.AppendField(name, values, sourceDepth)
				.AppendLine();

			return self;
		}
	}
}