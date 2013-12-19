using System;
using OpenTK;
using OpenTK.Input;
using PushEngine.Draw;
using PushEngine.Containers;

namespace PushEngine
{
    public class Client : Object, IDisposable, IContained
    {
        internal Context Context
        {
            get;
            private set;
        }

        public PEEventReceiver OnEventReceived = null;

        internal void setContext(Context context_)
        {
            Context = context_;
        }

        public virtual ClientData Data()
        {
            return new ClientData();
        }

        public Client()
        {
        }

        public override void ReceiveEvent(PEEvent event_)
        {
            if (OnEventReceived != null)
            {
                OnEventReceived(event_);
            }

            Context.state = Context.State.Running;
        }

        internal SceneDirector Director
        {
            get { return Context.sceneDirector; }
        }

        public virtual void Update()
        {
            Context.sceneDirector.CurrentScene.Update();
        }

        public virtual void Render()
        {
            Context.sceneDirector.Render();
        }

        public void SendEvent(PEEvent event_)
        {
            PushEngineCore.Instance.eManager.AddEvent(event_);
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            Context.Dispose();
            Context = null;

        }

        ~Client()
        {
            Dispose();
        }

        public IContainer ParentContainer
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
