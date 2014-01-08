using System;
using PushEngine.Events;

namespace PushEngine.Containers
{
    public class ContainerDelegate : Container
    {
        public ClientLogicDelegate OnStart = null;
        public ClientKeyDelegate OnKey = null;

        internal override void InternalOnStart()
        {
            base.InternalOnStart();
            elements.ForEach(x => x.InternalOnStart());

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
