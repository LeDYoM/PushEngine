using System;

namespace PushEngine
{
	class Program
	{	
        /// <summary>
        /// Entry point of this program.
        /// </summary>
        [STAThread]
		public static void Main(string[] args)
		{	
            PushEngineCore.Create();
            PushEngineCore.Execute();
		}
	}
}