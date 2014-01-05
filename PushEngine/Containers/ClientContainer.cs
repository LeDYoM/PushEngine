﻿using System;
using PushEngine.Draw;
using PushEngine.Events;

namespace PushEngine.Containers
{
    public class ClientContainer : Container
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
