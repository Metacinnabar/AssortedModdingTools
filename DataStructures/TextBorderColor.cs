using System;
using Microsoft.Xna.Framework;

namespace AssortedModdingTools.DataStructures
{
	public readonly struct TextBorderColor : IEquatable<TextBorderColor>
	{
		public static readonly TextBorderColor WhiteBlack = new TextBorderColor(Color.White, Color.Black);

		public readonly Color textColor;
		public readonly Color borderColor;

		public TextBorderColor(Color textColor, Color borderColor)
		{
			this.textColor = textColor;
			this.borderColor = borderColor;
		}

		public override bool Equals(object obj) => obj is TextBorderColor color && Equals(color);

		public bool Equals(TextBorderColor other) => textColor.Equals(other.textColor) && borderColor.Equals(other.borderColor);

		public override int GetHashCode()
		{
			var hashCode = 1200467471;
			hashCode = hashCode * -1521134295 + textColor.GetHashCode();
			hashCode = hashCode * -1521134295 + borderColor.GetHashCode();
			return hashCode;
		}

		public override string ToString() => $"{textColor}, {borderColor}";

		public static bool operator ==(TextBorderColor left, TextBorderColor right) => left.Equals(right);

		public static bool operator !=(TextBorderColor left, TextBorderColor right) => !(left == right);
	}
}