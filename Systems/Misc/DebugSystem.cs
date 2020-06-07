using AssortedModdingTools.Systems;
using Terraria;

namespace AssortedModdingTools.Systems.Misc
{
	public class DebugSystem : SystemBase
	{
		public override void OnMenuModeChange(int previousMenuMode)
		{
			if (MainMod.FPSCounter != null && Main.gameMenu)
				MainMod.FPSCounter.debugText = "Main Menu State: " + Main.menuMode + ". Previous Main Menu State: " + previousMenuMode;
		}
	}
}