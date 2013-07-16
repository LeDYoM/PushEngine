using System;
using OpenTK;
using OpenTK.Input;
using System.Diagnostics;

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
        }

        public virtual void Render()
        {
            context.sceneDirector.Render();
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
