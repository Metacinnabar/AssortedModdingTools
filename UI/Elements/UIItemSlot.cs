
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Dinosaur.ModUI
{
	public class UIItemSlot : UIElement
	{
		public Item item;
		public Func<Item, bool> validItem;
		public Texture2D backgroundTexture;
		public float scale;
		public int context;

		public event Func<Item, bool> PreItemChange;
		public event Action<Item> PostItemChange;

		public event Func<Item, bool> PreDrawItemSlot;
		public event Action<Item> PostDrawItemSlot;

		public event Action OnMouseHover;

		public UIItemSlot(Texture2D bgTexture = null, float scale = 1f, int context = ItemSlot.Context.InventoryItem)
		{
			validItem = null;
			backgroundTexture = bgTexture ?? Main.inventoryBackTexture;
			this.context = context;
			this.scale = scale;
			item = new Item();
			item.SetDefaults(0);

			Width.Set(backgroundTexture.Width * scale, 0f);
			Height.Set(backgroundTexture.Height * scale, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Rectangle rectangle = GetDimensions().ToRectangle();

			if (IsMouseHovering)
				OnMouseHover?.Invoke();

			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;

				if (validItem == null || validItem(Main.mouseItem))
				{
					bool? pre = PreItemChange?.Invoke(item);
					if (pre == null || pre == true)
						ItemSlot.Handle(ref item, context); //Handle handles all the click and hover actions based on the context.

					PostItemChange?.Invoke(item);
				}
			}

			//Draw draws the slot itself and Item
			bool? preDraw = PreDrawItemSlot?.Invoke(item);
			if (preDraw == null || preDraw == true)
				Draw(item, rectangle.TopLeft(), backgroundTexture, scale);

			PostDrawItemSlot?.Invoke(item);
		}

		public void PutItemInInventory(Player player = null)
		{
			if (player != null)
				player.PutItemInInventory(item.type);
			else
				Main.LocalPlayer.PutItemInInventory(item.type);

			item.TurnToAir();
		}

		public void ChangeItem(Item newItem)
		{
			if (newItem == null)
				throw new ArgumentNullException();

			bool? pre = PreItemChange?.Invoke(newItem);
			if (pre == null || pre == true)
				item = newItem;

			PostItemChange?.Invoke(item);
		}

		public void ChangeItem(int type)
		{
			if (type <= 0)
				throw new ArgumentException();

			bool? pre = PreItemChange?.Invoke(item);
			if (pre == null || pre == true)
				item.type = type;

			PostItemChange?.Invoke(item);
		}

		public static void Draw(Item item, Vector2 position, Texture2D bg = null, float scale = 1f)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Player player = Main.player[Main.myPlayer];
			Texture2D background = bg ?? Main.inventoryBackTexture;
			Vector2 bgSize = background.Size() * scale;

			spriteBatch.Draw(background, position, null, Main.inventoryBack, 0f, default, scale, SpriteEffects.None, 0f);

			if (item.type > ItemID.None && item.stack > 0)
			{
				Texture2D itemTexture = Main.itemTexture[item.type];
				Rectangle frame = Main.itemAnimations[item.type] != null ? Main.itemAnimations[item.type].GetFrame(itemTexture) : itemTexture.Frame(1, 1, 0, 0);
				Color itemColor = Color.White;
				float lightScale = 1f;
				float itemScale = 1f;

				ItemSlot.GetItemLight(ref itemColor, ref lightScale, item);

				if (frame.Width > 32 || frame.Height > 32)
				{
					if (frame.Width > frame.Height)
						itemScale = 32f / frame.Width;
					else
						itemScale = 32f / frame.Height;
				}

				itemScale *= scale;

				Vector2 itemPos = position + bgSize / 2f - frame.Size() * itemScale / 2f;
				Vector2 itemOrigin = frame.Size() * (lightScale / 2f - 0.5f);

				if (ItemLoader.PreDrawInInventory(item, spriteBatch, itemPos, frame, item.GetAlpha(itemColor), item.GetColor(Color.White), itemOrigin, itemScale * lightScale))
				{
					spriteBatch.Draw(itemTexture, itemPos, new Rectangle?(frame), item.GetAlpha(itemColor), 0f, itemOrigin, itemScale * lightScale, SpriteEffects.None, 0f);

					if (item.color != Color.Transparent)
						spriteBatch.Draw(itemTexture, itemPos, new Rectangle?(frame), item.GetColor(Color.White), 0f, itemOrigin, itemScale * lightScale, SpriteEffects.None, 0f);
				}

				ItemLoader.PostDrawInInventory(item, spriteBatch, itemPos, frame, item.GetAlpha(itemColor), item.GetColor(Color.White), itemOrigin, itemScale * lightScale);

				if (ItemID.Sets.TrapSigned[item.type])
					spriteBatch.Draw(Main.wireTexture, position + new Vector2(40f, 40f) * scale, new Rectangle?(new Rectangle(4, 58, 8, 8)), Color.White, 0f, new Vector2(4f), 1f, SpriteEffects.None, 0f);

				if (item.stack > 1)
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, item.stack.ToString(), position + new Vector2(10f, 26f) * scale, Color.White, 0f, Vector2.Zero, new Vector2(scale), -1f, scale);

				if (item.potion)
				{
					Vector2 pos = position + background.Size() * scale / 2f - Main.cdTexture.Size() * scale / 2f;
					Color color = item.GetAlpha(Color.White) * (player.potionDelay / (float)player.potionDelayTime);

					spriteBatch.Draw(Main.cdTexture, pos, null, color, 0f, default, itemScale, SpriteEffects.None, 0f);
				}

				if (item.expertOnly && !Main.expertMode)
				{
					Vector2 pos = position + background.Size() * scale / 2f - Main.cdTexture.Size() * scale / 2f;

					spriteBatch.Draw(Main.cdTexture, pos, null, Color.White, 0f, default, itemScale, SpriteEffects.None, 0f);
				}
			}
		}
	}
}