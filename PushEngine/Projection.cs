using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace PushEngine
{
    public class Projection
    {
        internal Vector2[] view = new Vector2[2];

        internal Projection(Vector2 topLeft, Vector2 downRight)
        {
            view[0].X = topLeft.X;
            view[1].X = downRight.X;
            view[0].Y = downRight.Y;
            view[1].Y = topLeft.Y;
        }

        internal RectangleF ViewF
        {
            get
            {
                return new RectangleF(view[0].X, view[1].Y, view[1].X, view[0].Y);
            }
        }

        internal Rectangle View
        {
            get
            {
                return new Rectangle((int)view[0].X, (int)view[1].Y, (int)(view[1].X - view[0].X), (int)(view[0].Y - view[1].Y));
            }
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
