using System;
using OpenTK;
using OpenTK.Graphics;

namespace PushEngine.Draw
{
    public class Material : Object, IDisposable
    {
        internal protected Vector2d[] textureCoordinates = null;
        internal protected Color4[] color = null;
        internal Texture texture = null;
        protected bool hasTransparency = false;
        private int numVertex = 0;

        protected Renderer renderer { get { return PushEngineCore.Instance.renderer; } }

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

        public void startMaterialRenderer()
        {
            if (HasTransparency)
            {
                renderer.StartBlending();
            }

            if (texture != null)
            {
                renderer.BindTexture(texture.Id);
            }
        }

        internal void PostRender()
        {
            renderer.UnbindTexture();
            renderer.EndBlending();
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
