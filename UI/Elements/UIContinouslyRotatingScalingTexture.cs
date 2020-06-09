using AssortedModdingTools.DataStructures;
using AssortedModdingTools.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace AssortedModdingTools.UI
{
	public class UIContinouslyRotatingScalingTexture : UIElement
	{
		public readonly ContinouslyRotatingScalingTexture texture;

		public UIContinouslyRotatingScalingTexture(Texture2D texture)
		{
			this.texture = new ContinouslyRotatingScalingTexture(texture);
		}

		public override void Update(GameTime gameTime)
		{
			texture.Update();
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			texture.Draw(GetDimensions().ToVector2());
		}
	}
}