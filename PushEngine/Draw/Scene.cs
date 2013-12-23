using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PushEngine.Draw
{
    public class Scene : IDisposable
    {
        private List<SceneElement> sceneElements = new List<SceneElement>();
        private Context context = null;

        internal Scene(Context context_)
        {
            context = context_;
        }

        internal T GetNewDrawElement<T>() where T : SceneElement, new()
        {
            T obj = new T();
            sceneElements.Add(obj);
            PEEvent evnt = PEEvent.CreatedEventForObject(context.client, obj);
            PushEngineCore.Instance.eManager.AddEvent(evnt);
            return obj;
        }

        public void Update()
        {
            sceneElements.ForEach(x => x.Update());
        }

        public void Render()
        {
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
