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
            PEEvent evnt = new PEEvent(PEEvent.EventScope.Object);
            evnt.receiverObject = obj;
            evnt.receiverClient = context.client;
            evnt.Action = "CreateCompleted";
            PushEngineCore.Instance.eManager.AddEvent(evnt);
            return obj;
        }

        public void Update()
        {
            foreach (DrawElement element in sceneElements)
            {
                element.Update(context);
            }
        }

        public void Render()
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
