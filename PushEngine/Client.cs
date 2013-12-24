using System;
using OpenTK;
using OpenTK.Input;
using PushEngine.Draw;
using PushEngine.Containers;

namespace PushEngine
{
    public delegate void PEEventReceiver(PEEvent event_);

    public class Client : Object, IUpdateAndRender, IDisposable
    {
        public PEEventReceiver OnEventReceived = null;
        public Container ParentContainer
        {
            get { return PushEngineCore.Instance.mainWindowContainer; }
        }

        public SceneDirector sceneDirector = null;
        public FrameData frameData = new FrameData();

        public virtual ClientData Data()
        {
            return new ClientData();
        }

        public Client()
        {
            sceneDirector = new SceneDirector();
        }

        public virtual void Start()
        {
        }

        public override void ReceiveEvent(PEEvent event_)
        {
            if (OnEventReceived != null)
            {
                OnEventReceived(event_);
            }
        }

        internal SceneDirector Director
        {
            get { return sceneDirector; }
        }

        public virtual void Update()
        {
            sceneDirector.CurrentScene.Update();
        }

        public virtual void Render()
        {
            sceneDirector.Render();
        }

        public void SendEvent(PEEvent event_)
        {
            PushEngineCore.Instance.eManager.AddEvent(event_);
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~Client()
        {
            Dispose();
        }
    }
}
