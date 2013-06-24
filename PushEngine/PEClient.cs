using System;
using OpenTK;
using OpenTK.Input;

namespace PushEngine
{
    public class PEClient
    {
        protected KeyboardDevice Keyboard = null;

        public enum State
        {
            Created = 0,
            Running
        }

        public State state { get;  protected set; }

        public PEClient()
        {
            state = State.Created;
        }

        public virtual void Start()
        {
            Keyboard = PushEngineCore.Instance.Keyboard;
            state = State.Running;
        }

        public virtual void Update(FrameEventArgs e)
        {
        }

        public virtual void Render(FrameEventArgs e)
        {
        }
    }
}
