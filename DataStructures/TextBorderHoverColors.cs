using Microsoft.Xna.Framework;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	public readonly struct TextBorderHoverColors
	{
		private static readonly byte grayByte = (byte)((255 + Main.tileColor.R * 2) / 3); //new Color(grayByte, grayByte, grayByte, 255)
		public static readonly TextBorderHoverColors GrayBlackYellow = new TextBorderHoverColors(GetHoverGray(), Color.Black * 0.5f, Color.Gold); //or Color.Gold
		//move out of struct
		public static Color GetHoverGray()
		{
			byte b = (byte)((255 + Main.tileColor.R * 2) / 3); //wat
			Color color = new Color(b, b, b);

			color = new Color((color.R + 35) / 2, (color.G + 35) / 2, (color.B + 35) / 2);

			return color;
		}

		public readonly Color textColor;
		public readonly Color borderColor;
		public readonly Color hoverColor;

		public TextBorderHoverColors(Color textColor, Color borderColor, Color hoverColor)
		{
			this.textColor = textColor;
			this.borderColor = borderColor;
			this.hoverColor = hoverColor;
		}

		public TextBorderColors TextBorderColors => new TextBorderColors(textColor, borderColor);
	}
}