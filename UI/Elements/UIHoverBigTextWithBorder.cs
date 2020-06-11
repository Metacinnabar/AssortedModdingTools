using AssortedModdingTools.DataStructures;
using AssortedModdingTools.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UIHoverBigTextWithBorder : UIBigTextWithBorder
	{
		public UIHoverBigTextWithBorder(string text, TextBorderHoverColors? textBorderHoverColors = null, Vector2? origin = null, float scale = 1f) 
			: base(text, textBorderHoverColors.Value.TextBorderColors, origin, scale) { }

		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);

		}
	}
}
