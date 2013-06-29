using System;
using OpenTK.Input;
using System.Drawing;

namespace PushEngine
{
    public class PEContext
    {
        public enum State
        {
            Created = 0,
            Running
        }

        public KeyboardDevice Keyboard;
        public State state = State.Created;
        public FrameData frameData = new FrameData();
        public Rectangle viewPort;
        public DebugVars dVars;

        public PEContext()
        {
            Keyboard = PushEngineCore.Instance.Keyboard;
            viewPort = PushEngineCore.Instance.SystemProjection.View;
            dVars = PushEngineCore.Instance.processManager.dVars;
        }
    }
}
