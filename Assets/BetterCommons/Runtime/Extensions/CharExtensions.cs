namespace Better.Commons.Runtime.Extensions
{
	public static class CharExtensions
	{
		public static char GetUpperCase(this char self)
		{
			var upper = char.ToUpper(self);
			return upper;
		}
	}
}