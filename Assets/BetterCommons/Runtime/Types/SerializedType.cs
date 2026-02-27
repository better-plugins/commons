using System;
using UnityEngine;

namespace Better.Commons.Runtime.Types
{
	[Serializable]
	public class SerializedType : ISerializationCallbackReceiver, IEquatable<SerializedType>
	{
		[SerializeField] private string _fullQualifiedName;

		private Type _type;

		public Type Type
		{
			get
			{
				if (_type == null)
				{
					_type = Type.GetType(_fullQualifiedName, false);
				}

				return _type;
			}
		}

		public SerializedType(string qualifiedTypeName)
		{
			_fullQualifiedName = qualifiedTypeName;
		}

		public SerializedType(Type type) : this(type.AssemblyQualifiedName)
		{
			_type = type;
		}

		public SerializedType() : this(string.Empty)
		{
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			// TODO: will be updated with #14
			// TypeUtility.TryGetType(_fullQualifiedName, out _type);
			Validate();
		}

		private protected bool Validate(bool logWarnings = true)
		{
			// TODO: will be updated with #14
			// var isValid = _type != null || TypeUtility.TryGetType(_fullQualifiedName, out _type);
			var isValid = true;
			
			if (!isValid && logWarnings)
			{
				var message = $"Type cannot be found by {nameof(_fullQualifiedName)}({_fullQualifiedName})";
				Debug.LogWarning(message);
			}

			return isValid;
		}

		public bool Equals(SerializedType other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			var equal = Type == other.Type;
			return equal;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			// TODO: will be updated with #14
			// if (obj.CompareTypes(this))
			// {
				// return false;
			// }

			var equal = Equals((SerializedType)obj);
			return equal;
		}

		public override int GetHashCode()
		{
			if (Type == null)
			{
				return 0;
			}

			var hashCode = Type.GetHashCode();
			return hashCode;
		}

		public override string ToString()
		{
			var convertedType = Convert.ToString(_type);
			return convertedType;
		}

		public static implicit operator string(SerializedType serializedType)
		{
			return serializedType._fullQualifiedName;
		}

		public static implicit operator Type(SerializedType serializedType)
		{
			return serializedType.Type;
		}

		public static implicit operator SerializedType(Type type)
		{
			var serializedType = new SerializedType(type);
			return serializedType;
		}
	}
}