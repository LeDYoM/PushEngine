using System;

namespace PushEngine
{
    public class Object
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
    }
}
