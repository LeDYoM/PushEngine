using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PushEngine.Draw
{
    public class Scene : IUpdateAndRender, IDisposable
    {
        private DebugHelper dh = Debugger.getDH("Scene");

        private List<DrawElement> sceneElements = new List<DrawElement>();

        internal Scene()
        {
        }

        public void ReceiveEvent(PEEvent event_)
        {
            foreach (DrawElement element in sceneElements)
            {
                element.ReceiveEvent(event_);
            }
        }

        internal T GetNewDrawElement<T>() where T : DrawElement, new()
        {
            T obj = new T();
            sceneElements.Add(obj);
            PEEvent evnt = new PEEvent(PEEvent.EventType.ObjectState, PEEvent.EventScope.All);
            evnt.setContextProperty("Object", obj);
            PushEngineCore.Instance.eManager.AddEvent(evnt);
            return obj;
        }

        public void Update(Context context)
        {
            foreach (DrawElement element in sceneElements)
            {
                element.Update(context);
            }
        }

        public void Render(Context context)
        {
            foreach (DrawElement element in sceneElements)
            {
                element.Render(context);
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
