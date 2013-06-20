using System;
using OpenTK;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace PushEngine
{
    sealed class PushEngineCore : GameWindow
    {
        private static PushEngineCore instance = null;
        internal static Configuration configuration = new Configuration();

        internal static void Create()
        {
            Debug.Assert(instance == null);
            configuration.Start();
            instance = new PushEngineCore();
        }

        internal static int Execute()
        {
            Debug.Assert(instance != null);
            instance.Run();
            return 0;
        }

        internal static PushEngineCore Instance
        {
            get { return instance; }
        }

        private PushEngineCore() : base(configuration.configurationData.WindowSize.Width, 
            configuration.configurationData.WindowSize.Height, configuration.configurationData.graphicsMode)
        {
            InitSubModules();
        }

        internal ProcessManager processManager = new ProcessManager();

        private void InitSubModules()
        {
            Debug.WriteLine("Starting submanagers");
            processManager.Start();
        }

        Projection systemProjection = new Projection(new Vector2(0, 0), new System.Drawing.SizeF(800, 600));

        private void clearScreen()
        {
            GL.ClearColor(configuration.configurationData.SystemBackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            systemProjection.apply();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            processManager.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            clearScreen();

            processManager.OnRenderFrame(e);
            SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            processManager.Stop();
            configuration.Stop();

            Debug.WriteLine("SubManagers stopped");

        }

    }
}
