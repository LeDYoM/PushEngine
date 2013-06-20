using System;

namespace PushEngine
{
    public class Manager
    {
        public enum State
        {
            NotInit,
            InitSuccess,
            InitError,
            Stopped
        }

        protected State state = State.NotInit;
        protected int error = 0;

        internal virtual void Start()
        {
        }

        internal void Success()
        {
            state = State.InitSuccess;
        }

        internal void Error(int error_)
        {
            state = State.InitError;
            error = error_;
        }

        internal virtual void Stop()
        {
            state = State.Stopped;
        }

    }
}
