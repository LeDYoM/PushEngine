using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PushEngine.Draw
{
    public class Scene : IDisposable
    {
        private DebugHelper dh = Debugger.getDH("Scene");

        private List<DrawElement> sceneElements = new List<DrawElement>();
        private Context context = null;

        internal Scene(Context context_)
        {
            context = context_;
        }

        internal T GetNewDrawElement<T>() where T : DrawElement, new()
        {
            T obj = new T();
            sceneElements.Add(obj);
            PEEvent evnt = PEEvent.CreatedEventForObject(context.client, obj);
            PushEngineCore.Instance.eManager.AddEvent(evnt);
            return obj;
        }

        public void Update()
        {
            sceneElements.ForEach(x => x.Update(context));
        }

        public void Render()
        {
            sceneElements.ForEach(x => x.Render(context));
        }

        internal List<DrawElement> ActiveElements
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
            dh.WriteLine("Destructor from scene called!");
            Dispose();
        }

    }
}
