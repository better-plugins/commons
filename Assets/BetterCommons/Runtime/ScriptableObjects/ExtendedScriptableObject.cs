using System.Text;
using Better.Commons.Runtime.Interfaces;
using UnityEngine;

namespace Better.Commons.Runtime.ScriptableObjects
{
	public abstract class ExtendedScriptableObject : ScriptableObject, IDebuggable
	{
		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		protected virtual void OnValidate()
		{
		}

		protected virtual void Reset()
		{
		}

		public virtual void CollectDebugInfo(int depth, ref StringBuilder builder)
		{
			// TODO: will be updated with #14
			// builder.AppendTypeLine(this);
		}
	}
}