using Microsoft.Xna.Framework;
using System;

namespace AssortedModdingTools.Extensions
{
	public static partial class Extensions
	{
		public static Vector2 Perpendicular(this Vector2 vector) => new Vector2(0f - vector.Y, vector.X);
	}
}
