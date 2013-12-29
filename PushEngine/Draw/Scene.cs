using System;
using System.Collections.Generic;
using System.Diagnostics;
using PushEngine.Containers;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
    public class Scene : WindowContainer, IDisposable
    {
        private Renderer renderer { get { return PushEngineCore.Instance.renderer; } }

        internal Scene():base(new Vector2d(-400, 300), new Vector2d(400, -300), new Size(800, 600))
        {
        }

        internal T GetNewDrawElement<T>() where T : SceneElement, new()
        {
            T obj = new T();
            elements.Add(obj);
            return obj;
        }

        public override void Render()
        {
            renderer.ClearScreen();
            renderer.ResetAll();
            base.Render();
        }

        internal List<IUpdateAndRender> ActiveElements
        {
            get { return elements; } 
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~Scene()
        {
            Dispose();
        }

    }
}
