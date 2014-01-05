using System;
using OpenTK;
using System.Drawing;
using PushEngine.Draw;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace PushEngine.Containers
{
    public class Container : LeafContainer
    {
        protected List<LeafClientContainer> elements = new List<LeafClientContainer>();

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            StartContainer();
            elements.ForEach(x => x.Render());
            FinishContainer();
        }
    }
}
