using System;
using System.Collections.Generic;
using PushEngine.Input;

namespace PushEngine
{
    public class EventManager
    {
        private Queue<PEEvent> events = new Queue<PEEvent>();

        internal void AddEvent(PEEvent event_)
        {
            events.Enqueue(event_);
        }

        internal void ProcessEvents()
        {
            while (events.Count > 0)
            {
                PEEvent temp = events.Dequeue();
                OnEvent(temp);
            }
        }

        internal virtual void OnEvent(PEEvent event_)
        {

        }

    }
}
