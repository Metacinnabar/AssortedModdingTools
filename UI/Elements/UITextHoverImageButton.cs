using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace AssortedModdingTools.UI.Elements
{
	public class UITextHoverImageButton : UIImageButton
	{
		public string HoverText { get; private set; }

		/// <summary>
		/// Called before changing text. THe string param is the new text before the change. Return false to stop the text from changing.
		/// </summary>
		public event Func<string, bool> PreTextChange;

		/// <summary>
		/// Called after changing text. The string param is the new text after the change. Only called if PreTextChange returns true
		/// </summary>
		public event Action<string> PostTextChange;

		/// <summary>
		/// Called before drawing text. Params in order is mouse hovering and spritebatch. Return false to stop the text draw.
		/// </summary>
		public event Func<bool, SpriteBatch, bool> PreDrawHoverText;

		/// <summary>
		/// Called after drawing text. Not called if PreDrawHoverText returns false.
		/// </summary>
		public event Action<SpriteBatch> PostDrawHoverText;

		public UITextHoverImageButton(Texture2D texture, string hoverText) : base(texture)
		{
			HoverText = hoverText;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			bool? flag = PreDrawHoverText?.Invoke(IsMouseHovering, spriteBatch);
			if (IsMouseHovering && (flag == true || flag == null))
			{
				Main.hoverItemName = HoverText;
				PostDrawHoverText?.Invoke(spriteBatch);
			}
		}

		public void ChangeHoverText(string newText)
		{
			if (newText != HoverText)
			{
				if (PreTextChange.Invoke(newText))
				{
					HoverText = newText;
					PostTextChange.Invoke(HoverText);
				}
			}
		}
	}
}