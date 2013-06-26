using System;
using System.Collections.Generic;
using PushEngine.Draw;
using OpenTK.Graphics;

namespace PushEngine.Demos
{
    internal class Blocker : PEClient
    {
        Scene scene = null;

        internal Blocker()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();
            scene = new Scene();
            CreateBoard();
        }

        private void CreateBlock(int x, int y)
        {
            int leftBase = -250;
            int TopBase = -250;
            int qWidth = 40;
            int qHeight = 20;

            Quad block = scene.GetNewDrawElement<Quad>();
            block.width = qWidth;
            block.height = qHeight;
            block.setLeftPosition(leftBase + (x * qWidth));
            block.setTopPosition(TopBase + (y * qHeight));
            block.baseColor = Color4.Green;
            block.PostInit();
        }

        private void CreateBoard()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    CreateBlock(x, y);
                }
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
            scene.Render();
        }

        public override void Dispose()
        {
            base.Dispose();

        }
    }
}
