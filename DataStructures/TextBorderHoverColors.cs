using AssortedModdingTools.Helpers;
using Microsoft.Xna.Framework;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	public readonly struct TextBorderHoverColors
	{
		public static readonly TextBorderHoverColors DefaultHover = new TextBorderHoverColors(ColorHelpers.GetHoverGray(), Color.Black * 0.5f, Color.Gold, Color.Black);

		public readonly Color textColor;
		public readonly Color borderColor;
		public readonly Color textHoverColor;
		public readonly Color borderHoverColor;

		public TextBorderHoverColors(Color textColor, Color borderColor, Color textHoverColor, Color borderHoverColor)
		{
			this.textColor = textColor;
			this.borderColor = borderColor;
			this.textHoverColor = textHoverColor;
			this.borderHoverColor = borderHoverColor;
		}

		public TextBorderColors TextBorderColors => new TextBorderColors(textColor, borderColor);
	}
}