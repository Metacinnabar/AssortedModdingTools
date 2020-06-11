using AssortedModdingTools.Systems.Menu;
using Terraria.UI;

namespace AssortedModdingTools.UI.States.Menu
{
	//todo: do logo here since logo is visible throught ever menu state ( i think )
	public abstract class MenuState : UIState
	{
		public abstract MenuModes MenuMode { get; }
	}
}
