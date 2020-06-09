using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.UI;

namespace AssortedModdingTools.Extensions
{
	public static partial class Extensions
	{
		public static Vector2 ToVector2(this CalculatedStyle style) => new Vector2(style.X, style.Y);

		public static Point ToPoint(this CalculatedStyle style) => new Point((int)style.X, (int)style.Y);

		public static Point16 ToPoint16(this CalculatedStyle style) => new Point16((short)style.X, (short)style.Y);
	}
}