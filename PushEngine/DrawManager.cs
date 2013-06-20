using System;
using OpenTK.Graphics.OpenGL;

namespace PushEngine
{
    public class DrawManager : Manager
    {
        public DrawManager()
        {
        }

        internal override void Start()
        {
            base.Start();

            Success();
        }

        internal void ClearBackground()
        {
//            GL.ClearColor(PushEngineCore.Instance.configuration.SystemBackgroundColor);
        }

        internal void Draw()
        {
        }
    }
}
