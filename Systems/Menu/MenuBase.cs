using AssortedModdingTools.Systems;
using System;
using System.Collections.Generic;

namespace AssortedModdingTools
{
	//Todo move to systembase
	public abstract class MenuBase : SystemBase
	{
		public static Action HookPreDrawMenu;
		public static Action HookPostDrawMenu;

		public virtual void PreDrawMenu() { }

		public virtual void PostDrawMenu() { }
	}
}