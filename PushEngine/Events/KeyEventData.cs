using System;
using PushEngine.Input;

namespace PushEngine.Events
{
    public class KeyEventData : EventArgs
    {
        public readonly KeyData.KeyState KState;
        public readonly Key EventKey;

        internal KeyEventData(KeyData.KeyState kState_, Key key_)
        {
            KState = kState_;
            EventKey = key_;
        }
    }
}
