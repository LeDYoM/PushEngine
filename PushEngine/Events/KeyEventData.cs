using System;
using PushEngine.Input;

namespace PushEngine.Events
{
    public class KeyEventData : EventArgs
    {
        public readonly KeyData kData;

        internal KeyEventData(KeyData kData_)
        {
            kData = kData_;
        }
    }
}
