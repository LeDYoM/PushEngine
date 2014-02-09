using System;

namespace PushEngine
{
    public struct TimerEventArgs : EventArgs
    {

    }

	public class Timer
	{
		public double Interval = 0;
		public double Ellapsed { get; private set; }
		public bool IsActive { get; private set; }

        public EventHandler<TimerEventArgs> OnInterval = null;

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
				if (Ellapsed > Interval) {
					IsActive = false;
				}
			}
		}
	}
}

