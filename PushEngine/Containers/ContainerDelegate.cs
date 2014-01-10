using System;
using PushEngine.Events;

namespace PushEngine.Containers
{
	public delegate void ClientLogicDelegate();
	public delegate void ClientKeyDelegate(KeyEventData ked_);

    public class ContainerDelegate : Container
    {
        public ClientLogicDelegate OnStart = null;
        public ClientKeyDelegate OnKey = null;

        internal override void InternalOnStart()
        {
            base.InternalOnStart();

            if (OnStart != null)
            {
                OnStart();
            }
        }

        internal override void InternalOnKey(KeyEventData kev_)
        {
            base.InternalOnKey(kev_);

            if (OnKey != null)
            {
                OnKey(kev_);
            }
        }

    }
}
