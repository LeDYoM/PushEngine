using System;
using OpenTK;

namespace PushEngine
{
    public class FrameData
    {
        public double ellapsedSinceStart = 0;
        public double ellapsedSinceLastFrame = 0;

        internal FrameData()
        {
        }

        internal void Apply(FrameEventArgs e)
        {
            ellapsedSinceStart += e.Time;
            ellapsedSinceLastFrame = e.Time;
        }
    }
}
