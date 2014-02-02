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
		protected Vector2d defaultFormSize;
		protected int numPoints = 0;
		protected int numPolygons = 0;

		protected const int VertexPerForm = 4;

		public DynamicImageRenderer()
		{
		}

        public virtual void Configure(Vector2d defaultFormSize_, int reserveElements = 10)
		{
            PEDebug.Assert(defaultFormSize_.X > 0.0000f && defaultFormSize_.Y > 0.0000f, "formSize_ has to be > 0 in both coordinates");
			PEDebug.Assert(reserveElements > 0, "You cannot start reserving 0 elements");

			numPolygons = reserveElements;
			numPoints = numPolygons * VertexPerForm;
			vertex = new Vector3d[numPoints];
			color = new Color4[numPoints];
			uv = new Vector2d[numPoints];

            defaultFormSize = defaultFormSize_;
        }
	}
}

