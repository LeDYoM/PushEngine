using System;
using PushEngine.Containers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace PushEngine.Draw.Components
{
	public class DynamicImageRenderer : BaseImageRenderer
	{
		private int maxImages = 0;
		public DynamicImageRenderer()
		{
		}

		public void Configure(Vector2d totalSize_, Vector2d defaultFormSize_, int maxImages_)
		{
            PEDebug.Assert(defaultFormSize_.X > 0.0000f && defaultFormSize_.Y > 0.0000f, "formSize_ has to be > 0 in both coordinates");
			PEDebug.Assert(totalSize_.X > 0.0000f && totalSize_.Y > 0.0000f, "totalSize_ has to be > 0 in both coordinates");

			maxImages = maxImages_;
			numPolygons = 0;
			numPoints = maxImages * VertexPerForm;
			vertex = new Vector3d[numPoints];
			color = new Color4[numPoints];
			uv = new Vector2d[numPoints];

            defaultFormSize = defaultFormSize_;
			totalSize = totalSize_;
			TopLeft.Y = 0 + (totalSize.Y * 0.5);
			TopLeft.X = 0 - (totalSize.X * 0.5);
        }

		public void AddImageInMatrix(int x, int y)
		{
			if (numPolygons < maxImages)
			{
				numPolygons++;
				AddImageInXY (x, y);
			}
		}

		public void AddNewImage(Vector2d? size_)
		{
			if (numPolygons < maxImages)
			{
				numPolygons++;
				AddImage (size_);
			}
		}
	}
}
