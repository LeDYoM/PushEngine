using System;
using OpenTK;
using System.Drawing;
using PushEngine.Draw;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Containers
{
    public class Container : View, ISceneElement
    {
        protected Matrix4d matrix = Matrix4d.Identity;
        protected List<ISceneElement> elements = new List<ISceneElement>();
        private Renderer renderer { get { return PushEngineCore.Instance.renderer; } }

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
    }
}
