using System;
using System.Collections.Generic;
using OpenTK;
using PushEngine.Events;

namespace PushEngine.Containers
{
	public class Container : ClientLogicImplementer
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
			elements.ForEach(x => x.Update());
		}

        public virtual void Render()
        {
            StartContainer();
            elements.ForEach(x => x.Render());
            FinishContainer();
        }

		internal override void InternalOnStart()
        {
			base.InternalOnStart ();
            elements.ForEach(x => x.InternalOnStart());
        }

		internal override void InternalOnKey(KeyEventData kev_)
        {
			base.InternalOnKey (kev_);
            elements.ForEach(x => x.InternalOnKey(kev_));
        }
    }
}
