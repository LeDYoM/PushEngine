using System;
using OpenTK;
using PushEngine.Demos;

namespace PushEngine
{
    internal class ProcessManager : Manager, IDisposable
    {
        DirectDemoQuad directDemoQuad;

        internal override void Start()
        {
            base.Start();

            directDemoQuad = new DirectDemoQuad();
            PEContext context = new PEContext();

            context.Keyboard = PushEngineCore.Instance.Keyboard;
            context.state = PEContext.State.Created;

            directDemoQuad.setContext(context);

            Success();
        }

        internal void startCreatedProcesses()
        {
            if (directDemoQuad.Context.state == PEContext.State.Created)
            {
                directDemoQuad.Start();
            }
        }

        internal void OnUpdateFrame(FrameEventArgs e)
        {
            startCreatedProcesses();

            directDemoQuad.Update(e);
        }

        internal void OnRenderFrame(FrameEventArgs e)
        {
            directDemoQuad.Render(e);
        }


        public void Dispose()
        {
            directDemoQuad.Dispose();
        }
    }
}
