using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;

namespace AssortedModdingTools.Systems.Misc
{
	public class FPSCounterSystem : SystemBase
	{
		private static FrameCounter frameCounter = null;

		public static Dictionary<string, string> debugTexts = new Dictionary<string, string>();

		public override void Load()
		{
			frameCounter = new FrameCounter();
			Main.OnPostDraw += Main_OnPostDraw;
			On.Terraria.Main.DrawFPS += Main_DrawFPS;
		}

		public override void Unload()
		{
			Main.OnPostDraw -= Main_OnPostDraw;
			frameCounter = null;
		}

		private static void Main_OnPostDraw(GameTime gameTime) => frameCounter?.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

		private static void Main_DrawFPS(On.Terraria.Main.orig_DrawFPS orig, Main self) => DrawFPS();

		public static void DrawFPS()
		{
			string text = $"Avg. FPS: {(int)frameCounter.AverageFramesPerSecond}";

			foreach (string key in debugTexts.Keys)
			{
				debugTexts.TryGetValue(key, out string debug);
				text += "\n" + debug;
			}

			float textHeight = Main.fontMouseText.MeasureString(text).Y;
			Main.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(4, Main.gameMenu ? 4 : Main.screenHeight - textHeight - 4), Color.White);
		}
	}
}