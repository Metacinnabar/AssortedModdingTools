using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Dinosaur.ModUI
{
	public class UITexturedPanel : UIElement
	{
		private readonly static int cornerSize = 12;
		private readonly static int barSize = 4;

		public Texture2D BorderTexture { get; set; }
		public Texture2D BackgroundTexture { get; set; }
		public Color BorderColor { get; set; } = Color.White;
		public Color BackgroundColor { get; set; } = Color.White;

		public UITexturedPanel(Texture2D bgTexture, Texture2D borderTexture)
		{
			BackgroundTexture = bgTexture;
			BorderTexture = borderTexture;

			SetPadding(cornerSize);
		}

		private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
		{
			CalculatedStyle dimensions = GetDimensions();

			Point dimen = new Point((int)dimensions.X, (int)dimensions.Y);
			Point center = new Point(dimen.X + (int)dimensions.Width - cornerSize, dimen.Y + (int)dimensions.Height - cornerSize);

			int width = center.X - dimen.X - cornerSize;
			int height = center.Y - dimen.Y - cornerSize;

			spriteBatch.Draw(texture, new Rectangle(dimen.X, dimen.Y, cornerSize, cornerSize), new Rectangle?(new Rectangle(0, 0, cornerSize, cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(center.X, dimen.Y, cornerSize, cornerSize), new Rectangle?(new Rectangle(cornerSize + barSize, 0, cornerSize, cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(dimen.X, center.Y, cornerSize, cornerSize), new Rectangle?(new Rectangle(0, cornerSize + barSize, cornerSize, cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(center.X, center.Y, cornerSize, cornerSize), new Rectangle?(new Rectangle(cornerSize + barSize, cornerSize + barSize, cornerSize, cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(dimen.X + cornerSize, dimen.Y, width, cornerSize), new Rectangle?(new Rectangle(cornerSize, 0, barSize, cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(dimen.X + cornerSize, center.Y, width, cornerSize), new Rectangle?(new Rectangle(cornerSize, cornerSize + barSize, barSize, cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(dimen.X, dimen.Y + cornerSize, cornerSize, height), new Rectangle?(new Rectangle(0, cornerSize, cornerSize, barSize)), color);
			spriteBatch.Draw(texture, new Rectangle(center.X, dimen.Y + cornerSize, cornerSize, height), new Rectangle?(new Rectangle(cornerSize + barSize, cornerSize, cornerSize, barSize)), color);
			spriteBatch.Draw(texture, new Rectangle(dimen.X + cornerSize, dimen.Y + cornerSize, width, height), new Rectangle?(new Rectangle(cornerSize, cornerSize, barSize, barSize)), color);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			DrawPanel(spriteBatch, BackgroundTexture, BackgroundColor);
			DrawPanel(spriteBatch, BorderTexture, BorderColor);
		}
	}
}