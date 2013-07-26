using System;
using System.Drawing;
using PushEngine.Draw;
using PushEngine.Input;

namespace PushEngine
{
    public class Context : IDisposable
    {
        public enum State
        {
            Created = 0,
            Running
        }

        public State state = State.Created;
        public FrameData frameData = new FrameData();
        public Rectangle viewPort;
        public DebugVars dVars;
        public SceneDirector sceneDirector = new SceneDirector();
        public Keyboard keyboard;


        public Context()
        {
            keyboard = PushEngineCore.Instance.keyboard;
            viewPort = PushEngineCore.Instance.SystemProjection.View;
            dVars = PushEngineCore.Instance.processManager.dVars;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            sceneDirector.Dispose();
            sceneDirector = null;
        }
    }
}
