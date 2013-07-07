using System;
using OpenTK.Graphics;

namespace PushEngine
{
    public static class PENamedPropertyListUtils
    {
        public static Color4 getColor4(string baseKey, PENamedPropertyList list)
        {
            Color4 color;

            color.R = list.ByName<float>(baseKey + "_R");
            color.G = list.ByName<float>(baseKey + "_G");
            color.B = list.ByName<float>(baseKey + "_B");
            color.A = list.ByName<float>(baseKey + "_A");

            return color;
        }
    }
}
