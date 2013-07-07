using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PushEngine
{
    public class PENamedPropertyList : List<PENamedProperty>
    {
        public T ByName<T>(string key) where T : IConvertible
        {
            IConvertible p = Find(x => x.name.Equals(key)).property;
            if (p != null)
            {
                if (p.GetType() == typeof(T))
                {
                    return (T)p;
                }
                else
                {
                    return (T)Convert.ChangeType(p, typeof(T));
                }
            }
            return default(T);
        }

        public PENamedProperty PropertyByName(string key)
        {
            return Find(x => x.name.Equals(key));
        }

        public bool ContainsKey(string key)
        {
            return Find(x => x.name.Equals(key)) != null;
        }

        public bool setValue(string key, IConvertible vt)
        {
            PENamedProperty p = PropertyByName(key);
            if (p != null)
            {
                p.property = vt;
                return true;
            }
            return false;
        }
    }
}
