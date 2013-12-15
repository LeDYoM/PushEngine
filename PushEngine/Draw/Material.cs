using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Draw
{
    public class Material : Object, IDisposable
    {
        internal protected Vector2d[] textureCoordinates = null;
        internal protected Color4[] color = null;
        internal Texture texture = null;
        protected bool hasTransparency = false;
        private int numVertex = 0;

        internal Material(int numVertex_, Texture t,Color4? color_ = null)
        {
            numVertex = numVertex_;
            texture = t;
            textureCoordinates = new Vector2d[numVertex];
            color = new Color4[numVertex];

            textureCoordinates[0] = new Vector2d(0, 1);
            textureCoordinates[1] = new Vector2d(1, 1);
            textureCoordinates[2] = new Vector2d(1, 0);
            textureCoordinates[3] = new Vector2d(0, 0);

            for (int i = 0; i < color.Length; ++i)
            {
                color[i] = color_.HasValue ? color_.Value : Color4.White;
            }
        }

        public bool HasTransparency { get { return hasTransparency; } }

        internal void setColor(Color4 color_)
        {
            for (int i = 0; i < numVertex; ++i)
            {
                color[i] = color_;
            }
        }

        private int nv = 0;

        public void startMaterialRenderer()
        {
            nv = 0;

            if (HasTransparency)
            {
                PushEngineCore.Instance.renderer.StartBlending();
            }

            if (texture != null)
            {
                PushEngineCore.Instance.renderer.BindTexture(texture.Id);
            }
        }

        public void RenderNextVertex()
        {
            GL.TexCoord2(textureCoordinates[nv]);
            GL.Color4(color[nv]);
        }

        internal void PostRender()
        {
            PushEngineCore.Instance.renderer.UnbindTexture();
            PushEngineCore.Instance.renderer.EndBlending();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (texture != null)
            {
                texture.Dispose();
            }
            texture = null;
        }

        ~Material()
        {
            Dispose();
        }

    }
}
