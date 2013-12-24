using System;
using OpenTK;
using System.Drawing;

namespace PushEngine
{
    public class View : Object
    {
        protected Vector2d[] view;

        public View()
        {
            view = new Vector2d[2];
            view[0] = new Vector2d(-1, -1);
            view[1] = new Vector2d(1, 1);
        }

        public View(Vector2d topLeft, Vector2d downRight) : this()
        {
            view[0] = topLeft;
            view[1] = downRight;
        }

        public View(double top, double down, double left, double right) : this()
        {
            view[0].X = left;
            view[0].Y = top;
            view[1].X = right;
            view[1].Y = down;
        }

        public View(RectangleF rect) : this()
        {
            this.ViewRectangle = rect;
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
