using System;
using System.Collections.Generic;
using PushEngine.Draw;
using OpenTK.Graphics;

namespace PushEngine.Demos
{
    internal class Blocker : Client
    {
        private int leftBase;
        private int TopBase;
        private int rightBase;
        private double player_x = 100;
        private double player_y = 200;
        private Quad player;
        int qWidth = 40;
        int qHeight = 50;
        int pWidth = 100;
        int pHeight = 50;

        internal Blocker()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();
            context.sceneDirector.GetNewAndPush();

            leftBase = context.viewPort.Left;
            TopBase = context.viewPort.Top;
            rightBase = context.viewPort.Right;

            CreateBoard();
            CreatePlayer();
            //CreateBall();
        }

        private void CreatePlayer()
        {
            player = context.sceneDirector.CurrentScene.GetNewDrawElement<Quad>();
            player.Width = pWidth;
            player.Height = pHeight;

//            player.setLeftPosition(player_x);
//            player.setTopPosition(player_y);
            player.BaseColor = Color4.Aqua;
        }

        private void CreateBall()
        {
        }

        private void CreateBlock(int x, int y)
        {
            Quad block = context.sceneDirector.CurrentScene.GetNewDrawElement<Quad>();
            block.Width = qWidth;
            block.Height = qHeight;
            block.OnCreationCompleted = delegate()
                {
                    block.setLeftPosition(leftBase + (x * qWidth));
                    block.setTopPosition(TopBase + (y * qHeight));
                };

            block.BaseColor = x % 2 == 0 ? 
                (y % 2 == 0 ? Color4.Red : Color4.Blue) : (y % 2 == 0 ?Color4.Yellow : Color4.Violet);
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

//            context.dVars.AddVar("playerAccel_x", playerAccel_x);
        }

    }
}
