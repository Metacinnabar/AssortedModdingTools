using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	public struct ContinouslyScalingTexture : IEquatable<ContinouslyScalingTexture>
	{
		public Direction ScaleDirection { get; private set; }
		public float Scale { get; private set; }
		public Texture2D texture;
		public FloatBounds speedBounds;
		public FloatBounds scaleBounds;
		public float speed;

		public ContinouslyScalingTexture(Texture2D texture, float speed = 1E-05f, FloatBounds? scaleBounds = null, FloatBounds? speedBounds = null)
		{
			this.speed = speed;
			this.texture = texture;
			this.speedBounds = speedBounds ?? new FloatBounds(50f, 50f);
			this.scaleBounds = scaleBounds ?? new FloatBounds(0.9f, 1.1f);
			ScaleDirection = Direction.Right;
			Scale = 1f;
		}

		public void Update()
		{
			Scale += speed;

			if (Scale > scaleBounds.Min)
				ScaleDirection = Direction.Down;
			else if (Scale < scaleBounds.Max)
				ScaleDirection = Direction.Up;

			if (speed < speedBounds.Max && ScaleDirection == Direction.Up)
				speed += 1f;
			else if (speed > speedBounds.Min && ScaleDirection == Direction.Down)
				speed -= 1f;
		}

		public void Draw(Vector2 position, Color? color = null, Rectangle frame = default, float rotation = 0f, SpriteEffects? spriteEffects = null, float layerDepth = 0f)
		{
			if (texture == null)
				return;

			Rectangle? sourceRect = frame == default ? null : new Rectangle?(frame);
			SpriteEffects effects = spriteEffects == null ? SpriteEffects.None : (SpriteEffects)spriteEffects;
			Color nonNullableColor = color == null ? Color.White : (Color)color;

			Main.spriteBatch.Draw(texture, position, sourceRect, nonNullableColor, rotation, new Vector2(texture.Width / 2, texture.Height / 2), Scale, effects, layerDepth);
		}

		public override bool Equals(object obj) => obj is ContinouslyScalingTexture texture && Equals(texture);

		public bool Equals(ContinouslyScalingTexture other) => ScaleDirection == other.ScaleDirection &&
				   Scale == other.Scale &&
				   EqualityComparer<Texture2D>.Default.Equals(texture, other.texture) &&
				   speedBounds.Equals(other.speedBounds) &&
				   scaleBounds.Equals(other.scaleBounds) &&
				   speed == other.speed;

		public override int GetHashCode()
		{
			int hashCode = 95415488;
			hashCode = hashCode * -1521134295 + ScaleDirection.GetHashCode();
			hashCode = hashCode * -1521134295 + Scale.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(texture);
			hashCode = hashCode * -1521134295 + speedBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + scaleBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + speed.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(ContinouslyScalingTexture left, ContinouslyScalingTexture right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ContinouslyScalingTexture left, ContinouslyScalingTexture right)
		{
			return !(left == right);
		}
	}
}