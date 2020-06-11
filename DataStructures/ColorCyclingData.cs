using Microsoft.Xna.Framework;
using System;

namespace AssortedModdingTools.DataStructures
{
	//todo fix i think
	public struct ColorCyclingData
	{
		public readonly int amountOfColors;
		public Color[] colors;

		public ColorCyclingData(int amountOfColors, Color[] colors)
		{
			if(colors.Length != amountOfColors)
				throw new ArgumentException("'amountOfColors' does not match the length of the 'colors' array");

			this.amountOfColors = amountOfColors;
			this.colors = colors;
		}
	}
}