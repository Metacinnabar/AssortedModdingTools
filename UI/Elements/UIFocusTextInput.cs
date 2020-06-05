using Terraria;
using System;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AssortedModdingTools.UI.Elements
{
	public class UIFocusTextInput : UIElement
	{
		public delegate void EventHandler(object sender, EventArgs e);

		public bool focused;

		public string currentText = string.Empty;

		private readonly string hintText;

		private int textBlinkerCount;

		private int textBlinkerState;

		public bool unfocusOnTab;

		public event EventHandler OnTextChange;

		public event EventHandler OnUnfocus;

		public event EventHandler OnTab;

		public UIFocusTextInput(string hintText)
		{
			this.hintText = hintText;
		}

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