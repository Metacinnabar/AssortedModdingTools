using System.Collections.Generic;
using System.Linq;

namespace AssortedModdingTools.DataStructures
{
	public class FrameCounter
	{
		public long TotalFrames { get; private set; }
		public float TotalSeconds { get; private set; }
		public float AverageFramesPerSecond { get; private set; }
		public float CurrentFramesPerSecond { get; private set; }

		public static readonly int maximumSamples = 100;

		private readonly Queue<float> sampleBuffer = new Queue<float>();

		public void Update(float deltaTime)
		{
			CurrentFramesPerSecond = 1f / deltaTime;

			sampleBuffer.Enqueue(CurrentFramesPerSecond);

			if (sampleBuffer.Count > maximumSamples)
			{
				sampleBuffer.Dequeue();
				AverageFramesPerSecond = sampleBuffer.Average();
			}
			else
				AverageFramesPerSecond = CurrentFramesPerSecond;

			TotalFrames++;
			TotalSeconds += deltaTime;
		}
	}
}