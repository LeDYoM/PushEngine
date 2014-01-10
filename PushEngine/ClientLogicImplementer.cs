using System;
using PushEngine.Events;

namespace PushEngine.Containers
{
	public delegate void ClientLogicDelegate();
	public delegate void ClientKeyDelegate(KeyEventData ked_);

    public class ClientLogicImplementer
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
