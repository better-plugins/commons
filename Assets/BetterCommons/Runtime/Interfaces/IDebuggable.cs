using System.Text;

namespace Better.Commons.Runtime.Interfaces
{
	public interface IDebuggable
	{
		public void CollectDebugInfo(int depth, ref StringBuilder builder);
	}
}