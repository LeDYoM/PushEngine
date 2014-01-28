using System;
using PushEngine.Containers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Draw.Components
{
    public class ImageRenderer : LeafContainer
    {
        // Private properties
        protected Vector3d[] vertex = null;
        protected Color4[] color = null;
        protected Vector2d[] uv = null;
        protected Vector2d formSize;
        protected Vector2d totalSize;
        protected int numPoints = 0;
        protected int numPolygons = 0;
        protected int matrixSizeX = 0;
        protected int matrixSizeY = 0;

        private const int VertexPerForm = 4;

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

            formSize = new Vector2d(perForm ? size.X : (size.X / numX), perForm ? size.Y : (size.Y / numY));
            totalSize = new Vector2d(perForm ? (size.X * numX) : size.X, perForm ? (size.Y / numY) : size.Y);

            setDefaults();
        }

        private Vector2d defaultUVFor(int numVertexInPolygon)
        {
            switch (numVertexInPolygon % VertexPerForm)
            {
                case 0:
                    return new Vector2d(0, 1);
                case 1:
                    return new Vector2d(1, 1);
                case 2:
                    return new Vector2d(1, 0);
                case 3:
                    return new Vector2d(0, 0);
                default:
                    return new Vector2d(0, 0);
            }
        }

        private void setDefaults()
        {
            int count = 0;

            double top = totalSize.Y * 0.5;
            double bottom = totalSize.Y * -0.5;
            double left = totalSize.X * -0.5;
            double right = totalSize.X * 0.5;

            for (int x = 0; x < matrixSizeX; ++x)
            {
                for (int y = 0; y < matrixSizeY; ++y)
                {
                    for (int i = 0; i < VertexPerForm; ++i)
                    {
                        Vector3d temp;
                        switch (i % VertexPerForm)
                        {
                            case 0:
							temp = new Vector3d(left + (x * formSize.X), top - (y * formSize.Y), 0);
                                break;
                            case 1:
                                temp = new Vector3d(left + ((x + 1) * formSize.X), top - (y * formSize.Y), 0);
                                break;
                            case 2:
                                temp = new Vector3d(left + ((x + 1) * formSize.X), top - ((y + 1) * formSize.Y), 0);
                                break;
                            case 3:
                                temp = new Vector3d(left + (x * formSize.X), top - ((y + 1) * formSize.Y), 0);
                                break;
                            default:
                                temp = new Vector3d(0, 0, 0);
                                break;
                        }
                     
                        setVertex(count, temp);
                        setColor(count, Color4.White);
                        setCoord(count, defaultUVFor(count));
                        count++;

						PEDebug.Log ("Vertex " + count + ": " + temp);
                    }
                }
            }
        }

        public override void Render()
        {
            RenderPolygons(vertex, uv, color);
        }

        private int indexFor(int y)
        {
			return (y * matrixSizeX * VertexPerForm);
        }

        private int indexFor(int x, int y)
        {
			return indexFor(y) + (x * VertexPerForm);
        }

        public void setVertex(int index, Vector3d vertex_)
        {
            vertex[index] = vertex_;
        }

        public void setColor(int index, Color4 color_)
        {
            color[index] = color_;
        }

        public void setCoord(int index, Vector2d coords_)
        {
            uv[index] = coords_;
        }

        public void setFormColor(Color4 color_, int x = 0, int y = 0)
        {
            int baseIndex = indexFor(x, y);

            for (int i = 0; i < VertexPerForm; ++i)
            {
                setColor(baseIndex + i, color_);
            }
        }

        public void RenderPolygons(Vector3d[] vextex, Vector2d[] uv, Color4[] color)
        {
            int count = 0;

			for (int i = 0; i < numPolygons; ++i)
            {
                GL.Begin(BeginMode.Polygon);

                for (int j = 0; j < VertexPerForm; ++j)
                {
                    GL.TexCoord2(uv[count]);
                    GL.Color4(color[count]);
                    GL.Vertex3(vertex[count]);
                    count++;
                }
                GL.End();
            }
        }
    }
}

