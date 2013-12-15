using System;
using OpenTK.Graphics.OpenGL;

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
    }
}
