using AssortedModdingTools.DataStructures.UI.Menu;
using AssortedModdingTools.UI.States;
using Terraria;
using Terraria.ModLoader;
using static AssortedModdingTools.Systems.HookBase;

namespace AssortedModdingTools.Systems.Menu
{
	public class ModdingToolsMenu : MenuBase, IHookBase
	{
		internal static UIAdvancedCreateMod createMod = new UIAdvancedCreateMod();

		private void OnLoad(Mod mod) => mod.Logger.Debug("Hook \"OnLoad\" was called!");

		private void PreDrawMenu()
		{
			if (Main.menuMode == (int)MenuMode.ModdingTools)
			{
				DrawModdingToolsMenu();
			}
		}

		private void DrawModdingToolsMenu()
		{
			Main.MenuUI.SetState(createMod);
			Main.menuMode = 888;
		}

		public static void OnModdingToolsButtonClick()
		{
			Main.MenuUI.SetState(createMod);
			Main.menuMode = 888;
		}

		public void Load()
		{
			BaseHookLoader.hookBases.Add(this);
			MenuBaseHookLoader.menuBases.Add(this);
			HookOnLoad += OnLoad;
			HookPreDrawMenu += PreDrawMenu;
		}
	}
}