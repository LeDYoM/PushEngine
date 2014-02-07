using System;

namespace PushEngine
{
	public delegate void OnIntervalDelegate();

	public class Timer
	{
		public uint Interval = 0;
		public uint Ellapsed { get; private set; }
		public bool IsActive { get; private set; }

		internal Timer ()
		{
			IsActive = false;
		}

		public void Start()
		{
			IsActive = true;
			Ellapsed = 0;
		}

		internal void Update()
		{
			if (IsActive) {
				Ellapsed += FrameData.EllapsedSinceLastFrame;
				if (Ellapsed >= Interval) {
					IsActive = false;
				}
			}
		}
	}
}

