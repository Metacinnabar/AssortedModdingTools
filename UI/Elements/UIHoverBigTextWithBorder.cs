using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UIHoverBigTextWithBorder : UIBigTextWithBorder
	{
		public float minScale, maxScale;

		public TextBorderHoverColors textBorderHoverColors;

		public UIHoverBigTextWithBorder(string text, TextBorderHoverColors? textBorderHoverColors = null, Vector2? origin = null, float maxscale = 1f)
			: base(text, textBorderHoverColors.Value.TextBorderColors, origin, maxscale)
		{
			minScale = maxscale - 0.2f;
			maxScale = maxscale;
			this.textBorderHoverColors = textBorderHoverColors ?? TextBorderHoverColors.DefaultHover;
		}

		//disregard the other draw and replace
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point16 pos = new Point16((short)dimensions.X, (short)dimensions.Y);
			Color color = textBorderHoverColors.textColor;
			Color borderColor = textBorderHoverColors.borderColor;

			if (IsMouseHovering)
			{
				color = textBorderHoverColors.textHoverColor;
				borderColor = textBorderHoverColors.borderHoverColor;
			}

			Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, text, pos.X, pos.Y, color, borderColor, origin, scale);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (IsMouseHovering)
			{
				if (scale < maxScale)
					scale += 0.015f;

				if (scale > maxScale)
					scale = maxScale;
			}
			else if (scale > minScale)
				scale -= 0.015f;
		}
	}
}