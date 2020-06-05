using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UITextVisualTogglePanel : UIVisualTogglePanel
	{
		private object text = string.Empty;
		private float textScale = 1f;
		private Vector2 textSize = Vector2.Zero;
		private bool isLarge;
		private Color color = Color.White;

		public string Text => text.ToString();

		public Color TextColor { get => color; set => color = value; }

		public UITextVisualTogglePanel(string text, float textScale = 1f, bool large = false)
		{
			InternalSetText(text, textScale, large);
		}

		public UITextVisualTogglePanel(LocalizedText text, float textScale = 1f, bool large = false)
		{
			InternalSetText(text, textScale, large);
		}

		public override void Recalculate()
		{
			InternalSetText(text, textScale, isLarge);
			base.Recalculate();
		}

		public void SetText(string text) => InternalSetText(text, textScale, isLarge);

		public void SetText(LocalizedText text) => InternalSetText(text, textScale, isLarge);

		public void SetText(string text, float textScale, bool large) => InternalSetText(text, textScale, large);

		public void SetText(LocalizedText text, float textScale, bool large) => InternalSetText(text, textScale, large);

		private void InternalSetText(object text, float textScale, bool large)
		{
			Vector2 textSize = new Vector2((large ? Main.fontDeathText : Main.fontMouseText).MeasureString(text.ToString()).X, large ? 32f : 16f) * textScale;
			this.text = text;
			this.textScale = textScale;
			this.textSize = textSize;
			isLarge = large;
			MinWidth.Set(textSize.X + PaddingLeft + PaddingRight, 0f);
			MinHeight.Set(textSize.Y + PaddingTop + PaddingBottom, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = GetInnerDimensions();
			CalculatedStyle dimensions = GetDimensions();

			//Vector2 pos = new Vector2(innerDimensions.X - innerDimensions.Width / 2 + 35, innerDimensions.Y - innerDimensions.Height / 2 + 30); //odd offsets
			Vector2 pos = new Vector2(dimensions.X - dimensions.Width / 2 + 35, dimensions.Y + dimensions.Height / 2 - (isLarge ? Main.fontDeathText : Main.fontMouseText).MeasureString(text.ToString()).Y / 2 + 4f); //wtf offset

			if (isLarge)
				pos.Y -= 10f * textScale;
			else
				pos.Y -= 2f * textScale;

			pos.X += (innerDimensions.Width - textSize.X) * 0.5f;

			if (isLarge)
				Utils.DrawBorderStringBig(spriteBatch, Text, pos, color, textScale);
			else
				Utils.DrawBorderString(spriteBatch, Text, pos, color, textScale);
		}
	}
}