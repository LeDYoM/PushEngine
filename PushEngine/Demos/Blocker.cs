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
            CreatePlayer();
            CreateBall();
        }

        private float player_x = 100;
        private float player_y = 200;
        private Quad player;


        private void CreatePlayer()
        {
            player = scene.GetNewDrawElement<Quad>();
            player.width = 100;
            player.height = 40;
            player.setLeftPosition(player_x);
            player.setTopPosition(player_y);
            player.baseColor = Color4.Aqua;
            player.PostInit();
        }

        private void CreateBall()
        {
        }

        private void CreateBlock(int x, int y)
        {
            int leftBase = context.viewPort.Left;
            int TopBase = context.viewPort.Top;
            int qWidth = 40;
            int qHeight = 50;

            Quad block = scene.GetNewDrawElement<Quad>();
            block.width = qWidth;
            block.height = qHeight;
            block.setLeftPosition(leftBase + (x * qWidth));
            block.setTopPosition(TopBase + (y * qHeight));
            block.baseColor = x % 2 == 0 ? 
                (y % 2 == 0 ? Color4.Red : Color4.Blue) : (y % 2 == 0 ?Color4.Yellow : Color4.Violet);
            block.PostInit();
        }

        private void CreateBoard()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    CreateBlock(x, y);
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if (context.Keyboard[OpenTK.Input.Key.A])
            {
                player_x -= (float)(context.frameData.ellapsedSinceLastFrame * 1000);
            }
            else if (context.Keyboard[OpenTK.Input.Key.D])
            {
                player_x += (float)(context.frameData.ellapsedSinceLastFrame * 1000);
            }

            player.position.X = player_x;
            player.position.Y = player_y;
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
