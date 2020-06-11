﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UIDragableTexturedPanel : UITexturedPanel
	{
		// Stores the offset from the top left of the UIPanel while dragging.
		private Vector2 Offset { get; set; }
		public bool Dragging { get; private set; }
		public bool StopItemUse { get; set; }

		public UIDragableTexturedPanel(Texture2D bgTexture, Texture2D borderTexture, bool stopItemUse = true) : base(bgTexture, borderTexture)
		{
			StopItemUse = stopItemUse;
		}

		public override void MouseDown(UIMouseEvent evt)
		{
			base.MouseDown(evt);
			DragStart(evt);
		}

		public override void MouseUp(UIMouseEvent evt)
		{
			base.MouseUp(evt);
			DragEnd(evt);
		}

		private void DragStart(UIMouseEvent evt)
		{
			Offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			Dragging = true;
		}

		private void DragEnd(UIMouseEvent evt)
		{
			Vector2 end = evt.MousePosition;
			Dragging = false;

			Left.Set(end.X - Offset.X, 0f);
			Top.Set(end.Y - Offset.Y, 0f);

			Recalculate();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime); // don't remove.

			// clicks on this UIElement dont cause the player to use current items. 
			if (ContainsPoint(Main.MouseScreen) && StopItemUse)
				Main.LocalPlayer.mouseInterface = true;

			if (Dragging)
			{
				Left.Set(Main.mouseX - Offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same.
				Top.Set(Main.mouseY - Offset.Y, 0f);
				Recalculate();
			}

			// Here we check if the UIDragablePanel is outside the Parent UIElement rectangle. 
			// By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
			var parentSpace = Parent.GetDimensions().ToRectangle();

			if (!GetDimensions().ToRectangle().Intersects(parentSpace))
			{
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);

				// Recalculate forces the UI system to do the positioning math again.
				Recalculate();
			}
		}
	}
}