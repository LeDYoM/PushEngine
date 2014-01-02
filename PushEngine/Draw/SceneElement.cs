using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;
using PushEngine.Input;

namespace PushEngine.Draw
{
    public delegate void DrawElementDelegate(SceneElement self);
    public delegate void PEEventReceiver(PEEvent event_);

    public class SceneElement : Object, ISceneElement, IDisposable
    {
        // Delegates
        public PEEventReceiver OnEventReceived = null;

        protected Renderer renderer { get { return PushEngineCore.Instance.renderer; } }
        protected Matrix4d matrix = Matrix4d.Identity;

        internal SceneElement()
        {
        }

        public bool OnEvent(PEEvent event_)
        {
            if (OnEventReceived != null)
            {
                OnEventReceived(event_);
                return true;
            }
            return false;
        }

        public void Update()
        {
        }

        public void Render()
        {
            PreRender();
            RenderObject();
            RenderImpl();
            PostRender();
        }

        internal virtual void PreRender()
        {
        }

        internal virtual void RenderObject()
        {
        }

        internal virtual void RenderImpl()
        {
        }

        internal virtual void PostRender()
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
