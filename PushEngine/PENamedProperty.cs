using System;

namespace PushEngine
{
    public class PENamedProperty
    {
        public string name;
        public IConvertible property;

        public PENamedProperty(string name_, IConvertible property_)
        {
            name = name_;
            property = property_;
        }
    }
}
