using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI.Chat;
using System;

namespace AssortedModdingTools.UI.Elements
{
	//TODO brokenenenenenenew ahhh
	public class UIBoolButton : UIElement
	{
		public bool value = false;
		public Vector2 valueTextScale = Vector2.One;
		public string text = string.Empty;
		public float labelScale = 1f;
		public bool labelLargeText = false; 
		
		private Texture2D toggleTexture;

		public UIBoolButton(string text, float labelScale = 1f, bool labelLarge = false)
		{
			this.text = text;
			this.labelScale = labelScale;
			this.labelLargeText = labelLarge;
		}

		public override void OnInitialize()
		{
			toggleTexture = TextureManager.Load("Images/UI/Settings_Toggle");

			var panel = new UIPanel();
			panel.SetPadding(0);
			panel.Width = Width;
			panel.Height = Height;
			Append(panel);

			var uiLabel = new UIText(text, labelScale, labelLargeText)
			{
				Left = { Pixels = 10 },
				Top = { Pixels = 10 }
			};
			panel.Append(uiLabel);

			OnClick += (ev, v) => value = !value;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = GetDimensions();
			//Color panelColor = IsMouseHovering ? backgroundColor : backgroundColor.MultiplyRGBA(new Color(180, 180, 180));
			//Vector2 position = vector;
			//DrawPanel2(spriteBatch, new Vector2(dimensions.X, dimensions.Y), Main.settingsPanelTexture, 40, dimensions.Height, new Color(180, 180, 180));
			// "Yes" and "No" since no "True" and "False" translation available
			Vector2 position = new Vector2(dimensions.X + dimensions.Width - 80f, dimensions.Y + dimensions.Height / 2f + 3f); //wtf offset
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, value ? "true" : "false", position, Color.White, 0f, Main.fontMouseText.MeasureString(value ? "true" : "false") / 2, valueTextScale);
			//Utils.DrawBorderString(spriteBatch, value ? "true" : "false", new Vector2(dimensions.X + dimensions.Width - 100, dimensions.Height / 2 + Main.fontMouseText.MeasureString(value ? "true" : "false").Y / 2), Color.White);
			Rectangle sourceRectangle = new Rectangle(value ? ((toggleTexture.Width - 2) / 2 + 2) : 0, 0, (toggleTexture.Width - 2) / 2, toggleTexture.Height);
			spriteBatch.Draw(toggleTexture, new Vector2(dimensions.X + dimensions.Width - 10, dimensions.Y + dimensions.Height / 2 + toggleTexture.Width / 2), sourceRectangle, Color.White);
		}
	}
}