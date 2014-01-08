using System;
using System.Collections.Generic;
using OpenTK;
using PushEngine.Events;

namespace PushEngine.Containers
{
    public delegate void ClientLogicDelegate();
    public delegate void ClientKeyDelegate(KeyEventData ked_);

    public class Container
    {
        public ClientLogicDelegate OnStart = null;
        public ClientKeyDelegate OnKey = null;

        protected List<Container> elements = new List<Container>();
        protected Matrix4d matrix = Matrix4d.Identity;
        protected Renderer renderer { get { return PushEngineCore.Instance.renderer; } }

        public virtual void StartContainer()
        {
            renderer.MultAndPushModelView(ref matrix);
        }

        public virtual void FinishContainer()
        {
            renderer.PopModelView();
        }

        public virtual void Update()
        {
        }

        public virtual void Render()
        {
            StartContainer();
            elements.ForEach(x => x.Render());
            FinishContainer();
        }

        internal virtual void InternalOnStart()
        {
            elements.ForEach(x => x.InternalOnStart());

            if (OnStart != null)
            {
                OnStart();
            }
        }

        internal virtual void InternalOnKey(KeyEventData kev_)
        {
            elements.ForEach(x => x.InternalOnKey(kev_));

            if (OnKey != null)
            {
                OnKey(kev_);
            }
        }
    }
}
