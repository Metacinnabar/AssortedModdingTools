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
				Utils.DrawLine(spriteBatch, position + last, position + at, color);
				Utils.DrawLine(spriteBatch, position - last, position - at, color);
				Utils.DrawLine(spriteBatch, position + lastP, position + atP, color);
				Utils.DrawLine(spriteBatch, position - lastP, position - atP, color);
				last = at;
				lastP = atP;
			}
		}
	}
}
