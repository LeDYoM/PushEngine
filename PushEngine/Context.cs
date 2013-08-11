using System;
using System.Drawing;
using PushEngine.Draw;
using PushEngine.Input;

namespace PushEngine
{
    public delegate void PEEventReceiver(PEEvent event_);

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
        public SceneDirector sceneDirector = null;
        public Keyboard keyboard;
        public Client client = null;

        public Context(Client client_)
        {
            client = client_;
            sceneDirector = new SceneDirector(this);
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
