﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace PushEngine
{
    sealed class PushEngineCore : GameWindow
    {
        private DebugHelper dh = Debugger.getDH("PushEngineCore");

        private static PushEngineCore instance = null;
        internal static Configuration configuration = new Configuration();

        internal static void Create()
        {
            configuration.Start();
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
            systemProjection = new Projection(
                configuration.configurationData.virtualWindowTopLeft,
                configuration.configurationData.virtualWindowDownRight);
        }

        internal ProcessManager processManager = new ProcessManager();

        private void InitSubModules()
        {
            dh.WriteLine("Starting submanagers");
            processManager.Start();
        }

        Projection systemProjection;

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
            processManager.Dispose();
            configuration.Stop();
            configuration = null;

            dh.WriteLine("SubManagers stopped");

        }

    }
}
