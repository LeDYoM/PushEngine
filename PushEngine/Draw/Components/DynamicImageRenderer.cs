using System;
using PushEngine.Containers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Draw.Components
{
	public class DynamicImageRenderer : ImageRenderer
	{
		protected bool visibleChanged = false;

		public DynamicImageRenderer()
		{
		}

		public void setAllVisible(bool value_)
		{
			for (int i = 0; i < numPolygons; ++i)
			{
				visible = value_;
			}
			visibleChanged = true;
		}

		public void setVisibility(bool value_, int x = 0, int y = 0)
		{
			int temp = indexForPolygon (x, y);
			if (visible [temp] != value_)
			{
				visible [temp] = value_;
				visibleChanged = true;
			}
		}

		private void updateIfNecessary()
		{
		}

		public override void Render()
		{
			RenderPolygons(vertex, uv, color);
		}

		private int indexForPolygon(int y)
		{
			return (y * matrixSizeX);
		}

		private int indexForPolygon(int x, int y)
		{
			return indexForPolygon(y) + x;
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

