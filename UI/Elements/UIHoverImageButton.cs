using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;

namespace AssortedModdingTools.UI.Elements
{
	public class UIHoverImageButton : UIImageButton
	{
		/// <summary>
		/// Called when the cursor is hovering over the element.
		/// </summary>
		public event Action<SpriteBatch> OnHover;

		public UIHoverImageButton(Texture2D texture) : base(texture)
		{
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			if (IsMouseHovering)
				OnHover?.Invoke(spriteBatch);
		}
	}
}