using System;

namespace PushEngine
{
    interface IEventReceiver
    {
        void ReceiveEvent(PEEvent event_);
    }
}
