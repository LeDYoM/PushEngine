using System;
using PushEngine.Containers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Draw.Components
{
	public class ImageRenderer : LeafContainer
	{
		protected Material material;

		// Private properties
		protected Vector3d[] vertex = null;
		protected Color4[] color;
		protected int numEntities = 0;
		protected int matrixSizeX = 0;
		protected int matrixSizeY = 0;
		protected int matrixSizeZ = 0;

		private const int VertexPerForm = 4; 

		public ImageRenderer ()
		{
		}

		public void Configure(Vector2d size, bool perForm)
		{
			Configure (1, 1, 1, size, perForm);
		}

		public void Configure(int numX, Vector2d size, bool perForm)
		{
			Configure (numX, 1, 1, size, perForm);
		}

		public void Configure(int numX, int numY, Vector2d size, bool perForm)
		{
			Configure (numX, numY, 1, size, perForm);
		}

		public void Configure(int numX, int numY, int numZ, Vector2d size, bool perForm)
		{
			PEDebug.Assert (numX > 0 && numY > 0, "numX and numY have to be > 0");

			matrixSizeX = numX;
			matrixSizeY = numY;
			matrixSizeZ = numZ;

			numEntities = numX * numY * *numZ * VertexPerForm;
			vertex = new Vector3d[numEntities];
			color = new Color4[numEntities];

			Vector2d formSize = new Vector2d (perForm ? size.X : (size.X / numX), perForm ? size.Y : (size.Y / numY));
		}

		public override void Render ()
		{
		}

		private int indexFor(int z)
		{
			return z * (matrixSizeX * matrixSizeY);
		}

		private int indexFor(int y, int z)
		{
			return indexFor(z) + (y * matrixSizeX);
		}


		private int indexFor(int x, int y, int z)
		{
			return indexFor(y, z) + x;
		}

		public void RenderPolygon(Vector3d[] v, Vector2d[] t, Color4[] col)
		{
			int count = 0;
			GL.Begin (BeginMode.Polygon);
			for (int i = 0; i < VertexPerForm; ++i)
			{
				int index = i;
				GL.TexCoord2(t[index]);
				GL.Color4(vertex[index]);
				GL.Vertex3(v[index]);
			}

			for (int z = 0; z < matrixSizeZ; ++z) 
			{
				for (int y = 0; x < matrixSizeY; ++y) 
				{
					for (int x = 0; x < matrixSizeX; ++x) 
					{

					}
				}
			}

			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(t[0]);
			GL.Color4(col[0]);
			GL.Vertex3(v[0]);
			GL.TexCoord2(t[1]);
			GL.Color4(col[1]);
			GL.Vertex3(v[1]);
			GL.TexCoord2(t[2]);
			GL.Color4(col[2]);
			GL.Vertex3(v[2]);
			GL.TexCoord2(t[3]);
			GL.Color4(col[3]);
			GL.Vertex3(v[3]);
			GL.End();
		}

	}
}

