using System;
using OpenTK;
using PushEngine.Demos;
using System.Collections.Generic;
using OpenTK.Input;
using System.Drawing;

namespace PushEngine
{
    internal class ClientManager : IDisposable
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
            Debug.Log("Created client with name:" + newP.Name());
            Debug.Log("Client " + newP.Name() + " wants view of: " + newP.ClientWindow.ViewRectangle);
            newP.Start();
        }

        internal void OnResize(EventArgs e)
        {
            foreach (Client cl in clients)
            {
                cl.ClientWindow.StartContainer();
            }
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
