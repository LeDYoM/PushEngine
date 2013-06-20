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
 
            state = State.Running;
        }

        public override void Update(OpenTK.FrameEventArgs e)
        {
            base.Update(e);

            if (Keyboard[OpenTK.Input.Key.A])
            {
                quad.position.X++;
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
