using System;
using OpenTK;
using OpenTK.Input;
using PushEngine.Draw;
using PushEngine.Containers;
using System.Drawing;

namespace PushEngine
{
    public delegate void PEEventReceiver(PEEvent event_);

    public class Client : SceneDirector, IDisposable
    {
        public PEEventReceiver OnEventReceived = null;
        public Container ParentContainer
        {
            get { return PushEngineCore.Instance.mainWindowContainer; }
        }

        internal FrameData frameData = new FrameData();

        protected Client()
        {
        }

        public virtual void Start()
        {
        }

        public virtual string Name()
        {
            return "NoNamed";
        }

        public override void ReceiveEvent(PEEvent event_)
        {
            if (OnEventReceived != null)
            {
                OnEventReceived(event_);
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        public void SendEvent(PEEvent event_)
        {
            PushEngineCore.Instance.eManager.AddEvent(event_);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~Client()
        {
            Dispose();
        }
    }
}
