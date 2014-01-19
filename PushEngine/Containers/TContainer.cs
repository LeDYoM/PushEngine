using System;
using System.Collections.Generic;
using OpenTK;
using PushEngine.Events;

namespace PushEngine.Containers
{
    public class TContainer<T> where T : Container
    {
        protected List<T> elements = new List<T>();
        protected Matrix4d matrix = Matrix4d.Identity;
        protected Renderer renderer { get { return PushEngineCore.Instance.renderer; } }
        public string Name { get; set; }


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
            elements.ForEach(x => x.Update());
        }

        public virtual void Render()
        {
            StartContainer();
            elements.ForEach(x => x.Render());
            FinishContainer();
        }

        internal virtual void OnStart()
        {
            elements.ForEach(x => x.OnStart());
        }

        internal virtual void OnKey(KeyEventData kev_)
        {
            elements.ForEach(x => x.OnKey(kev_));
        }
    }
}
