using System;
using OpenTK;

namespace PushEngine
{
    public class FrameData
    {
        double ellapsedSinceStart = 0;
        double ellapsedSinceLastFrame = 0;

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
