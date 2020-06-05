namespace AssortedModdingTools.Extensions
{
	public static partial class Extensions
	{
		public static void RemoveSpaces(this string str)
		{
			str.Replace(" ", "");
		}
	}
}