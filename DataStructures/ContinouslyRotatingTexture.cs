using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	public struct ContinouslyRotatingTexture : IEquatable<ContinouslyRotatingTexture>
	{
		public Direction RotationDirection { get; private set; }
		public float Rotation { get; private set; }
		public Texture2D texture;
		public FloatBounds speedBounds;
		public FloatBounds scaleBounds;
		public float speed;

		public ContinouslyRotatingTexture(Texture2D texture, float speed = 3E-05f, FloatBounds? scaleBounds = null, FloatBounds? speedBounds = null)
		{
			this.speed = speed;
			this.texture = texture;
			this.speedBounds = speedBounds ?? new FloatBounds(20f, 20f);
			this.scaleBounds = scaleBounds ?? new FloatBounds(0.1f, 0.1f);
			RotationDirection = Direction.Clockwise;
			Rotation = 0f;
		}

		public void Update()
		{
			Rotation += speed;

			if (Rotation > scaleBounds.Min)
				RotationDirection = Direction.AntiClockwise;
			else if (Rotation < scaleBounds.Max)
				RotationDirection = Direction.Clockwise;

			if (speed < speedBounds.Max && RotationDirection == Direction.Clockwise)
				speed += 1f;
			else if (speed > speedBounds.Min && RotationDirection == Direction.AntiClockwise)
				speed -= 1f;
		}

		public void Draw(Vector2 position, Color? color = null, Rectangle frame = default, float scale = 1f, SpriteEffects? spriteEffects = null, float layerDepth = 0f)
		{
			if (texture == null)
				return;

			Rectangle? sourceRect = frame == default ? null : new Rectangle?(frame);
			SpriteEffects effects = spriteEffects ?? SpriteEffects.None;
			Color nonNullableColor = color ?? Color.White;

			Main.spriteBatch.Draw(texture, position, sourceRect, nonNullableColor, Rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, effects, layerDepth);
		}

		public override bool Equals(object obj) => obj is ContinouslyRotatingTexture texture && Equals(texture);

		public bool Equals(ContinouslyRotatingTexture other) => RotationDirection == other.RotationDirection &&
				   Rotation == other.Rotation &&
				   EqualityComparer<Texture2D>.Default.Equals(texture, other.texture) &&
				   speedBounds.Equals(other.speedBounds) &&
				   scaleBounds.Equals(other.scaleBounds) &&
				   speed == other.speed;

		public override int GetHashCode()
		{
			int hashCode = -444241936;
			hashCode = hashCode * -1521134295 + RotationDirection.GetHashCode();
			hashCode = hashCode * -1521134295 + Rotation.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(texture);
			hashCode = hashCode * -1521134295 + speedBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + scaleBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + speed.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(ContinouslyRotatingTexture left, ContinouslyRotatingTexture right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ContinouslyRotatingTexture left, ContinouslyRotatingTexture right)
		{
			return !(left == right);
		}
	}
}