using Microsoft.Xna.Framework;
using System;

namespace AssortedModdingTools.DataStructures
{
	public readonly struct TextBorderColors : IEquatable<TextBorderColors>
	{
		public static readonly TextBorderColors WhiteBlack = new TextBorderColors(Color.White, Color.Black);

		public readonly Color textColor;
		public readonly Color borderColor;

		public TextBorderColors(Color textColor, Color borderColor)
		{
			this.textColor = textColor;
			this.borderColor = borderColor;
		}

		public override bool Equals(object obj) => obj is TextBorderColors color && Equals(color);

		public bool Equals(TextBorderColors other) => textColor.Equals(other.textColor) && borderColor.Equals(other.borderColor);

		public override int GetHashCode()
		{
			int hashCode = 1200467471;
			hashCode = hashCode * -1521134295 + textColor.GetHashCode();
			hashCode = hashCode * -1521134295 + borderColor.GetHashCode();
			return hashCode;
		}

		public override string ToString() => $"{textColor}, {borderColor}";

		public static bool operator ==(TextBorderColors left, TextBorderColors right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(TextBorderColors left, TextBorderColors right)
		{
			return !(left == right);
		}
	}
}