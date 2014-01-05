using System;
using PushEngine.Events;

namespace PushEngine.Containers
{
    public class LeafClientContainer : LeafContainer
    {
        public ClientLogicDelegate OnStart = null;
        public ClientKeyDelegate OnKey = null;

        internal virtual void InternalOnStart()
        {
            if (OnStart != null)
            {
                OnStart();
            }
        }

        internal virtual void InternalOnKey(KeyEventData kev_)
        {
            if (OnKey != null)
            {
                OnKey(kev_);
            }
        }
    }
}
