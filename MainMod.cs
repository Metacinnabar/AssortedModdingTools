using AssortedModdingTools.Systems;
using AssortedModdingTools.Systems.Menu;
using AssortedModdingTools.Systems.Misc;
using AssortedModdingTools.Systems.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace AssortedModdingTools
{
	public class MainMod : Mod
	{
		public static ReflectionManager Reflection { get; private set; } = null;
		public static FPSCounterSystem FPSCounter { get; private set; } = null;

		public override void Load()
		{
			// Initialization
			Reflection = new ReflectionManager();
			FPSCounter = new FPSCounterSystem();
			HookLoader.Initialize();

			// Loading
			Reflection.Load();
			FPSCounter.Load();
			HookLoader.CallLoad();
			HookBase.BaseHookLoader.CallOnLoad(this);
			MenuSwap.Load();

			Main.OnTick += Main_OnTick;
		}

		private void Main_OnTick()
		{
			FPSCounter.debugText = "Main Menu State: " + Main.menuMode;
		}

		public override void Unload()
		{
			// Unloading
			MenuSwap.Unload();
			Reflection?.Unload();
			FPSCounter?.Unload();

			// Field Nulling
			Reflection = null;
			FPSCounter = null;
		}
	}
}