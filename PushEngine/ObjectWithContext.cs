using System;
using System.Collections.Generic;


namespace PushEngine
{
    public class ObjectWithContext : Object
    {
        protected Dictionary<string, object> contextProperties = new Dictionary<string, object>();

        public object this[string key]
        {
            get { return contextProperties[key]; }
            set { contextProperties[key] = value; }
        }

        public ObjectWithContext(string id_):base(id_)
        {
        }

        public void setContextProperty(string key, object val)
        {
            this[key] = val;
        }

        public object getContextProperty(string key)
        {
            return this[key];
        }

        public void removeContextProperty(string key)
        {
            contextProperties.Remove(key);
        }

        public object getAndRemoveContextProperty(string key)
        {
            object obj = getContextProperty(key);
            removeContextProperty(key);
            return obj;
        }
    }
}
