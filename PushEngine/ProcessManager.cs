using System;
using OpenTK;
using PushEngine.Demos;
using System.Collections.Generic;

namespace PushEngine
{
    internal class ProcessManager : Manager, IDisposable
    {
        private List<Client> clients = new List<Client>();
        internal DebugVars dVars = new DebugVars();

        internal override void Start()
        {
            base.Start();

            clients.Add(new Blocker());
            clients.Add(dVars);
            //clients.Add(new DirectDemoQuad());

            foreach (Client client in clients)
            {
                client.setContext(new Context());
            }

            Success();
        }

        internal void startCreatedProcesses()
        {
            foreach (Client client in clients)
            {
                if (client.Context.state == Context.State.Created)
                {
                    client.Start();
                }
            }
        }

        internal void OnUpdateFrame(FrameEventArgs e)
        {
            startCreatedProcesses();

            foreach (Client client in clients)
            {
                if (client.Context.state == Context.State.Running)
                {
                    client.Update();
                }
            }
        }

        internal void OnRenderFrame(FrameEventArgs e)
        {
            foreach (Client client in clients)
            {
                if (client.Context.state == Context.State.Running)
                {
                    client.Context.frameData.Apply(e);
                    client.Render();
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            foreach (Client client in clients)
            {
                client.Dispose();
            }

            clients.Clear();
        }
    }
}
