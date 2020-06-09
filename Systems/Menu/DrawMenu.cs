using AssortedModdingTools.Helpers;
using AssortedModdingTools.Systems.Misc;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using static AssortedModdingTools.Systems.Reflection.ReflectionSystem;

namespace AssortedModdingTools.Systems.Menu
{
	public partial class MenuSystem
	{
		public static void DrawMenu(GameTime gameTime)
		{
			//float uiScaleWanted = Main._uiScaleWanted;
			//Main.UIScale = 1f;
			//Main._uiScaleWanted = uiScaleWanted;

			if (!(bool)IsEngineLoaded.GetValue(null))
			{
				IsEngineLoaded.SetValue(null, true);
				EventHelper.InvokeEvent(Main.instance, nameof(Main.OnEngineLoad));
			}

			GamepadMainMenuHandler.Update();
			GamepadMainMenuHandler.MenuItemPositions.Clear();

			//int num = Main.menuMode;

			if (MenuState <= MenuMode.PlayerSelection && Main.slimeRain)
				Main.StopSlimeRain();

			Main.render = false;

			Star.UpdateStars();
			Cloud.UpdateClouds();
			BiomeHelper.ResetTiles();

			Main.drawingPlayerChat = false;

			for (int i = 0; i < Main.numChatLines; i++)
			{
				Main.chatLine[i] = new ChatLine();
			}

			FPSCounterSystem.DrawFPS();

			Main.screenLastPosition = Main.screenPosition;
			Main.screenPosition.Y = (float)(Main.worldSurface * 16.0 - Main.screenHeight);

			if (Main.grabSky)
				Main.screenPosition.X += (Main.mouseX - Main.screenWidth / 2) * 0.02f;
			else
				Main.screenPosition.X += 2f;

			Main.background = 0;

			byte b = (byte)((255 + Main.tileColor.R * 2) / 3);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(b, b, b, 255);

			Microsoft.Xna.Framework.Color color2 = new Microsoft.Xna.Framework.Color((byte)(color.R * (Main.LogoA / 255f)), (byte)(color.G * (Main.LogoA / 255f)), (byte)(color.B * (Main.LogoA / 255f)), (byte)(color.A * (Main.LogoA / 255f)));
			Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color((byte)(color.R * (Main.LogoB / 255f)), (byte)(color.G * (Main.LogoB / 255f)), (byte)(color.B * (Main.LogoB / 255f)), (byte)(color.A * (Main.LogoB / 255f)));
			if (Main.dayTime)
			{
				Main.LogoA += 2;
				if (Main.LogoA > 255)
				{
					Main.LogoA = 255;
				}
				Main.LogoB--;
				if (Main.LogoB < 0)
				{
					Main.LogoB = 0;
				}
			}
			else
			{
				Main.LogoB += 2;
				if (Main.LogoB > 255)
				{
					Main.LogoB = 255;
				}
				Main.LogoA--;
				if (Main.LogoA < 0)
				{
					Main.LogoA = 0;
					Main.LogoT = true;
				}
			}
		}
	}
}