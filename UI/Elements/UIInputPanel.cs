using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UIFocusTextInputPanel : UIFocusTextInput
	{
		public string label = string.Empty;

		protected UIPanel TextBoxPanel;

		public UIFocusTextInputPanel(string label, string textInputHint = "Type here") : base(textInputHint)
		{
			hintText = textInputHint;
			this.label = label;
		}

		public override void OnActivate()
		{
			base.OnActivate();
			UIPanel panel = new UIPanel();
			panel.SetPadding(0);
			panel.Width = Width;
			panel.Height = Height;
			panel.Append(this);

			UIText uiLabel = new UIText(label)
			{
				Left = { Pixels = 10 },
				Top = { Pixels = 10 }
			};
			panel.Append(uiLabel);

			TextBoxPanel = new UIPanel();
			TextBoxPanel.SetPadding(0);
			TextBoxPanel.Top.Set(6f, 0f);
			TextBoxPanel.Left.Set(-10, .5f);
			TextBoxPanel.Width.Set(0, .5f);
			TextBoxPanel.Height.Set(30, 0f);
			panel.Append(TextBoxPanel);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawChildren(spriteBatch);
			base.DrawSelf(spriteBatch);
		}

		protected override void DrawHint(SpriteBatch spriteBatch, string text)
		{
			if (TextBoxPanel == null)
				return;

			CalculatedStyle dimensions = TextBoxPanel.GetDimensions();
			Vector2 pos = new Vector2(dimensions.X + 10, dimensions.Y + 5);

			if (currentText.Length == 0 && !focused)
				Utils.DrawBorderString(spriteBatch, hintText, pos, Color.Gray);
			else
				Utils.DrawBorderString(spriteBatch, text, pos, Color.White);
		}
	}
}