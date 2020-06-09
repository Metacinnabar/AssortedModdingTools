using System;

namespace AssortedModdingTools.Systems
{
	public abstract class SystemBase
	{
		public static Action HookLoad;
		public static Action HookUnload;
		public static Action HookOnUpdate;
		public static Action<int> HookOnMenuModeChange;
		public static Action HookPostSetupContent;
		public static Action HookPreDrawMenu;
		public static Action HookPostDrawMenu;

		public virtual void Load() { }

		public virtual void OnUpdate() { }

		public virtual void PostSetupContent() { }

		public virtual void OnMenuModeChange(int previousMenuMode) { }

		public virtual void Unload() { }

		public virtual void PreDrawMenu() { }

		public virtual void PostDrawMenu() { }

		internal SystemBase() { }

		public virtual void Dispose() { }

		//Todo Call on Unload
		//Todo do this?
		protected static void StaticDispose()
		{
			//OverhaulUtils.UnloadCollection<SystemBase>(ref systems, true);
		}
	}
}