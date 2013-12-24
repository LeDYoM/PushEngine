using System;
using OpenTK;
using PushEngine.Demos;
using System.Collections.Generic;
using OpenTK.Input;

namespace PushEngine
{
    internal class ProcessManager : IDisposable
    {
        private List<Client> clients = new List<Client>();

        internal List<Client> Clients
        {
            get { return clients; }
        }

        internal void Start()
        {
            StartNewProcess(new Blocker());
//            StartNewProcess(new DirectDemoQuad());
        }

        internal void StartNewProcess(Client newP)
        {
            clients.Add(newP);
            newP.Start();
        }

        internal void OnUpdateFrame(FrameEventArgs e)
        {
            clients.ForEach(x => x.Update());
        }

        internal void OnRenderFrame(FrameEventArgs e)
        {
            clients.ForEach(x => { x.frameData.Apply(e); x.Render(); });
        }

        internal void Stop()
        {
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
