﻿using System;
using PushEngine.Draw;

namespace PushEngine.Demos
{
    internal class DirectDemoQuad : PEClient
    {
        Quad quad = null;
        TextLabel label = null;
        Scene scene = null;

        internal DirectDemoQuad() : base()
        {
        }

        public override void Start()
        {
            base.Start();
            scene = new Scene();
            quad = scene.GetNewDrawElement<Quad>();
            quad.width = 100;
            quad.height = 100;
            quad.PostInit();

            label = scene.GetNewDrawElement<TextLabel>();
            label.PostInit(); 
        }

        public override void Update(OpenTK.FrameEventArgs e)
        {
            base.Update(e);

            if (context.Keyboard[OpenTK.Input.Key.A])
            {
                quad.position.X++;
            }
            if (context.Keyboard[OpenTK.Input.Key.B])
            {
                quad.setLeftPosition(0);
            }

            if (context.Keyboard[OpenTK.Input.Key.C])
            {
                quad.setTopPosition(0);
            }

            if (context.Keyboard[OpenTK.Input.Key.D])
            {
                label.Text = "a";
            }


        }

        public override void Render(OpenTK.FrameEventArgs e)
        {
            base.Render(e);
            scene.Render();
        }

        public override void Dispose()
        {
            base.Dispose();
            scene.Dispose();
            scene = null;

        }
    }
}
