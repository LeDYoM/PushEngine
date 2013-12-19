using System;
using System.Drawing;
using PushEngine.Draw;
using PushEngine.Input;
using PushEngine.Containers;

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
        public SceneDirector sceneDirector = null;
        public FrameData frameData = new FrameData();
        public Keyboard keyboard;
        public Client client = null;
        public IContainer parentContainer;

        public Context(Client client_)
        {
            client = client_;
            sceneDirector = new SceneDirector(this);
            keyboard = PushEngineCore.Instance.keyboard;
            parentContainer = PushEngineCore.Instance.mainWindowContainer; 
           // viewPort = PushEngineCore.Instance.SystemProjection.View;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            sceneDirector.Dispose();
            sceneDirector = null;
        }
    }
}
