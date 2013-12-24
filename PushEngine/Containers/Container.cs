using System;
using OpenTK;
using System.Drawing;
using PushEngine.Draw;
using System.Collections.Generic;

namespace PushEngine.Containers
{
    public class Container : View, IUpdateAndRender
    {
        protected Matrix4d matrix;
        protected List<IUpdateAndRender> elements = new List<IUpdateAndRender>();

        public Container() : base()
        {
        }

        public virtual void Apply()
        {
        }

        public virtual void Update()
        {
            elements.ForEach(x => x.Update());
        }

        public virtual void Render()
        {
            elements.ForEach(x => x.Render());
        }
    }
}
