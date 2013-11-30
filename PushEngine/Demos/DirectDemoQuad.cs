using System;
using PushEngine.Draw;
using System.Collections.Generic;

namespace PushEngine.Demos
{
    internal class DirectDemoQuad : Client
    {
        Sprite quad = null;

        internal DirectDemoQuad() : base()
        {
            OnEventReceived = delegate(PEEvent event_)
            {
                if (event_.isAction(PEEvent.ActionStartProcess))
                {
                    Scene scene = context.sceneDirector.GetNewAndPush();
                    quad = scene.GetNewDrawElement<Sprite>();
                    quad.Width = 100;
                    quad.Height = 100;
                }
            };
        }

        public override ClientData Data()
        {
            ClientData data = base.Data();
            data.Name = "DirectDemoQuad";
            data.Version = "1.00";
            return data;

        }

        public override void Update()
        {
            base.Update();
            /*
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
             * */
        }
    }
}
