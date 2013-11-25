using System;

namespace PushEngine
{
    internal class PECore
    {
        private static bool initialized = false;
        private static PEWindow window = null;
        private static PESystemProperties properties = null;

        internal static void Init()
        {
            if (!initialized)
            {
                properties = new PESystemProperties();
                window = new PEWindow();


            }

        }
    }
}
