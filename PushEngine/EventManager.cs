using System;
using System.Collections.Generic;
using PushEngine.Input;

namespace PushEngine
{
    public class EventManager
    {
        private Queue<PEEvent> events = new Queue<PEEvent>();

        internal void Start()
        {
        }

        internal void Stop()
        {
        }

        internal void AddEvent(PEEvent event_)
        {
            events.Enqueue(event_);
        }

        internal void ProcessEvents()
        {
            while (events.Count > 0)
            {
                PEEvent temp = events.Dequeue();

                //Apply the logic to decide where the event should be forwarded.
                if (temp.receiverClient != null)
                {
                    if (temp.receiverObject != null)
                    {
                        temp.receiverObject.ReceiveEvent(temp);
                    }
                    else
                    {
                        if (temp.oBroadCast == PEEvent.ObjectBroadcasting.ToActiveObjects)
                        {
                            toAllActiveObjects(temp, temp.receiverClient);
                        }
                        else
                        {
                            temp.receiverClient.ReceiveEvent(temp);
                        }
                    }
                }
                else
                {
                    if (temp.cBroadCast == PEEvent.ClientBroadcasting.ToActiveClients)
                    {
                        PushEngineCore.Instance.clientManager.Clients.ForEach(x => toAllActiveObjects(temp, x));
                    }
                }
            }
        }

        private void toAllActiveObjects(PEEvent event_, Client cl)
        {
//            cl.sceneDirector.CurrentScene.ActiveElements.ForEach(y => y.ReceiveEvent(event_));
        }
    }
}
