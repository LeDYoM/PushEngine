using System;
using System.Collections.Generic;
using OpenTK;
using PushEngine.Events;

namespace PushEngine.Containers
{
    public class Container
    {
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
        }

        internal virtual void InternalOnKey(KeyEventData kev_)
        {
            elements.ForEach(x => x.InternalOnKey(kev_));
        }
    }
}
