using System;
using System.Collections.Generic;

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
                switch (temp.eScope)
                {
                    case PEEvent.EventScope.SystemOnly:
                        break;
                    case PEEvent.EventScope.All:
                        PushEngineCore.Instance.processManager.EventForProcesses(temp);
                        break;
                    case PEEvent.EventScope.ProcessesOnly:
                        PushEngineCore.Instance.processManager.EventForProcesses(temp);
                        break;
                    case PEEvent.EventScope.ProcessInternal:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
