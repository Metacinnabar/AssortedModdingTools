using AssortedModdingTools.DataStructures.Misc;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System;
using Terraria;

namespace AssortedModdingTools.Systems.Misc
{
	public class FPSCounterSystem
	{
		private FrameCounter frameCounter = null;

		public string debugText = string.Empty;

		internal void Load()
		{
			frameCounter = new FrameCounter();
			Main.OnPostDraw += Main_OnPostDraw;
			On.Terraria.Main.DrawFPS += Main_DrawFPS;
		}

		internal void Unload()
		{
			frameCounter = null;
		}

		private void Main_OnPostDraw(GameTime gameTime) => frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

		private void Main_DrawFPS(On.Terraria.Main.orig_DrawFPS orig, Main self)
		{
			string text = $"Avg. FPS: {(int)Math.Round(frameCounter.AverageFramesPerSecond)}";

			if (debugText != string.Empty)
				text += "\nDebug Text: " + debugText;

			float textHeight = Main.fontMouseText.MeasureString(text).Y;

			Main.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(4, Main.gameMenu ? 4 : Main.screenHeight - textHeight - 4), Color.White);
		}
	}
}