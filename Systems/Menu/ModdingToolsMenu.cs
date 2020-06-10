using AssortedModdingTools.UI.States;
using Terraria;

namespace AssortedModdingTools.Systems.Menu
{
	public partial class MenuSystem : SystemBase
	{
		public static UIAdvancedCreateMod createMod = new UIAdvancedCreateMod();

		public override void PreDrawMenu()
		{
			if (Main.menuMode == (int)MenuModes.ModdingTools)
				DrawModdingToolsMenu();
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
	}
}