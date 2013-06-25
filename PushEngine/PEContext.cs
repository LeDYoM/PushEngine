using System;
using OpenTK.Input;

namespace PushEngine
{
    public class PEContext
    {
        public enum State
        {
            Created = 0,
            Running
        }

        public KeyboardDevice Keyboard;
        public State state;

    }
}
