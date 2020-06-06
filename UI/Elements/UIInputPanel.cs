using Terraria.GameContent.UI.Elements;
using AssortedModdingTools.UI.Elements;
using System;
using Terraria.GameInput;
using Terraria;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssortedModdingTools.UI.Elements
{
	public class UIFocusTextInputPanel : UIPanel
	{
		public string label = string.Empty;

		public UIFocusTextInputPanel(string label, string textInputHint = "Type here")
		{
			this.hintText = textInputHint;
			this.label = label;
			Top.Set(5, 0f);
			Left.Set(10, 0f);
			Width.Set(-20, 1f);
			Height.Set(20, 0);
		}

		public override void OnActivate()
		{
			base.OnActivate();
			var panel = new UIPanel();
			panel.SetPadding(0);
			panel.Width = Width;
			panel.Height = Height;
			Append(panel);

			var uiLabel = new UIText(label)
			{
				Left = { Pixels = 10 },
				Top = { Pixels = 10 }
			};
			panel.Append(uiLabel);

			var textBoxBackground = new UIPanel();
			textBoxBackground.SetPadding(0);
			textBoxBackground.Top.Set(6f, 0f);
			textBoxBackground.Left.Set(-10, .5f);
			textBoxBackground.Width.Set(0, .5f);
			textBoxBackground.Height.Set(30, 0f);
			panel.Append(textBoxBackground);
		}

		public delegate void EventHandler(object sender, EventArgs e);

		public bool focused;

		public string currentText = string.Empty;

		private readonly string hintText;

		private int textBlinkerCount;

		private int textBlinkerState;

		public bool unfocusOnTab = false;

		public event EventHandler OnTextChange;

		public event EventHandler OnUnfocus;

		public event EventHandler OnTab;

		public void SetText(string text)
		{
			if (text == null)
				text = string.Empty;

			if (currentText != text)
			{
				currentText = text;
				OnTextChange?.Invoke(this, new EventArgs());
			}
		}

		public override void Click(UIMouseEvent evt)
		{
			Main.clrInput();
			focused = true;
		}

		public override void Update(GameTime gameTime)
		{
			Vector2 point = new Vector2(Main.mouseX, Main.mouseY);

			if (!ContainsPoint(point) && Main.mouseLeft)
			{
				focused = false;
				OnUnfocus?.Invoke(this, new EventArgs());
			}

			base.Update(gameTime);
		}

		private static bool JustPressed(Keys key)
		{
			if (Main.inputText.IsKeyDown(key))
				return !Main.oldInputText.IsKeyDown(key);

			return false;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			if (focused)
			{
				PlayerInput.WritingText = true;
				Main.instance.HandleIME();
				string inputText = Main.GetInputText(currentText);

				if (!inputText.Equals(currentText))
				{
					currentText = inputText;
					OnTextChange?.Invoke(this, new EventArgs());
				}
				else
					currentText = inputText;

				if (JustPressed(Keys.Tab))
				{
					if (unfocusOnTab)
					{
						focused = false;
						OnUnfocus?.Invoke(this, new EventArgs());
					}

					OnTab?.Invoke(this, new EventArgs());
				}

				if (++textBlinkerCount >= 20)
				{
					textBlinkerState = (textBlinkerState + 1) % 2;
					textBlinkerCount = 0;
				}
			}
			string text = currentText;

			if (textBlinkerState == 1 && focused)
				text += "|";

			CalculatedStyle dimensions = GetDimensions();

			if (currentText.Length == 0 && !focused)
				Utils.DrawBorderString(spriteBatch, hintText, new Vector2(dimensions.X, dimensions.Y), Color.Gray);
			else
				Utils.DrawBorderString(spriteBatch, text, new Vector2(dimensions.X, dimensions.Y), Color.White);
		}
	}
}