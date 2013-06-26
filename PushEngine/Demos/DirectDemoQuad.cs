using System;
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

        public override PEData Data()
        {
            PEData data = base.Data();
            data.Name = "DirectDemoQuad";
            data.Version = "1.00";
            return data;

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

        public override void Update()
        {
            base.Update();

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

        public override void Render()
        {
            base.Render();
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
