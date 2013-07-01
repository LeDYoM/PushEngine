using System;
using System.Collections.Generic;
using PushEngine.Draw;
using OpenTK.Graphics;

namespace PushEngine.Demos
{
    internal class Blocker : PEClient
    {
        Scene scene = null;
        private int leftBase;
        private int TopBase;
        private int rightBase;
        private double player_x = 100;
        private double player_y = 200;
        private Quad player;
        int qWidth = 40;
        int qHeight = 50;

        internal Blocker()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();
            leftBase = context.viewPort.Left;
            TopBase = context.viewPort.Top;
            rightBase = context.viewPort.Right;

            scene = new Scene();
            CreateBoard();
            CreatePlayer();
            CreateBall();
        }

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

        private double playerAccel_x = 0.0;
        private double accelRate_x = 1;
        private double pressRate = 500;
        private double maxAccel = 0.5;

        public override void Update()
        {
            base.Update();

            if (context.Keyboard[OpenTK.Input.Key.A])
            {
                playerAccel_x -= (context.frameData.ellapsedSinceLastFrame * pressRate);
            }
            else if (context.Keyboard[OpenTK.Input.Key.D])
            {
                playerAccel_x += (context.frameData.ellapsedSinceLastFrame * pressRate);
            }

            if (playerAccel_x > 0)
            {
                playerAccel_x -= (context.frameData.ellapsedSinceLastFrame * accelRate_x);
                if (playerAccel_x < 0.0)
                    playerAccel_x = 0.0;
                else if (playerAccel_x > maxAccel)
                    playerAccel_x = maxAccel;

            }
            else if (playerAccel_x < 0.0)
            {
                playerAccel_x += (context.frameData.ellapsedSinceLastFrame * accelRate_x);
                if (playerAccel_x > 0.0)
                    playerAccel_x = 0.0;
                else if (playerAccel_x < -1 * maxAccel)
                    playerAccel_x = -1 * maxAccel;
            }
            else
            {
                playerAccel_x = 0;
            }

            player_x += playerAccel_x;

            player.position.X = player_x;
            player.position.Y = player_y;

            context.dVars.AddVar("playerAccel_x", playerAccel_x);
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
