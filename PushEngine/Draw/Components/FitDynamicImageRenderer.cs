using System;
using OpenTK;
using OpenTK.Graphics;

namespace PushEngine.Draw.Components
{
    class FitDynamicImageRenderer : DynamicImageRenderer
    {
		protected Vector2d totalSize;
		protected Vector2d TopLeft;

		protected const int VertexPerForm = 4;

        public FitDynamicImageRenderer()
		{
		}

        public override void Configure(Vector2d totalSize_, Vector2d defaultFormSize_, int reserveElements = 10)
		{
            base.Configure(defaultFormSize_, reserveElements);
            PEDebug.Assert(totalSize_.X > 0.0000f && totalSize_.Y > 0.0000f, "totalSize_ has to be > 0 in both coordinates");

            totalSize = totalSize_;
            TopLeft.Y = 0 + (totalSize.Y * 0.5);
            TopLeft.X = 0 - (totalSize.X * 0.5);
        }
    }
}
