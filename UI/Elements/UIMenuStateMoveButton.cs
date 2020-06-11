using AssortedModdingTools.DataStructures;
using AssortedModdingTools.Systems.Menu;
using AssortedModdingTools.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	//todo: hovering
	public class UIMenuStateMoveButton : UIBigTextWithBorder
	{
		public readonly UIState moveState;

		public UIMenuStateMoveButton(string text, UIState state, TextBorderColors? textBorderColor = null, Vector2? origin = null, float scale = 1f) 
			: base(text, textBorderColor, origin, scale)
		{
			moveState = state;
		}

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
			MenuSystem.MenuInterface.SetState(moveState);
		}
	}
}
