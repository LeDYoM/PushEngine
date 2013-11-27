using System;

namespace PushEngine
{
    public class Object : IEventReceiver
    {
        protected string id = "noname";

        public Object(string id_)
        {
            id = id_;
        }

        public Object()
        {
            id = this.GetHashCode().ToString();
        }

        public virtual string DebugDescription()
        {
            return "<" + id + ">";
        }


        public virtual void ReceiveEvent(PEEvent event_)
        {
            throw new NotImplementedException();
        }
    }
}
