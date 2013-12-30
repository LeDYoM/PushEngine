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
        internal Configuration config = new Configuration();
        internal Renderer renderer = null;

        internal static void Create()
        {
            Configuration config = Configuration.ReadConfigFile();
            instance = new PushEngineCore(config);
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

        private PushEngineCore(Configuration config) : base(config.WindowSize.Width, config.WindowSize.Height, new GraphicsMode(new ColorFormat(config.bpp)))
        {
            Title = config.WindowTitle;
            VSync = VSyncMode.On;
            InitSubModules();
        }

        internal ClientManager clientManager = new ClientManager();
        internal Keyboard keyboard = new Keyboard();

        private void InitSubModules()
        {
            renderer = new Renderer();
            keyboard.setKeyboard(Keyboard);

            clientManager.Start();

            Keyboard.KeyDown += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(keyboard.ApplyKeyDown);
            Keyboard.KeyUp += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(keyboard.ApplyKeyUp);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            clientManager.OnResize(e);
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
                Title = config.WindowTitle + " Ellapsed now: " + e.Time + "FPS: " + 1.0 / e.Time;
                ellapsed = 0;
            }

            clientManager.ProcessEvents();
            clientManager.OnUpdateFrame(e);

            keyboard.ApplyUpdate();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            clientManager.OnRenderFrame(e);

            SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            clientManager.Stop();
            clientManager.Dispose();
            Configuration.SaveConfigFile();
            config = null;

        }

    }
}
