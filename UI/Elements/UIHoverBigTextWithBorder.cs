using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	//todo hovering lol
	public class UIHoverBigTextWithBorder : UIBigTextWithBorder
	{
		public UIHoverBigTextWithBorder(string text, TextBorderHoverColors? textBorderHoverColors = null, Vector2? origin = null, float scale = 1f)
			: base(text, textBorderHoverColors.Value.TextBorderColors, origin, scale) { }

		//disregard the other draw and replace
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point16 pos = new Point16((short)dimensions.X, (short)dimensions.Y);
			byte b = (byte)((255 + Main.tileColor.R * 2) / 3); //wat
			Color color = new Color(b, b, b);

			color = new Color((color.R + 35) / 2, (color.G + 35) / 2, (color.B + 35) / 2);

			if (IsMouseHovering)
				color = Color.Gold;

			Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, text, pos.X, pos.Y, color, textBorderColor.borderColor, origin, scale);
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