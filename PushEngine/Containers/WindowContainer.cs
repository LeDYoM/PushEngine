using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Containers
{
    public class WindowContainer : IContainer
    {
        private Vector2d[] view;
        private Matrix4d matrix;

        private WindowContainer()
        {
            view = new Vector2d[2];
        }

        public Vector2d TopLeft { get { return view[0]; } }
        public Vector2d DownRight { get { return view[1]; } }

        internal WindowContainer(Vector2 topLeft, Vector2 downRight)
            : this()
        {
            view[0].X = topLeft.X;
            view[1].X = downRight.X;
            view[0].Y = topLeft.Y;
            view[1].Y = downRight.Y;
            matrix = Matrix4d.CreateOrthographicOffCenter(view[0].X, view[1].X, view[0].Y, view[1].Y, -1.0, 1.0);

        }

        internal WindowContainer(Vector3d center, Vector3d size)
            : this()
        {
            view[0].X = center.X - (size.X / 2.0);
            view[1].X = center.X + (size.X / 2.0);
            view[0].Y = center.Y - (size.X / 2.0);
            view[1].Y = center.X + (size.X / 2.0);

            matrix = Matrix4d.CreateOrthographicOffCenter(view[0].X, view[1].X, view[0].Y, view[1].Y, -1.0, 1.0);

        }

        public void apply()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

    }
}
