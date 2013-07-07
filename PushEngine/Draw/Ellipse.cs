using System;
using OpenTK;
using System.Collections.Generic;

namespace PushEngine.Draw
{
    class Ellipse : DrawElement
    {
        public float width = -1;
        public float height = -1;

        public Ellipse()
        {
        }

        internal void setLeftPosition(double x)
        {
            position.X = x + (width / 2.0);
        }

        internal void setTopPosition(double y)
        {
            position.Y = y + (height / 2.0);
        }

        public override void initObject(PENamedPropertyList prop)
        {
            int nVertex = numVertex;

            resetVertex(nVertex);

            if (texture != null)
            {
                width = texture.TextureSize.Width;
                height = texture.TextureSize.Height;
            }

            double w2 = width / 2.0;
            double h2 = height / 2.0;

            vertex[0] = new Vector2d(w2 * -1, h2);
            vertex[1] = new Vector2d(w2, h2);
            vertex[2] = new Vector2d(w2, h2 * -1);
            vertex[3] = new Vector2d(w2 * -1, h2 * -1);

            textureCoordinates[0] = new Vector2d(0, 1);
            textureCoordinates[1] = new Vector2d(1, 1);
            textureCoordinates[2] = new Vector2d(1, 0);
            textureCoordinates[3] = new Vector2d(0, 0);
            //base.PostInit();
        }

    }
}
