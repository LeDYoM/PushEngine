using System;
using System.Collections.Generic;
using PushEngine.Events;
using PushEngine.Draw;

namespace PushEngine.Containers
{
    public class ClientContainer : Container, ISceneElement
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
