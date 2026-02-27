using System.Text;
using System.Threading;
using Better.Commons.Runtime.Interfaces;
using UnityEngine;

namespace Better.Commons.Runtime.Components
{
	public abstract class ExtendedMonoBehaviour : MonoBehaviour, IDebuggable
	{
		private CancellationTokenSource _aliveTokenSource;

		public bool Alive { get; private set; }

		public CancellationToken AliveToken
		{
			get
			{
				EnsureAliveTokenSource();
				return _aliveTokenSource.Token;
			}
		}

		protected virtual void Awake()
		{
			Alive = true;
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void FixedUpdate()
		{
		}

		protected virtual void OnBecameVisible()
		{
		}

		protected virtual void OnBecameInvisible()
		{
		}

		protected virtual void OnCollisionEnter(Collision collision)
		{
		}

		protected virtual void OnCollisionExit(Collision collision)
		{
		}

		protected virtual void OnCollisionStay(Collision collision)
		{
		}

		protected virtual void OnTriggerEnter(Collider other)
		{
		}

		protected virtual void OnTriggerExit(Collider other)
		{
		}

		protected virtual void OnTriggerStay(Collider other)
		{
		}

#if UNITY_PHYSICS_2D

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
		}

		protected virtual void OnCollisionExit2D(Collision2D collision)
		{
		}

		protected virtual void OnCollisionStay2D(Collision2D collision)
		{
		}

		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
		}

		protected virtual void OnTriggerExit2D(Collider2D other)
		{
		}

		protected virtual void OnTriggerStay2D(Collider2D other)
		{
		}

#endif

		protected virtual void OnApplicationFocus(bool hasFocus)
		{
		}

		protected virtual void OnApplicationPause(bool pauseStatus)
		{
		}

		protected virtual void OnApplicationQuit()
		{
		}

		protected virtual void OnPreCull()
		{
		}

		protected virtual void OnPreRender()
		{
		}

		protected virtual void OnPostRender()
		{
		}

		protected virtual void OnRenderObject()
		{
		}

		protected virtual void OnWillRenderObject()
		{
		}

		private void EnsureAliveTokenSource()
		{
			if (_aliveTokenSource != null)
			{
				return;
			}

			if (Alive)
			{
				_aliveTokenSource = new CancellationTokenSource();
				return;
			}

			// TODO: will be updated with #14
			// _aliveTokenSource = CancellationTokenSourceUtility.CancelledSource;
		}

		protected virtual void OnDestroy()
		{
			Alive = false;
			_aliveTokenSource?.Cancel();
		}

		protected virtual void OnDrawGizmos()
		{
		}

		protected virtual void OnDrawGizmosSelected()
		{
		}

		protected virtual void OnValidate()
		{
		}

		public virtual void CollectDebugInfo(int depth, ref StringBuilder builder)
		{
			// TODO: will be updated with #14
			// builder.AppendTypeLine(this)
			// .AppendFieldLine(nameof(name), name)
			// .AppendFieldLine(nameof(Alive), Alive)
			// .AppendFieldLine(nameof(enabled), enabled)
			// .AppendFieldLine(nameof(isActiveAndEnabled), isActiveAndEnabled);
		}

		protected virtual void Reset()
		{
		}
	}
}