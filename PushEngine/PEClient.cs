using System;
using OpenTK;
using OpenTK.Input;
using System.Diagnostics;

namespace PushEngine
{
    public class PEClient : IDisposable
    {
        private DebugHelper dh = Debugger.getDH("PEClient");

        protected PEContext context = null;
        internal PEContext Context { get { return context; } }

        internal void setContext(PEContext context_)
        {
            context = context_;
        }

        public PEClient()
        {
        }

        public virtual void Start()
        {
            context.state = PEContext.State.Running;
        }

        public virtual void Update(FrameEventArgs e)
        {
        }

        public virtual void Render(FrameEventArgs e)
        {
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~PEClient()
        {
            dh.WriteLine("Destructor from PEClient called!");
            Dispose();
        }

    }
}
