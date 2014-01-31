using System;
using PushEngine.Containers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Draw.Components
{
	public class DynamicImageRenderer
	{
		protected Vector3d[] vertex = null;
		protected Color4[] color = null;
		protected Vector2d[] uv = null;
		protected Vector2d formSize;
		protected Vector2d totalSize;
		protected Vector2d TopLeft;
		protected bool[] visible;
		protected int numPoints = 0;
		protected int numPolygons = 0;
		protected int matrixSizeX = 0;
		protected int matrixSizeY = 0;

		protected const int VertexPerForm = 4;

		public DynamicImageRenderer()
		{
		}

		public void Configure(Vector2d formSize_, Vector2d totalSize_, int reserveElements = 10)
		{
			PEDebug.Assert(size.X > 0.0000f && size.Y > 0.0000f, "Size has to be > 0 in both coordinates");
			PEDebug.Assert(reserveElements > 0, "You cannot start reserving 0 elements");

			numPolygons = reserveElements;
			numPoints = numPolygons * VertexPerForm;
			vertex = new Vector3d[numPoints];
			color = new Color4[numPoints];
			uv = new Vector2d[numPoints];
			visible = new bool[numPolygons];

			formSize = formSize_;
			totalSize = totalSize_;
			TopLeft.Y = 0 + (totalSize.Y * 0.5);
			TopLeft.X = 0 - (totalSize.X * 0.5);

			setDefaults();
		}

	}
}

