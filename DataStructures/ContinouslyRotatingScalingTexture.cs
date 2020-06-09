using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace AssortedModdingTools.DataStructures
{
	public struct ContinouslyRotatingScalingTexture : IEquatable<ContinouslyRotatingScalingTexture>
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

		public ContinouslyRotatingScalingTexture(Texture2D texture, float rotationSpeed = 1f, float scaleSpeed = 1f, FloatBounds? scaleSpeedBounds = null, FloatBounds? rotationSpeedBounds = null, FloatBounds? scaleBounds = null, FloatBounds? rotationBounds = null) : this()
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

		public void Draw(Vector2 position, float extraScale = 1f, Color? color = null, Rectangle frame = default, SpriteEffects? spriteEffects = null, float layerDepth = 0)
		{
			if (texture == null)
				return;

			Rectangle? sourceRect = frame == default ? null : new Rectangle?(frame);
			SpriteEffects effects = spriteEffects == null ? SpriteEffects.None : (SpriteEffects)spriteEffects;
			Color nonNullableColor = color == null ? Color.White : (Color)color;

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

		public override bool Equals(object obj) => obj is ContinouslyRotatingScalingTexture texture && Equals(texture);

		public bool Equals(ContinouslyRotatingScalingTexture other) => Rotation == other.Rotation &&
				   RotationDirection == other.RotationDirection &&
				   Scale == other.Scale &&
				   ScaleDirection == other.ScaleDirection &&
				   EqualityComparer<Texture2D>.Default.Equals(texture, other.texture) &&
				   scaleSpeedBounds.Equals(other.scaleSpeedBounds) &&
				   scaleBounds.Equals(other.scaleBounds) &&
				   scaleSpeed == other.scaleSpeed &&
				   rotationSpeedBounds.Equals(other.rotationSpeedBounds) &&
				   rotationBounds.Equals(other.rotationBounds) &&
				   rotationSpeed == other.rotationSpeed;

		public override int GetHashCode()
		{
			int hashCode = -438475418;
			hashCode = hashCode * -1521134295 + Rotation.GetHashCode();
			hashCode = hashCode * -1521134295 + RotationDirection.GetHashCode();
			hashCode = hashCode * -1521134295 + Scale.GetHashCode();
			hashCode = hashCode * -1521134295 + ScaleDirection.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(texture);
			hashCode = hashCode * -1521134295 + scaleSpeedBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + scaleBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + scaleSpeed.GetHashCode();
			hashCode = hashCode * -1521134295 + rotationSpeedBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + rotationBounds.GetHashCode();
			hashCode = hashCode * -1521134295 + rotationSpeed.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(ContinouslyRotatingScalingTexture left, ContinouslyRotatingScalingTexture right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ContinouslyRotatingScalingTexture left, ContinouslyRotatingScalingTexture right)
		{
			return !(left == right);
		}
	}

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
			Color nonNullableColor = color == null ? Color.White : (Color)color;

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

	public struct ColorCyclingData
	{
		public readonly int amountOfColors;
		public Color[] colors;

		public ColorCyclingData(int amountOfColors, Color[] colors)
		{
			if(colors.Length != amountOfColors)
				throw new ArgumentException("'amountOfColors' does not match the length of the 'colors' array");

			this.amountOfColors = amountOfColors;
			this.colors = colors;
		}
	}
}