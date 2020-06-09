using System;

namespace AssortedModdingTools.DataStructures
{
	public struct FloatBounds : IEquatable<FloatBounds>, IBounds<float>
	{
		public float Min { get; private set; }
		public float Max { get; private set; }

		public FloatBounds(float min, float max)
		{
			Min = min;
			Max = max;
		}

		public override bool Equals(object obj) => obj is FloatBounds @float && Equals(@float);

		public bool Equals(FloatBounds other) => Min == other.Min && Max == other.Max;

		public override int GetHashCode()
		{
			int hashCode = -897720056;
			hashCode = hashCode * -1521134295 + Min.GetHashCode();
			hashCode = hashCode * -1521134295 + Max.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(FloatBounds left, FloatBounds right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(FloatBounds left, FloatBounds right)
		{
			return !(left == right);
		}
	}
}