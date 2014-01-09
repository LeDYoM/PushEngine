using System;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
    public class View
    {
        public Size pixelSize;
		public Vector2d TopLeft;
		public Vector2d DownRight;

        public View(Vector2d topLeft, Vector2d downRight)
        {
			TopLeft = topLeft;
			DownRight = downRight;

            pixelSize = new Size(800, 600);
        }

        public View()
            : this(new Vector2d(-400, 300), new Vector2d(400, -300))
        {
            pixelSize = new Size(800, 600);
        }

        public Matrix4d updateMatrixFromView()
        {
			return Matrix4d.CreateOrthographicOffCenter(TopLeft.X, DownRight.X, DownRight.Y, TopLeft.Y, -1.0, 1.0);
        }

        public RectangleF ViewRectangle
        {
			get { return new RectangleF((float)TopLeft.X, (float)TopLeft.Y, (float)(DownRight.X - TopLeft.X), (float)(DownRight.Y - TopLeft.Y)); }
			set { TopLeft = new Vector2d(value.Left, value.Top); DownRight = new Vector2d(value.Right, value.Bottom); }
        }

        public override string ToString()
        {
            return ViewRectangle.ToString();
        }

    }
}
