using System;
using System.Collections.Generic;
using System.Diagnostics;
using PushEngine.Containers;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
    public class Scene : IDisposable
    {
        private List<SceneElement> sceneElements = new List<SceneElement>();
        private Renderer renderer { get { return PushEngineCore.Instance.renderer; } }
        public WindowContainer ClientWindow
        {
            internal set;
            get;
        }

        internal Scene()
        {
            ClientWindow = new WindowContainer(new Vector2d(-400, 300), new Vector2d(400, -300), new Size(800, 600));
        }

        internal T GetNewDrawElement<T>() where T : SceneElement, new()
        {
            T obj = new T();
            sceneElements.Add(obj);
            return obj;
        }

        public void Update()
        {
            sceneElements.ForEach(x => x.Update());
        }

        public void Render()
        {
            renderer.ClearScreen();
            renderer.ResetAll();
            ClientWindow.StartContainer();
            sceneElements.ForEach(x => x.Render());
            ClientWindow.FinishContainer();
        }

        internal List<SceneElement> ActiveElements
        {
            get { return sceneElements; } 
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            sceneElements.ForEach(x => x.Dispose());
            sceneElements.Clear();
        }

        ~Scene()
        {
            Dispose();
        }

    }
}
