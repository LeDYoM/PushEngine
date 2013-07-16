using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PushEngine.Draw
{
    public class Scene : IDisposable
    {
        private DebugHelper dh = Debugger.getDH("Scene");

        private List<DrawElement> sceneElements = new List<DrawElement>();

        internal Scene()
        {
        }

        internal T GetNewDrawElement<T>() where T : DrawElement, new()
        {
            T obj = new T();
            sceneElements.Add(obj);
            return obj;
        }

        internal void Render()
        {
            foreach (DrawElement element in sceneElements)
            {
                element.Render();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            foreach (DrawElement element in sceneElements)
            {
                element.Dispose();
            }

            sceneElements.Clear();
        }

        ~Scene()
        {
            dh.WriteLine("Destructor from scene called!");
            Dispose();
        }

    }
}
