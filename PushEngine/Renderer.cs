using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics;

namespace PushEngine
{
    public class Renderer
    {
        public void StartBlending()
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public void EndBlending()
        {
            GL.Disable(EnableCap.Blend);
        }

        public void BindTexture(int id)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, id);
        }

        public void UnbindTexture()
        {
            GL.Disable(EnableCap.Texture2D);
        }

        public void RenderVertexTextureAndColorArray(int nVertex, Vector3d[] v, Vector2d[] t, Color4[] col)
        {
            for (int i = 0; i < nVertex; ++i)
            {
                RenderVertexTextureAndColor(ref v[i], ref t[i], ref col[i]);
            }
        }

        public void RenderPolygon(int nVertex, Vector3d[] v, Vector2d[] t, Color4[] col)
        {
            GL.Begin(BeginMode.Polygon);
            RenderVertexTextureAndColorArray(nVertex, v, t, col);
            GL.End();
        }

        public void RenderQuad(Vector3d[] v, Vector2d[] t, Color4[] col)
        {
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(t[0]);
            GL.Color4(col[0]);
            GL.Vertex3(v[0]);
            GL.TexCoord2(t[1]);
            GL.Color4(col[1]);
            GL.Vertex3(v[1]);
            GL.TexCoord2(t[2]);
            GL.Color4(col[2]);
            GL.Vertex3(v[2]);
            GL.TexCoord2(t[3]);
            GL.Color4(col[3]);
            GL.Vertex3(v[3]);
            GL.End();
        }

        public void RenderVertexTextureAndColor(ref Vector3d v, ref Vector2d t, ref Color4 col)
        {
            GL.TexCoord2(t);
            GL.Color4(col);
            GL.Vertex3(v);
        }

        public void PutMatrix(ref Matrix4d m)
        {
            GL.PushMatrix();
            GL.MultMatrix(ref m);
        }

        public void PopMatrix()
        {
            GL.PopMatrix();
        }
    }
}
