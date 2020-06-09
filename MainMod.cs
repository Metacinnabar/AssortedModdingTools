using AssortedModdingTools.Extensions;
using AssortedModdingTools.Helpers;
using AssortedModdingTools.Systems;
using AssortedModdingTools.Systems.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace AssortedModdingTools
{
	public class MainMod : Mod
	{
		public static FPSCounterSystem FPSCounter { get; private set; } = null;

		private static SystemBase[] systems;

		public override void Load()
		{
			// Initialization
			FPSCounter = new FPSCounterSystem();
			InitializeSystems();

			// Loading
			SystemBase.HookLoad();
			FPSCounter.Load();

			//Events
			Main.OnTick += Main_OnTick;
		}

		private static void InitializeSystems()
		{
			IEnumerable<Type> systemTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && t != typeof(SystemBase) && typeof(SystemBase).IsAssignableFrom(t));
			systems = systemTypes.DependencySort().Select(type => (SystemBase)Activator.CreateInstance(type)).ToArray();

			foreach (SystemBase system in systems)
			{
				DelegateHelper.AddToDelegate(ref SystemBase.HookLoad, system.Load);
				DelegateHelper.AddToDelegate(ref SystemBase.HookPostSetupContent, system.PostSetupContent);
				DelegateHelper.AddToDelegate(ref SystemBase.HookUnload, system.Unload);
				DelegateHelper.AddToDelegate(ref SystemBase.HookOnUpdate, system.OnUpdate);
				DelegateHelper.AddToDelegate(ref SystemBase.HookOnMenuModeChange, system.OnMenuModeChange);
			}
		}

		private void Main_OnTick() => SystemBase.HookOnUpdate();

		public override void Unload()
		{
			// Unloading
			SystemBase.HookUnload();
			FPSCounter?.Unload();

			// Field Nulling
			FPSCounter = null;
		}

		public override void PostSetupContent() => SystemBase.HookPostSetupContent();
	}
}