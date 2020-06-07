using AssortedModdingTools.Extensions;
using AssortedModdingTools.Systems;
using AssortedModdingTools.Systems.Menu;
using AssortedModdingTools.Systems.Misc;
using AssortedModdingTools.Systems.Reflection;
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
		public static ReflectionManager Reflection { get; private set; } = null;
		public static FPSCounterSystem FPSCounter { get; private set; } = null;

		private static SystemBase[] systems;

		public override void Load()
		{
			// Initialization
			Reflection = new ReflectionManager();
			FPSCounter = new FPSCounterSystem();
			InitializeSystems();

			// Loading
			SystemBase.HookLoad();
			Reflection.Load();
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

		private void Main_OnTick()
		{
			SystemBase.HookOnUpdate();
		}

		public override void Unload()
		{
			// Unloading
			SystemBase.HookUnload();
			Reflection?.Unload();
			FPSCounter?.Unload();

			// Field Nulling
			Reflection = null;
			FPSCounter = null;
		}

		public override void PostSetupContent()
		{
			SystemBase.HookPostSetupContent();
		}
	}
}