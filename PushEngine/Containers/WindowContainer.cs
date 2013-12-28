using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Containers
{
    public class WindowContainer : Container
    {

        public Size pixelSize;
        Matrix4d pMatrix = Matrix4d.Identity;

        protected WindowContainer()
        {
        }

        protected WindowContainer(Size pixelSize_)
        {
            pixelSize = pixelSize_;
        }

        internal WindowContainer(Vector2d topLeft, Vector2d downRight, Size pixelSize_)
            : this(pixelSize_)
        {
            view[0].X = topLeft.X;
            view[1].X = downRight.X;
            view[0].Y = topLeft.Y;
            view[1].Y = downRight.Y;
            pMatrix = Matrix4d.CreateOrthographicOffCenter(view[0].X, view[1].X, view[1].Y, view[0].Y, -1.0, 1.0);

        }

        internal WindowContainer(Vector3d center, Vector3d size, Size pixelSize_)
            : this(pixelSize_)
        {
            view[0].X = center.X - (size.X / 2.0);
            view[1].X = center.X + (size.X / 2.0);
            view[0].Y = center.Y - (size.X / 2.0);
            view[1].Y = center.X + (size.X / 2.0);

            pMatrix = Matrix4d.CreateOrthographicOffCenter(view[0].X, view[1].X, view[0].Y, view[1].Y, -1.0, 1.0);

        }

        public override void StartContainer()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.MultMatrix(ref pMatrix);
            base.StartContainer();
        }

        public override void FinishContainer()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            base.FinishContainer();
        }
    }
}
