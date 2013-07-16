using System;
using OpenTK.Input;
using System.Drawing;
using PushEngine.Draw;

namespace PushEngine
{
    public class Context : IDisposable
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
        public SceneDirector sceneDirector = new SceneDirector();

        public Context()
        {
            Keyboard = PushEngineCore.Instance.Keyboard;
            viewPort = PushEngineCore.Instance.SystemProjection.View;
            dVars = PushEngineCore.Instance.processManager.dVars;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            sceneDirector.Dispose();
        }
    }
}
