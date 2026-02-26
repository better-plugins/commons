namespace Better.Commons.Runtime.Interfaces
{
	public interface ICloneable<out T>
	{
		public T Clone();
	}
}