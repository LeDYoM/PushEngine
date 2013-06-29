using System;
using OpenTK;
using PushEngine.Demos;
using System.Collections.Generic;

namespace PushEngine
{
    internal class ProcessManager : Manager, IDisposable
    {
        private List<PEClient> clients = new List<PEClient>();
        internal DebugVars dVars = new DebugVars();

        internal override void Start()
        {
            base.Start();

            clients.Add(new Blocker());
            clients.Add(dVars);

            foreach (PEClient client in clients)
            {
                client.setContext(new PEContext());
            }

            Success();
        }

        internal void startCreatedProcesses()
        {
            foreach (PEClient client in clients)
            {
                if (client.Context.state == PEContext.State.Created)
                {
                    client.Start();
                }
            }
        }

        internal void OnUpdateFrame(FrameEventArgs e)
        {
            startCreatedProcesses();

            foreach (PEClient client in clients)
            {
                if (client.Context.state == PEContext.State.Running)
                {
                    client.Update();
                }
            }
        }

        internal void OnRenderFrame(FrameEventArgs e)
        {
            foreach (PEClient client in clients)
            {
                if (client.Context.state == PEContext.State.Running)
                {
                    client.Context.frameData.Apply(e);
                    client.Render();
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            foreach (PEClient client in clients)
            {
                if (client.Context.state == PEContext.State.Created)
                {
                    client.Dispose();
                }
            }

            clients.Clear();
        }
    }
}
