using AssortedModdingTools.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AssortedModdingTools.Extensions
{
	//i didnt yeet this code from celeste. nope.
	//todo: test
	public static partial class Extensions
	{
		public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 position, float radius, Color color, int resolution)
		{
			Vector2 last = Vector2.UnitX * radius;
			Vector2 lastP = last.Perpendicular();

			for (int i = 1; i <= resolution; i++)
			{
				Vector2 at = CalcHelper.AngleToVector(i * 1.57079637f / resolution, radius);
				Vector2 atP = at.Perpendicular();
				spriteBatch.DrawLine(position + last, position + at, color);
				spriteBatch.DrawLine(position - last, position - at, color);
				spriteBatch.DrawLine(position + lastP, position + atP, color);
				spriteBatch.DrawLine(position - lastP, position - atP, color);
				last = at;
				lastP = atP;
			}
		}

		public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color) => spriteBatch.DrawLineAngle(start, start.Angle(end), Vector2.Distance(start, end), color);

		public static void DrawLineAngle(this SpriteBatch spriteBatch, Vector2 start, float angle, float length, Color color)
		{
			spriteBatch.Draw(Main.magicPixel, start, new Rectangle(0, 0, 1, 1), color, angle, Vector2.Zero, new Vector2(length, 1f), SpriteEffects.None, 0f);
		}
	}
}
