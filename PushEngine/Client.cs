using System;
using OpenTK;
using OpenTK.Input;
using System.Diagnostics;
using PushEngine.Draw;

namespace PushEngine
{
    public class Client : IDisposable
    {
        private DebugHelper dh = Debugger.getDH("PEClient");

        protected Context context = null;
        internal Context Context { get { return context; } }

        internal void setContext(Context context_)
        {
            context = context_;
        }

        public virtual ClientData Data()
        {
            return new ClientData();
        }

        public Client()
        {
        }

        public virtual void Start()
        {
            context.state = Context.State.Running;
        }

        public virtual void Update()
        {
            context.sceneDirector.CurrentScene.Update(context);
        }

        public virtual void Render()
        {
            context.sceneDirector.Render(context);
        }

        public void SendEvent(PEEvent event_)
        {
            PushEngineCore.Instance.eManager.AddEvent(event_);
        }

        public void ReceiveEvent(PEEvent event_)
        {
            context.sceneDirector.ReceiveEvent(event_);
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            dh.Assert(context != null);
            context.Dispose();
            context = null;

        }

        ~Client()
        {
            dh.WriteLine("Destructor from PEClient called!");
            Dispose();
        }

    }
}
