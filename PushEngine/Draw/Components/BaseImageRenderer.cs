using System;
using PushEngine.Containers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Draw.Components
{
	public class BaseImageRenderer : LeafContainer
	{
		protected Vector3d[] vertex = null;
		protected Color4[] color = null;
		protected Vector2d[] uv = null;
		protected Vector2d defaultFormSize;
		protected Vector2d totalSize;
		protected Vector2d TopLeft;
		protected int numPoints = 0;
		protected int numPolygons = 0;
		protected int matrixSizeX = 0;
		protected int matrixSizeY = 0;

		protected const int VertexPerForm = 4;

		public BaseImageRenderer()
		{
		}

		protected Vector2d defaultUVFor(int numVertexInPolygon)
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

		protected Vector3d defaultPositionFor(int vertexIndex, int x, int y, Vector2d? size)
		{
			Vector2d temp = size ?? defaultFormSize;

			switch (vertexIndex % VertexPerForm)
			{
			case 0:
				return new Vector3d(TopLeft.X + (x * temp.X), TopLeft.Y - (y * temp.Y), 0);
			case 1:
				return new Vector3d(TopLeft.X + ((x + 1) * temp.X), TopLeft.Y - (y * temp.Y), 0);
			case 2:
				return new Vector3d(TopLeft.X + ((x + 1) * temp.X), TopLeft.Y - ((y + 1) * temp.Y), 0);
			case 3:
				return new Vector3d(TopLeft.X + (x * temp.X), TopLeft.Y - ((y + 1) * temp.Y), 0);
			default:
				return new Vector3d(0, 0, 0);
			}
		}

		protected Vector3d defaultPositionFor(int vertexIndex, Vector2d? size)
		{
			Vector2d temp = size ?? defaultFormSize;
			Vector2d halfSize = temp / 2.0;

			switch (vertexIndex % VertexPerForm)
			{
			case 0:
				return new Vector3d(halfSize.X * -1, halfSize.Y, 0);
			case 1:
				return new Vector3d(halfSize.X,  halfSize.Y, 0);
			case 2:
				return new Vector3d(halfSize.X,  halfSize.Y * -1, 0);
			case 3:
				return new Vector3d(halfSize.X * -1, halfSize.Y * -1, 0);
			default:
				return new Vector3d(0, 0, 0);
			}
		}

		public override void Render()
		{
			RenderPolygons(vertex, uv, color);
		}

		protected int indexFor(int y)
		{
			return (y * matrixSizeX * VertexPerForm);
		}

		protected int indexFor(int x, int y)
		{
			return indexFor(y) + (x * VertexPerForm);
		}

		protected int indexForPolygon(int y)
		{
			return (y * matrixSizeX);
		}

		protected int indexForPolygon(int x, int y)
		{
			return indexForPolygon(y) + x;
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

		protected void AddImageInXY(int x, int y, Vector2d? size_ = null)
		{
			int count = indexFor (x, y);
			for (int i = 0; i < VertexPerForm; ++i)
			{
				setVertex(count, defaultPositionFor (i, x, y, size_));
				setColor(count, Color4.White);
				setCoord(count, defaultUVFor(count));
				count++;

				PEDebug.Log ("Vertex " + count + ": " + defaultPositionFor (i, x, y, size_));
			}
		}

		protected void AddImage(Vector2d? size_)
		{
			int count = numPolygons * VertexPerForm;

			for (int i = 0; i < VertexPerForm; ++i)
			{
				setVertex(count, defaultPositionFor (i, size_));
				setColor(count, Color4.White);
				setCoord(count, defaultUVFor(count));
				count++;

				PEDebug.Log ("Vertex " + count + ": " + defaultPositionFor (i, size_));
			}
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

