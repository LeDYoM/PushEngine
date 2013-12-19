using System;
using System.Diagnostics;

namespace PushEngine
{
    public static class Debug
    {
        public static void Log(object o)
        {
            System.Diagnostics.Debug.WriteLine(extraInfo() + o.ToString());
        }

        private static string extraInfo()
        {
            StackFrame stackFrame = new StackFrame(2, true);

            string method = stackFrame.GetMethod().ToString();
            int line = stackFrame.GetFileLineNumber();

            return line + ": " + method + ":: ";
        }
    }
}
