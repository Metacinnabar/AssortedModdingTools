using System.Reflection;
using Terraria;

namespace AssortedModdingTools.Systems.Reflection
{
	public static class ReflectionHelper
	{
		public static readonly BindingFlags AllFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

		public static readonly Assembly TerrariaAsb = Assembly.GetAssembly(typeof(Main));
	}
}