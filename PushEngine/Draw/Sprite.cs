using System;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK;

namespace PushEngine.Draw
{
    internal class Sprite : SceneElement
    {
        public Sprite()
        {
        }

        internal void CreateSprite(SizeF s, Color4? color)
        {
            Create(4);

            material = new Material(4, null, color);
            size_ = s;
            PostCreate();

        }

        internal void CreateSprite(Texture t, Color4? color)
        {
            Create(4);

            material = new Material(4, t, color);
            size_ = t.TextureSize;
            PostCreate();
        }

        protected override void SizeChanged()
        {
            base.SizeChanged();
            double w2 = Width / 2.0;
            double h2 = Height / 2.0;

            vertex[0] = new Vector2d(w2 * -1, h2);
            vertex[1] = new Vector2d(w2, h2);
            vertex[2] = new Vector2d(w2, h2 * -1);
            vertex[3] = new Vector2d(w2 * -1, h2 * -1);

        }

        protected void PostCreate()
        {
            SizeChanged();
        }

        internal override void PreRender()
        {
            material.startMaterialRenderer();
            renderer.PutMatrix(ref matrix);

        }

        internal override void RenderObject()
        {
            renderer.RenderPolygon(numVertex, vertex, material.textureCoordinates, material.color);
        }

        internal override void PostRender()
        {
            renderer.PopMatrix();
            material.PostRender();
        }


    }
}
