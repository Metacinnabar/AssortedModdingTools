using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UIBigTextWithBorder : UIElement
	{
		public string text = string.Empty;

		public TextBorderColors textBorderColor = TextBorderColors.WhiteBlack;

		public Vector2 origin = Vector2.Zero;

		public float scale = 1f;

		public UIBigTextWithBorder(string text, TextBorderColors? textBorderColor = null, Vector2? origin = null, float scale = 1f)
		{
			this.text = text;
			this.textBorderColor = textBorderColor ?? TextBorderColors.WhiteBlack;
			this.origin = origin ?? Vector2.Zero;
			this.scale = scale;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point16 pos = new Point16((short)dimensions.X, (short)dimensions.Y);

			Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, text, pos.X, pos.Y, textBorderColor.textColor, textBorderColor.borderColor, origin, scale);
		}
	}
}