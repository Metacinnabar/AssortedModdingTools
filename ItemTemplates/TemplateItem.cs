using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedModdingTools.ItemTemplates
{
	public abstract class TemplateItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Template Item"); // The text here is the item name
			Tooltip.SetDefault("Template Item Tooltip"); // The text here is the item tooltip
		}

		public override void SetDefaults()
		{
			item.useTime = item.useAnimation = 30; // Swings once every 30 frames
			item.Size = new Vector2(20, 20); // The hitbox for this weapon if it swings
			item.damage = 1; // The amount of damage this item deals to enemies
		}

		public override void AddRecipes()
		{
			// This is the recipe to craft this item
			ModRecipe recipe = new ModRecipe(mod);
			// This adds 5 dirt blocks as an ingredient to the recipe
			recipe.AddIngredient(ItemID.DirtBlock, 5);
			// To add more ingredients, simply call AddIngredient again
			// This sets the resualt or the product of this recipe to this current item
			recipe.SetResult(this);
		}
	}
}