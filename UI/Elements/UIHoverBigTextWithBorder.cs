using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
    //todo hovering lol
    public class UIHoverBigTextWithBorder : UIBigTextWithBorder
    {
        public UIHoverBigTextWithBorder(string text, TextBorderHoverColors? textBorderHoverColors = null, Vector2? origin = null, float scale = 1f)
            : base(text, textBorderHoverColors.Value.TextBorderColors, origin, scale) { }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsMouseHovering)
            {
                if (scale < 1)
                    scale += 0.2f;
                if (scale > 1)
                    scale = 1;
            }
            else if (scale > 0.8f)
                scale -= 0.02f;
        }
    }
}