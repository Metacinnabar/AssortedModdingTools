using Terraria;
using Terraria.ModLoader;

namespace AssortedModdingTools.Systems.Misc
{
	public class DebugSystem : SystemBase
	{
		public override void OnMenuModeChange(int previousMenuMode)
		{
			if (Main.gameMenu)
				FPSCounterSystem.debugTexts["MenuState"] = "Main Menu State: " + Main.menuMode + ". Previous Main Menu State: " + previousMenuMode;
		}

        public override void PostDrawMenu()
		{
			if (Main.gameMenu)
				FPSCounterSystem.debugTexts["EnabledMods"] = $"Enabled Mod Count: {ModLoader.Mods.Length - 1}";
		}
    }
}