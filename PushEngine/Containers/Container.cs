using System;
using OpenTK;
using System.Drawing;
using PushEngine.Draw;
using System.Collections.Generic;

namespace PushEngine.Containers
{
    public class Container : IUpdateAndRender
    {
        protected Matrix4d matrix;
        protected Vector2d[] view;
        protected List<IUpdateAndRender> elements = new List<IUpdateAndRender>();

        public Container()
        {
            view = new Vector2d[2];
        }

        public virtual void Apply()
        {
        }

        public RectangleF View
        {
            get { return new RectangleF((float)view[0].X, (float)view[0].Y, (float)(view[1].X - view[0].X), (float)(view[1].Y - view[0].Y)); }
        }

        public Vector2d TopLeft
        {
            get { return view[0]; }
        }

        public Vector2d DownRight
        {
            get { return view[1]; }
        }

        public void Update()
        {
            elements.ForEach(x => x.Update());
        }

        public void Render()
        {
            elements.ForEach(x => x.Render());
        }
    }
}
