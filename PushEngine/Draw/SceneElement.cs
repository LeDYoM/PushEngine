using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;
using PushEngine.Input;
using PushEngine.Containers;

namespace PushEngine.Draw
{
    public class SceneElement : ClientContainer, ISceneElement, IDisposable
    {
        internal SceneElement()
        {
        }

        public override void Update()
        {
        }

        public override void Render()
        {
            RenderObject();
            RenderImpl();
        }

        internal virtual void RenderImpl()
        {
        }

        internal virtual void RenderObject()
        {
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~SceneElement()
        {
            Dispose();
        }
    }
}
