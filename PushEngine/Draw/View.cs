using System;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
    public class View
    {
        public Size pixelSize;
        protected Vector2d[] view;

        public View(Vector2d topLeft, Vector2d downRight)
        {
            view = new Vector2d[2];
            view[0] = topLeft;
            view[1] = downRight;

            pixelSize = new Size(800, 600);
        }

        public View()
            : this(new Vector2d(-400, 300), new Vector2d(400, -300))
        {
            pixelSize = new Size(800, 600);
        }

        public Matrix4d updateMatrixFromView()
        {
            return Matrix4d.CreateOrthographicOffCenter(view[0].X, view[1].X, view[1].Y, view[0].Y, -1.0, 1.0);
        }

        public RectangleF ViewRectangle
        {
            get { return new RectangleF((float)view[0].X, (float)view[0].Y, (float)(view[1].X - view[0].X), (float)(view[1].Y - view[0].Y)); }
            set { view[0] = new Vector2d(value.Left, value.Top); view[1] = new Vector2d(value.Right, value.Bottom); }
        }

        public Vector2d TopLeft
        {
            get { return view[0]; }
        }

        public Vector2d DownRight
        {
            get { return view[1]; }
        }

        public override string ToString()
        {
            return ViewRectangle.ToString();
        }

    }
}
