using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	//todo fix
	public struct ContinouslyRotatingScalingColorCyclingTexture
	{
		public float Rotation { get; private set; }
		public Direction RotationDirection { get; private set; }
		public float Scale { get; private set; }
		public Direction ScaleDirection { get; private set; }
		public float scaleSpeedBuffer;
		public float rotationSpeedBuffer;
		public Texture2D texture;
		public FloatBounds scaleSpeedBounds;
		public FloatBounds scaleBounds;
		public float scaleSpeed;
		public FloatBounds rotationSpeedBounds;
		public FloatBounds rotationBounds;
		public float rotationSpeed;
		public Color[] colorsToCycle;

		public ContinouslyRotatingScalingColorCyclingTexture(Texture2D texture, float rotationSpeed = 1f, float scaleSpeed = 1f, FloatBounds? scaleSpeedBounds = null, FloatBounds? rotationSpeedBounds = null, FloatBounds? scaleBounds = null, FloatBounds? rotationBounds = null) : this()
		{
			this.texture = texture;
			this.scaleSpeedBounds = scaleSpeedBounds ?? new FloatBounds(-20f, 20f);
			this.scaleBounds = scaleBounds ?? new FloatBounds(0.9f, 1.1f);
			this.scaleSpeed = scaleSpeed;
			this.rotationSpeedBounds = rotationSpeedBounds ?? new FloatBounds(-20f, 20f);
			this.rotationBounds = rotationBounds ?? new FloatBounds(-0.1f, 0.1f);
			this.rotationSpeed = rotationSpeed;
			//Rotation = 0f;
			RotationDirection = Direction.Clockwise;
			Scale = 1f;
			ScaleDirection = Direction.Up;
			scaleSpeedBuffer = 1E-05f;
			rotationSpeedBuffer = 3E-05f;
		}

		public void Draw(Vector2 position, float extraScale = 1f, float? alpha = null, Rectangle frame = default, SpriteEffects? spriteEffects = null, float layerDepth = 0)
		{
			if (texture == null)
				return;

			Rectangle? sourceRect = frame == default ? null : new Rectangle?(frame);
			SpriteEffects effects = spriteEffects == null ? SpriteEffects.None : (SpriteEffects)spriteEffects;
			//Color nonNullableColor = color == null ? Color.White : (Color)color;
			Color nonNullableColor = Color.White; //todo: fix h

			Main.spriteBatch.Draw(texture, position, sourceRect, nonNullableColor, Rotation, new Vector2(texture.Width / 2, texture.Height / 2), Scale * extraScale, effects, layerDepth);
		}

		public void Update()
		{
			Rotation += rotationSpeed * rotationSpeedBuffer;

			if (Rotation > rotationBounds.Max)
				RotationDirection = Direction.AntiClockwise;
			else if (Rotation < rotationBounds.Min)
				RotationDirection = Direction.Clockwise;

			if (rotationSpeed < rotationSpeedBounds.Max && RotationDirection == Direction.Clockwise)
				rotationSpeed += 1f;
			else if (rotationSpeed > rotationSpeedBounds.Min && RotationDirection == Direction.AntiClockwise)
				rotationSpeed -= 1f;

			Scale += scaleSpeed * scaleSpeedBuffer;

			if (Scale > scaleBounds.Max)
				ScaleDirection = Direction.Down;
			else if (Scale < scaleBounds.Min)
				ScaleDirection = Direction.Up;

			if (scaleSpeed < scaleSpeedBounds.Max && ScaleDirection == Direction.Up)
				scaleSpeed += 1f;
			else if (scaleSpeed > scaleSpeedBounds.Min && ScaleDirection == Direction.Down)
				scaleSpeed -= 1f;
		}
	}
}