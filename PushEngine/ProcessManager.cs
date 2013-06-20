using System;
using OpenTK;
using PushEngine.Demos;

namespace PushEngine
{
    internal class ProcessManager : Manager
    {
        DirectDemoQuad directDemoQuad;

        internal override void Start()
        {
            base.Start();

            directDemoQuad = new DirectDemoQuad();

            Success();
        }

        internal void startCreatedProcesses()
        {
            if (directDemoQuad.state == PEClient.State.Created)
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

    }
}
