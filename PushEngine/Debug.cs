using System;
using System.Diagnostics;

namespace PushEngine
{
    public static class Debug
    {
        public static void Assert(bool condition, object o = null)
        {
            if (!condition)
            {
                Debug.LogError(o);
                System.Diagnostics.Debug.Assert(condition);
            }
        }

        public static void LogError(object o)
        {
            Debug.Log(o);
        }

        public static void Log(object o)
        {
            if (o != null)
            {
                string output = extraInfo() + o.ToString();
                System.Diagnostics.Debug.WriteLine(output);
                Console.WriteLine(output);
            }
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
