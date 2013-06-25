using System;
using PushEngine.Draw;

namespace PushEngine.Demos
{
    internal class DebugVars : PEClient
    {
        Quad quad = null;
        TextLabel label = null;
        Scene scene = null;

        internal DebugVars()
            : base()
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
        }

        public override void Render(OpenTK.FrameEventArgs e)
        {
            base.Render(e);
        }

        public override void Dispose()
        {
            base.Dispose();

        }
    }
}
