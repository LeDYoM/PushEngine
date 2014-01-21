using System;
using OpenTK;

namespace PushEngine
{
	public static class FrameData
    {
		public static double EllapsedSinceStart { get; private set; }
		public static double EllapsedSinceLastFrame { get; private set; }

		static FrameData()
        {
			EllapsedSinceStart = 0;
			EllapsedSinceLastFrame = 0;
        }

		internal static void Apply(FrameEventArgs e)
        {
            EllapsedSinceStart += e.Time;
            EllapsedSinceLastFrame = e.Time;
        }
    }
}
