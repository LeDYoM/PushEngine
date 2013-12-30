using System;

namespace PushEngine
{
    public interface IEventReceiver
    {
        bool OnEvent(PEEvent event_);
    }
}
