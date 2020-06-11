using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	public readonly struct TextBorderHoverColors
	{
		public static readonly TextBorderHoverColors WhiteBlackYellow = new TextBorderHoverColors(Color.White, Color.Black, Color.Goldenrod); //or Color.Gold

		public readonly Color textColor;
		public readonly Color borderColor;
		public readonly Color hoverColor;

		public TextBorderHoverColors(Color textColor, Color borderColor, Color hoverColor)
		{
			this.textColor = textColor;
			this.borderColor = borderColor;
			this.hoverColor = hoverColor;
		}
	}
}