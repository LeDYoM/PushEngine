using System;
using System.Diagnostics;

namespace PushEngine
{
    internal class DebugHelper
    {

        private bool enabled;
        private string category;

        internal DebugHelper(string category_)
        {
            enabled = true;
            category = category_;
        }

        internal void WriteLine(object obj)
        {
            Debug.WriteLineIf(enabled, obj, category);
        }

        internal void Assert(bool condition)
        {
            Debug.Assert(condition);
        }
    }

    internal static class Debugger
    {
        internal static DebugHelper getDH(string clName)
        {
            return new DebugHelper(clName);
        }
    }
}
