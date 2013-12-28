using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PushEngine.Draw
{
    public class Scene : IDisposable
    {
        private List<SceneElement> sceneElements = new List<SceneElement>();
        private Renderer renderer { get { return PushEngineCore.Instance.renderer; } }

        internal Scene()
        {
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
            sceneElements.ForEach(x => x.Render());
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
