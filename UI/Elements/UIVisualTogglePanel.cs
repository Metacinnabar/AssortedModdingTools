using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.UI;
using Terraria.UI.Chat;

namespace AssortedModdingTools.UI.Elements
{
	public class UIVisualTogglePanel : UITogglePanel
	{
		private Texture2D toggleTexture;

		public override void OnActivate()
		{
			base.OnActivate();
			toggleTexture = TextureManager.Load("Images/UI/Settings_Toggle");
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = GetDimensions();

			Vector2 position = new Vector2(dimensions.X + dimensions.Width - 80f, dimensions.Y + dimensions.Height / 2f + 4); //wtf offset
			string text = Value ? "true" : "false";
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, text, position, Color.White, 0f, Main.fontMouseText.MeasureString(text) / 2, Vector2.One);

			Rectangle sourceRectangle = new Rectangle(Value ? ((toggleTexture.Width - 2) / 2 + 2) : 0, 0, (toggleTexture.Width - 2) / 2, toggleTexture.Height);
			Vector2 pos = new Vector2(dimensions.X + dimensions.Width - 40, dimensions.Y + dimensions.Height / 2 - toggleTexture.Height / 2);
			spriteBatch.Draw(toggleTexture, pos, sourceRectangle, Color.White);
		}
	}
}