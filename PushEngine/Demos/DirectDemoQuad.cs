using System;
using PushEngine.Draw;

namespace PushEngine.Demos
{
    internal class DirectDemoQuad : PEClient
    {
        Quad quad = null;
        TextLabel label = null;

        internal DirectDemoQuad() : base()
        {
        }

        public override void Start()
        {
            base.Start();
            quad = new Quad();
            quad.width = 100;
            quad.height = 100;
            quad.PostInit();

            label = new TextLabel();
            label.PostInit(); 
        }

        public override void Update(OpenTK.FrameEventArgs e)
        {
            base.Update(e);

            if (Keyboard[OpenTK.Input.Key.A])
            {
                quad.position.X++;
            }
            if (Keyboard[OpenTK.Input.Key.B])
            {
                quad.setLeftPosition(0);
            }

            if (Keyboard[OpenTK.Input.Key.C])
            {
                quad.setTopPosition(0);
            }

        }

        public override void Render(OpenTK.FrameEventArgs e)
        {
            base.Render(e);
            quad.Render();
            label.Render();
        }
    }
}
