﻿using System;
using OpenTK;
using PushEngine.Demos;
using System.Collections.Generic;
using OpenTK.Input;

namespace PushEngine
{
    internal class ProcessManager : IDisposable
    {
        private DebugHelper dh = Debugger.getDH("ProcessManager");

        private List<Client> clients = new List<Client>();
        internal DebugVars dVars = new DebugVars();

        internal void Start()
        {
            clients.Add(new Blocker());
            clients.Add(dVars);
            //clients.Add(new DirectDemoQuad());

            foreach (Client client in clients)
            {
                client.setContext(new Context(client));
            }

        }

        internal void startCreatedProcesses()
        {
            foreach (Client client in clients)
            {
                if (client.Context.state == Context.State.Created)
                {
                    PushEngineCore.Instance.eManager.AddEvent(PEEvent.StartEventForClient(client));
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

        internal void EventForProcesses(PEEvent event_)
        {
            foreach (Client client in clients)
            {
                client.ReceiveEvent(event_);
            }
        }

        internal void EventForProcess(PEEvent event_)
        {
            Client cl = event_.receiverClient;
            dh.Assert(cl != null);
            if (cl != null)
            {
                foreach (Client client in clients)
                {
                    if (client == cl)
                        client.ReceiveEvent(event_);
                }
            }
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
