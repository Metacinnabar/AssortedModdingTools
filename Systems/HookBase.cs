using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace AssortedModdingTools.Systems
{
	public abstract class HookBase
	{
		internal static class BaseHookLoader
		{
			public static List<HookBase> hookBases;

			public static void Initialize()
			{
				hookBases = new List<HookBase>();
			}

			public static void CallOnLoad(Mod mod)
			{
				for (int i = 0; i < hookBases.Count; i++)
				{
					HookBase menuBase = hookBases[i];
					menuBase.HookOnLoad?.Invoke(mod);
				}
			}

			public static void CallOnUnload()
			{
				for (int i = 0; i < hookBases.Count; i++)
				{
					HookBase menuBase = hookBases[i];
					menuBase.HookOnUnload?.Invoke();
				}
			}

			public static void CallOnUpdate()
			{
				for (int i = 0; i < hookBases.Count; i++)
				{
					HookBase menuBase = hookBases[i];
					menuBase.HookOnUpdate?.Invoke();
				}
			}
		}

		public event Action HookOnUpdate;

		public event Action<Mod> HookOnLoad;

		public event Action HookOnUnload;
	}
}