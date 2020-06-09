using Microsoft.Xna.Framework;

namespace AssortedModdingTools.DataStructures
{
	public struct ColorBorderBackground
	{
		public static readonly ColorBorderBackground Default = new ColorBorderBackground(null);

		public Color borderColor;
		public Color backgroundColor;

		public ColorBorderBackground(Color? backgroundColor, Color? borderColor = null)
		{
			this.borderColor = borderColor ?? Color.Black;
			this.backgroundColor = backgroundColor ?? new Color(63, 82, 151) * 0.7f;
		}
	}
}