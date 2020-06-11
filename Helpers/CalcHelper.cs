using Microsoft.Xna.Framework;
using System;

namespace AssortedModdingTools.Helpers
{
	public static class CalcHelper
	{
		public static Vector2 AngleToVector(float angleRadians, float length) => new Vector2((float)Math.Cos(angleRadians) * length, (float)Math.Sin(angleRadians) * length);
	}
}
