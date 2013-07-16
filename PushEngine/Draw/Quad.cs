using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace PushEngine.Draw
{
    internal class Quad : DrawElement
    {
        public Quad()
        {
        }

        public override void  Create()
        {
            base.Create();

            resetVertex(4);

            if (texture != null)
            {
                Width = texture.TextureSize.Width;
                Height = texture.TextureSize.Height;
            }

            double w2 = Width / 2.0;
            double h2 = Height / 2.0;

            vertex[0] = new Vector2d(w2 * -1, h2);
            vertex[1] = new Vector2d(w2, h2);
            vertex[2] = new Vector2d(w2, h2 * -1);
            vertex[3] = new Vector2d(w2 * -1, h2 * -1);

            textureCoordinates[0] = new Vector2d(0, 1);
            textureCoordinates[1] = new Vector2d(1, 1);
            textureCoordinates[2] = new Vector2d(1, 0);
            textureCoordinates[3] = new Vector2d(0, 0);
        }
    }
}
