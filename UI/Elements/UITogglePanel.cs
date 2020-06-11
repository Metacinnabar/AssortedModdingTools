using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UITogglePanel : UIToggle
	{
		private static readonly int CornerSize = 12;
		private static readonly int BarSize = 4;

		private Texture2D borderTexture;
		private Texture2D backgroundTexture;

		public Color borderColor = Color.Black;
		public Color backgroundColor = new Color(63, 82, 151) * 0.7f;

		public ColorBorderBackground borderBackgroundColors = ColorBorderBackground.Default;

		public UITogglePanel(ColorBorderBackground? borderBackgroundColors = null)
		{
			this.borderBackgroundColors = borderBackgroundColors ?? ColorBorderBackground.Default;
			SetPadding(CornerSize);
		}

		public override void OnActivate()
		{
			if (borderTexture == null)
				borderTexture = TextureManager.Load("Images/UI/PanelBorder");

			if (backgroundTexture == null)
				backgroundTexture = TextureManager.Load("Images/UI/PanelBackground");
		}

		private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
		{
			CalculatedStyle dimensions = GetDimensions();

			Point point = new Point((int)dimensions.X, (int)dimensions.Y);
			Point point2 = new Point(point.X + (int)dimensions.Width - CornerSize, point.Y + (int)dimensions.Height - CornerSize);

			int width = point2.X - point.X - CornerSize;
			int height = point2.Y - point.Y - CornerSize;

			spriteBatch.Draw(texture, new Rectangle(point.X, point.Y, CornerSize, CornerSize), new Rectangle(0, 0, CornerSize, CornerSize), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y, CornerSize, CornerSize), new Rectangle(CornerSize + BarSize, 0, CornerSize, CornerSize), color);
			spriteBatch.Draw(texture, new Rectangle(point.X, point2.Y, CornerSize, CornerSize), new Rectangle(0, CornerSize + BarSize, CornerSize, CornerSize), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point2.Y, CornerSize, CornerSize), new Rectangle(CornerSize + BarSize, CornerSize + BarSize, CornerSize, CornerSize), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + CornerSize, point.Y, width, CornerSize), new Rectangle(CornerSize, 0, BarSize, CornerSize), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + CornerSize, point2.Y, width, CornerSize), new Rectangle(CornerSize, CornerSize + BarSize, BarSize, CornerSize), color);
			spriteBatch.Draw(texture, new Rectangle(point.X, point.Y + CornerSize, CornerSize, height), new Rectangle(0, CornerSize, CornerSize, BarSize), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y + CornerSize, CornerSize, height), new Rectangle(CornerSize + BarSize, CornerSize, CornerSize, BarSize), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + CornerSize, point.Y + CornerSize, width, height), new Rectangle(CornerSize, CornerSize, BarSize, BarSize), color);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			DrawPanel(spriteBatch, backgroundTexture, backgroundColor);
			DrawPanel(spriteBatch, borderTexture, borderColor);
		}
	}
}