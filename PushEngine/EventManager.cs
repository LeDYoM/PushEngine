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
                if (temp.receiverClient == null)
                {
                    // There is no client, that should be a system event.
                    // Not implemented yet.
                }
                else
                {
                    temp.receiverClient.ReceiveEvent(temp);
                }
            }
        }

        internal static PEEvent KeyPressingEvent(Key k)
        {
            PEEvent temp = new PEEvent();
            temp.key = k;
            temp.Action = "KeyPressing";
            return temp;
        }

        internal static PEEvent KeyReleasedEvent(Key k)
        {
            PEEvent temp = new PEEvent();
            temp.key = k;
            temp.Action = "KeyReleased";
            return temp;
        }

        internal static PEEvent NewEvent(Client receiverClient, Object receiverObject, Client senderClient, Object senderObject)
        {
            PEEvent temp = new PEEvent();
            temp.receiverClient = receiverClient;
            temp.receiverObject = receiverObject;
            temp.senderClient = senderClient;
            temp.senderObject = senderObject;
            return temp;
        }

        internal static PEEvent EventForObject(Client receiverClient, Object receiverObject, Client senderClient, Object senderObject)
        {
            PEEvent temp = NewEvent(receiverClient, receiverObject, senderClient, senderObject);
            return temp;
        }

        internal static PEEvent EventForClient(Client receiverClient, Client senderClient, Object senderObject)
        {
            PEEvent temp = NewEvent(receiverClient, null, senderClient, senderObject);
            return temp;
        }

        internal static PEEvent StartEventForClient(Client receiverClient)
        {
            PEEvent temp = EventForClient(receiverClient, null, null);
            temp.Action = PEEvent.ActionStartProcess;
            return temp;
        }
    }
}
