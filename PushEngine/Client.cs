using System;
using OpenTK;
using OpenTK.Input;
using PushEngine.Draw;
using PushEngine.Containers;
using System.Drawing;
using PushEngine.Events;

namespace PushEngine
{
    public class Client : IDisposable
    {
        internal FrameData frameData = new FrameData();
        internal SceneDirector sceneDirector = new SceneDirector();

        public Client()
        {
        }

        public virtual void Start()
        {
            sceneDirector.InternalOnStart();
        }

        public virtual void OnKey(KeyEventData kev_)
        {
            sceneDirector.InternalOnKey(kev_);
        }

        public virtual string Name()
        {
            return "NoNamed";
        }

        public void Update()
        {
            sceneDirector.Update();
        }

        public void Render()
        {
            sceneDirector.Render();
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~Client()
        {
            Dispose();
        }
    }
}
