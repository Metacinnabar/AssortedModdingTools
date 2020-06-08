using System;
using Terraria;
using Terraria.ID;

namespace AssortedModdingTools.Systems.Menu
{
	public static class MenuHelper
	{
		public static void AddButton(string text, MenuMode menuMode, int selectedMenu, string[] buttonNames, ref int buttonIndex, ref int numButtons)
		{
			buttonNames[buttonIndex] = text;

			if (selectedMenu == buttonIndex)
			{
				Main.PlaySound(SoundID.MenuOpen);
				Main.menuMode = (int)menuMode;
			}

			buttonIndex++;
			numButtons++;
		}

		public static void AddButton(string text, Action act, int selectedMenu, string[] buttonNames, ref int buttonIndex, ref int numButtons)
		{
			buttonNames[buttonIndex] = text;

			if (selectedMenu == buttonIndex)
			{
				Main.PlaySound(SoundID.MenuOpen);
				act();
			}

			buttonIndex++;
			numButtons++;
		}
	}
}
