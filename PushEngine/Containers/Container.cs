using System;
using OpenTK;
using System.Drawing;
using PushEngine.Draw;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Containers
{
    public class Container : View, IUpdateAndRender
    {
        protected Matrix4d matrix;
        protected List<IUpdateAndRender> elements = new List<IUpdateAndRender>();

        public Container() : base()
        {
        }

        public virtual void StartContainer()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.MultMatrix(ref matrix);
        }

        public virtual void FinishContainer()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();
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
