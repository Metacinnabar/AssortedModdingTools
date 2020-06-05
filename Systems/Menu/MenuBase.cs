using AssortedModdingTools.Systems;
using System;
using System.Collections.Generic;

namespace AssortedModdingTools
{
	public abstract class MenuBase : HookBase
	{
		internal static class MenuBaseHookLoader
		{
			public static List<MenuBase> menuBases;

			public static void Initialize()
			{
				menuBases = new List<MenuBase>();
			}

			public static void CallPreDrawMenu()
			{
				for (int i = 0; i < menuBases.Count; i++)
				{
					MenuBase menuBase = menuBases[i];
					menuBase.HookPreDrawMenu?.Invoke();
				}
			}

			public static void CallPostDrawMenu()
			{
				for (int i = 0; i < menuBases.Count; i++)
				{
					MenuBase menuBase = menuBases[i];
					menuBase.HookPostDrawMenu?.Invoke();
				}
			}
		}

		public event Action HookPreDrawMenu;

		public event Action HookPostDrawMenu;
	}
}