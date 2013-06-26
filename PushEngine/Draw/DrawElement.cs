﻿using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace PushEngine.Draw
{
    internal class DrawElement : IDisposable
    {
        private DebugHelper dh = Debugger.getDH("DrawElement");

        internal protected Vector2d[] vertex = null;
        internal protected Color4[] color = null;
        internal protected Vector2d[] textureCoordinates = null;
        internal protected Vector2d position = new Vector2d();
        internal protected bool hasTransparency = false;
        internal protected int numVertex = 0;
        internal protected Color4 baseColor = Color4.White;
        internal Texture texture = null;
        protected bool initialized = false;


        internal DrawElement()
        {
        }

        protected void resetVertex(int nVertex)
        {
            dh.Assert(nVertex > 0);
            numVertex = nVertex;
            vertex = new Vector2d[numVertex];
            color = new Color4[numVertex];
            setColor(baseColor);
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
            dh.Assert(initialized);
            PreRender();
            RenderObject();
            RenderImpl();
            PostRender();
        }

        internal void PreRender()
        {
            if (hasTransparency)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            }

            if (texture != null)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, texture.Id);
            }

            GL.PushMatrix();
            GL.Translate(position.X, position.Y, 0);
        }

        internal void RenderObject()
        {
            GL.Begin(BeginMode.Polygon);
            for (int i = 0; i < numVertex; ++i)
            {
                DrawVertex(ref vertex[i], ref textureCoordinates[i], ref color[i]);
            }
            GL.End();
        }

        internal virtual void RenderImpl()
        {
        }

        internal void PostRender()
        {
            GL.PopMatrix();

            if (texture != null)
            {
                GL.Disable(EnableCap.Texture2D);
            }

            if (hasTransparency)
            {
                GL.Disable(EnableCap.Blend);
            }

        }

        internal protected void DrawVertex(ref Vector2d v, ref Vector2d t, ref Color4 c)
        {
            GL.TexCoord2(t);
            GL.Color4(c);
            GL.Vertex2(v);
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

        ~DrawElement()
        {
            dh.WriteLine("Destructor from DrawElement called!");
            Dispose();
        }

    }
}
