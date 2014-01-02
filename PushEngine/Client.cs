using System;
using OpenTK;
using OpenTK.Input;
using PushEngine.Draw;
using PushEngine.Containers;
using System.Drawing;

namespace PushEngine
{
    public class Client : IDisposable
    {
        public delegate void ClientLogicDelegate();

        internal FrameData frameData = new FrameData();
        internal SceneDirector sceneDirector = new SceneDirector();
        public ClientLogicDelegate OnStart = null;

        public Client()
        {
        }

        private void ExecuteIfExists(ClientLogicDelegate d)
        {
            if (d != null)
            {
                d();
            }
        }

        public void Start()
        {
            ExecuteIfExists(OnStart);
        }

        public virtual string Name()
        {
            return "NoNamed";
        }

        public bool OnEvent(PEEvent event_)
        {
            return sceneDirector.OnEvent(event_);
        }

        public void Update()
        {
            sceneDirector.Update();
        }

        public void Render()
        {
            sceneDirector.Render();
        }

        public void SendEvent(PEEvent event_)
        {
            PushEngineCore.Instance.clientManager.AddEvent(event_);
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
