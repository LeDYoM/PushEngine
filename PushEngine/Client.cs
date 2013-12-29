using System;
using OpenTK;
using OpenTK.Input;
using PushEngine.Draw;
using PushEngine.Containers;
using System.Drawing;

namespace PushEngine
{
    public delegate void PEEventReceiver(PEEvent event_);

    public class Client : IDisposable
    {
        public PEEventReceiver OnEventReceived = null;
        internal FrameData frameData = new FrameData();
        internal SceneDirector sceneDirector = new SceneDirector();

        public Client()
        {
        }

        public virtual void Start()
        {
        }

        public virtual string Name()
        {
            return "NoNamed";
        }

        public void ReceiveEvent(PEEvent event_)
        {
            if (OnEventReceived != null)
            {
                OnEventReceived(event_);
            }
        }

        public void Update()
        {
            sceneDirector.Update();
        }

        public void Render()
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
