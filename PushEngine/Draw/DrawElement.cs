using System;
using OpenTK;
using System.Diagnostics;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace PushEngine.Draw
{
    internal class DrawElement
    {
        internal protected Vector2d[] vertex = null;
        internal protected Color4[] color = null;
        internal protected Vector2d[] textureCoordinates = null;
        internal protected Vector2d position = new Vector2d();
        internal protected bool hasTransparency = false;
        internal protected int numVertex = 0;
        internal protected Color transparentColor = Color.Black;
        internal Texture texture = null;
        private bool initialized = false;

        internal DrawElement()
        {
        }

        protected void resetVertex(int nVertex)
        {
            Debug.Assert(nVertex > 0);
            numVertex = nVertex;
            vertex = new Vector2d[numVertex];
            color = new Color4[numVertex];
            setColor(Color4.White);
            textureCoordinates = new Vector2d[numVertex];
        }

        internal void setColor(Color4 color_)
        {
            for (int i = 0; i < numVertex; ++i)
            {
                color[i] = color_;
            }
        }


        internal virtual void PostInit()
        {
            initialized = true;
        }

        internal void Render()
        {
            Debug.Assert(initialized, "You must call PostInit() after setting the properties");
            PreRender();
            RenderImpl();
            PostRender();
        }

        internal void PreRender()
        {
            if (texture != null)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, texture.Id);
            }

            GL.Translate(position.X, position.Y, 0);

            GL.Begin(BeginMode.Polygon);
            for (int i = 0; i < numVertex; ++i)
            {
                DrawVertex(ref vertex[i], ref textureCoordinates[i], ref color[i]);
            }
            GL.End();
        }

        internal protected void DrawVertex(ref Vector2d v, ref Vector2d t, ref Color4 c)
        {
            GL.TexCoord2(t);
            GL.Color4(c);
            GL.Vertex2(v);
        }

        internal virtual void RenderImpl()
        {
        }

        internal void PostRender()
        {
            if (texture != null)
            {
                GL.Disable(EnableCap.Texture2D);
            }
        }

    }
}
