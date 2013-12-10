using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Graphics;

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

            material = new Material(4, null);
            size_ = s;
            GlobalColor = color.HasValue ? color.Value : Color4.White;
            PostCreate();

        }

        internal void CreateSprite(Texture t, Color4? color)
        {
            Create(4);

            material = new Material(4, t);
            size_ = t.TextureSize;
            GlobalColor = color.HasValue ? color.Value : Color4.White;
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

            GL.PushMatrix();
            GL.MultMatrix(ref matrix);
        }

        internal override void RenderObject()
        {
            GL.Begin(BeginMode.Polygon);

            for (int i = 0; i < numVertex; ++i)
            {
                // TODO: Combine material color and GlobalColor
                material.RenderNextVertex();
                GL.Color4(GlobalColor);
                GL.Vertex2(vertex[i]);
            }
            GL.End();
        }

        internal override void PostRender()
        {
            GL.PopMatrix();
            material.PostRender();
        }


    }
}
