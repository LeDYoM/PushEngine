using System;
using System.Diagnostics;

namespace PushEngine
{
    public static class PEDebug
    {
        public static void Assert(bool condition, object o = null)
        {
            if (!condition)
            {
                PEDebug.LogError(o);
                System.Diagnostics.Debug.Assert(condition);
            }
        }

        public static void LogError(object o)
        {
            PEDebug.Log(o);
        }

        public static void Log(object o)
        {
            if (o != null)
            {
                string output = extraInfo() + o.ToString();
				//                System.Diagnostics.Debug.WriteLine(output);
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
