using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AssortedModdingTools.Helpers
{
	public static class ColorHelpers
	{
		public static Color GetHoverGray()
		{
			byte b = (byte)((255 + Main.tileColor.R * 2) / 3); //wat
			Color color = new Color(b, b, b);

			color = new Color((color.R + 35) / 2, (color.G + 35) / 2, (color.B + 35) / 2);

			return color;
		}
	}
}
