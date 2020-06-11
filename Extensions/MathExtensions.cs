using Microsoft.Xna.Framework;
using System;

namespace AssortedModdingTools.Extensions
{
	public static partial class Extensions
	{
		public static float Angle(this Vector2 vector) => (float)Math.Atan2(vector.Y, vector.X);

		public static Vector2 Perpendicular(this Vector2 vector) => new Vector2(0f - vector.Y, vector.X);

		public static float Angle(this Vector2 from, Vector2 to) => (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
	}
}
