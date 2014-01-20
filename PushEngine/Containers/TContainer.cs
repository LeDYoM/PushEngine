using System;
using System.Collections.Generic;
using OpenTK;
using PushEngine.Events;

namespace PushEngine.Containers
{
	public class TContainer<T> : LeafContainer where T : IContainer
    {
        protected List<T> elements = new List<T>();

		public override void StartContainer()
        {
            renderer.MultAndPushModelView(ref matrix);
        }

		public override void FinishContainer()
        {
            renderer.PopModelView();
        }

		public override void Update()
        {
            elements.ForEach(x => x.Update());
        }

		public override void Render()
        {
            StartContainer();
            elements.ForEach(x => x.Render());
            FinishContainer();
        }

		public override void OnStart()
        {
            elements.ForEach(x => x.OnStart());
        }

		public override void OnKey(KeyEventData kev_)
        {
            elements.ForEach(x => x.OnKey(kev_));
        }
    }
}
