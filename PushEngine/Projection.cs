using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace PushEngine
{
    public class Projection
    {
        internal Vector2[] view = new Vector2[2];

        internal Projection(Vector2 center, SizeF size)
        {
            view[0].X = center.X - (size.Width / 2);
            view[1].X = center.X + (size.Width / 2);
            view[0].Y = center.Y - (size.Height / 2);
            view[1].Y = center.X + (size.Height / 2);
        }

        internal void apply()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(view[0].X, view[1].X, view[0].Y, view[1].Y, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);

        }

    }
}
