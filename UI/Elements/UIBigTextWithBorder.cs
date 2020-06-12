using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
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

		public UIBigTextWithBorder(string text, TextBorderColors? textBorderColor = null, Vector2? origin = null, float scale = 0.8f)
		{
			this.text = text;
			this.textBorderColor = textBorderColor ?? TextBorderColors.WhiteBlack;
			Vector2 size = Main.fontDeathText.MeasureString(text);
			this.origin = origin ?? new Vector2(size.X / 2f, 0);
			this.scale = scale;
			Width.Set(size.X, 0f);
			Height.Set(size.Y, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point16 pos = new Point16((short)dimensions.X, (short)dimensions.Y);
			byte b = (byte)((255 + Main.tileColor.R * 2) / 3); //wat
			Color color = new Color(b, b, b);

			color = new Color((color.R + Color.White.R) / 2, (color.G + Color.White.G) / 2, (color.B + Color.White.B) / 2);

			Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, text, pos.X, pos.Y, color, Color.Black, origin, scale - 0.2f);
		}
	}
}