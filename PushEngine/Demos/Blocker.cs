using System;
using System.Collections.Generic;
using PushEngine.Draw;
using OpenTK.Graphics;
using PushEngine.Input;

namespace PushEngine.Demos
{
    internal class Blocker : Client
    {
        private int leftBase;
        private int TopBase;
        private int rightBase;
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

        private const double pressRate = 500.0;

        private void CreatePlayer()
        {
            player = context.sceneDirector.CurrentScene.GetNewDrawElement<Quad>();
            player.Width = pWidth;
            player.Height = pHeight;
            player.BaseColor = Color4.Aqua;
            player.position.X = 100.0;
            player.position.Y = 200.0;

            player.OnKeyPressing = delegate(DrawElement self, Key key)
            {
                if (key == Key.A)
                {
                    self.position.X -= (context.frameData.ellapsedSinceLastFrame * pressRate);
                }
                else if (key == Key.D)
                {
                    player.position.X += (context.frameData.ellapsedSinceLastFrame * pressRate);
                }
            };
        }

        private void CreateBall()
        {
        }

        private void CreateBlock(int x, int y)
        {
            Quad block = context.sceneDirector.CurrentScene.GetNewDrawElement<Quad>();
            block.Width = qWidth;
            block.Height = qHeight;
            block.OnCreationCompleted = delegate(DrawElement self)
                {
                    self.setLeftPosition(leftBase + (x * qWidth));
                    self.setTopPosition(TopBase + (y * qHeight));
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


        public override void Update()
        {
            base.Update();
/*
            if (context.Keyboard[OpenTK.Input.Key.A])
            {
                player.position.X -= (context.frameData.ellapsedSinceLastFrame * pressRate);
            }
            else if (context.Keyboard[OpenTK.Input.Key.D])
            {
                player.position.X += (context.frameData.ellapsedSinceLastFrame * pressRate);
            }

            */
//            context.dVars.AddVar("playerAccel_x", playerAccel_x);
        }

    }
}
