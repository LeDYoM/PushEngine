using System;
using PushEngine.Containers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Draw.Components
{
	public class ImageRenderer : BaseImageRenderer
    {
        public ImageRenderer()
        {
        }

        public void Configure(Vector2d size, bool perForm)
        {
            Configure(1, 1, size, perForm);
        }

        public void Configure(int numX, Vector2d size, bool perForm)
        {
            Configure(numX, 1, size, perForm);
        }

        public void Configure(int numX, int numY, Vector2d size, bool perForm)
        {
            PEDebug.Assert(numX > 0 && numY > 0, "numX and numY have to be > 0");

            matrixSizeX = numX;
            matrixSizeY = numY;

            numPolygons = numX * numY;
            numPoints = numPolygons * VertexPerForm;
            vertex = new Vector3d[numPoints];
            color = new Color4[numPoints];
            uv = new Vector2d[numPoints];

            defaultFormSize = new Vector2d(perForm ? size.X : (size.X / numX), perForm ? size.Y : (size.Y / numY));
            totalSize = new Vector2d(perForm ? (size.X * numX) : size.X, perForm ? (size.Y / numY) : size.Y);
			TopLeft.Y = 0 + (totalSize.Y * 0.5);
			TopLeft.X = 0 - (totalSize.X * 0.5);

            setDefaults();
        }

		private void setDefaults()
		{
			int count = 0;
			Vector3d temp;

			for (int x = 0; x < matrixSizeX; ++x)
			{
				for (int y = 0; y < matrixSizeY; ++y)
				{
					AddImageInXY (x, y);
				}
			}
		}
    }
}

