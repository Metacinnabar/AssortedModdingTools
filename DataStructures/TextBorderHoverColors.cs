using Microsoft.Xna.Framework;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	public readonly struct TextBorderHoverColors
	{
		private static readonly byte grayByte = (byte)((255 + Main.tileColor.R * 2) / 3); //new Color(grayByte, grayByte, grayByte, 255)
		public static readonly TextBorderHoverColors GrayBlackYellow = new TextBorderHoverColors(new Color(grayByte, grayByte, grayByte, 255), Color.Black, Color.Goldenrod); //or Color.Gold

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