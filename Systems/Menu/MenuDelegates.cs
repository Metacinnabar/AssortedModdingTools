using AssortedModdingTools.Systems.Reflection;
using MonoMod.RuntimeDetour.HookGen;
using Terraria;
using Terraria.ModLoader;

namespace AssortedModdingTools.Systems.Menu
{
	public partial class MenuSystem : SystemBase
	{
		private delegate void Hook_AddMenuButtons(Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		private delegate void Orig_AddMenuButtons(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		private static event Hook_AddMenuButtons On_AddMenuButtons
		{
			add
			{
				HookEndpointManager.Add<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", ReflectionHelper.AllFlags), value);
			}
			remove
			{
				HookEndpointManager.Remove<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", ReflectionHelper.AllFlags), value);
			}
		}
	}
}