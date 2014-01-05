using System;
using PushEngine.Draw;
using OpenTK;

namespace PushEngine.Containers
{
    public class LeafContainer
    {
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
            FinishContainer();
        }
    }
}
