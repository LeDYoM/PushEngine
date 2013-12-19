using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using PushEngine.Input;
using PushEngine.Containers;

namespace PushEngine
{
    sealed class PushEngineCore : GameWindow
    {
        private static PushEngineCore instance = null;
        internal static Configuration configuration = new Configuration();
        internal Renderer renderer = null;

        internal static void Create()
        {
            configuration.ReadConfigFile();
            instance = new PushEngineCore();
            instance.Title = configuration.configurationData.WindowTitle;
            instance.VSync = VSyncMode.Off;

            instance.InitSubModules();
        }

        internal static int Execute()
        {
            instance.Run();
            return 0;
        }

        internal static PushEngineCore Instance
        {
            get { return instance; }
        }

        private PushEngineCore() : base(configuration.configurationData.WindowSize.Width, 
            configuration.configurationData.WindowSize.Height, configuration.graphicsMode)
        {
            mainWindowContainer = new WindowContainer(
                configuration.configurationData.virtualWindowTopLeft,
                configuration.configurationData.virtualWindowDownRight);
        }

        internal ProcessManager processManager = new ProcessManager();
        internal Keyboard keyboard = new Keyboard();
        internal EventManager eManager = new EventManager();

        private void InitSubModules()
        {
            eManager.Start();
            renderer = new Renderer();
            keyboard.setKeyboard(instance.Keyboard);

            processManager.Start();

            instance.Keyboard.KeyDown += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(keyboard.ApplyKeyDown);
            instance.Keyboard.KeyUp += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(keyboard.ApplyKeyUp);

        }

        internal WindowContainer mainWindowContainer
        {
            private set;
            get;
        }

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
            mainWindowContainer.apply();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private double ellapsed = 0.0;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            ellapsed += e.Time;
            if (ellapsed > 1)
            {
                Title = configuration.configurationData.WindowTitle + " Ellapsed now: " + e.Time + "FPS: " + 1.0 / e.Time;
                ellapsed = 0;
            }

            eManager.ProcessEvents();
            processManager.OnUpdateFrame(e);

            keyboard.ApplyUpdate();
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

            eManager.Stop();
            eManager = null;
            processManager.Stop();
            processManager.Dispose();
            configuration.SaveConfigFile();
            configuration = null;

        }

    }
}
