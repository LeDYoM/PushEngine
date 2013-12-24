using System;
using System.Collections.Generic;
using PushEngine.Draw;
using OpenTK.Graphics;
using PushEngine.Input;

namespace PushEngine.Demos
{
    internal class Blocker : Client
    {
        private Sprite player;
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
            sceneDirector.GetNewAndPush();

            CreateBoard();
            CreatePlayer();
            //CreateBall();
        }

        private const double pressRate = 500.0;

        private void CreatePlayer()
        {
            player = sceneDirector.CurrentScene.GetNewDrawElement<Sprite>();
            player.CreateSprite(new System.Drawing.SizeF(pWidth, pHeight), Color4.Aqua);
            player.PositionX = 100;
            player.PositionY = 200;

            player.OnEventReceived = delegate(PEEvent event_)
            {
                if (event_.isKeyEvent)
                {
                    if (event_.IsKeyPressing)
                    {
                        if (event_.EventKey == Key.A)
                        {
                            player.PositionX -= (frameData.ellapsedSinceLastFrame * pressRate);
                        }
                        else if (event_.EventKey == Key.D)
                        {
                            player.PositionX += (frameData.ellapsedSinceLastFrame * pressRate);
                        }

                        if (player.LeftPosition < ParentContainer.TopLeft.X)
                        {
                            player.LeftPosition = ParentContainer.TopLeft.X;
                        }
                        else if (player.RightPosition > ParentContainer.DownRight.X)
                        {
                            player.RightPosition = ParentContainer.DownRight.X;
                        }
                    }
                }
            };
        }

        private void CreateBall()
        {
        }

        private void CreateBlock(int x, int y)
        {
            Sprite block = sceneDirector.CurrentScene.GetNewDrawElement<Sprite>();
            block.CreateSprite(new System.Drawing.SizeF(qWidth,qHeight), 
                x % 2 == 0 ? 
				(y % 2 == 0 ? Color4.Red : Color4.Blue) : (y % 2 == 0 ? Color4.Yellow : Color4.Violet));
            block.Width = qWidth;
            block.Height = qHeight;
            block.LeftPosition = ParentContainer.TopLeft.X + (x * qWidth);
			block.TopPosition = ParentContainer.TopLeft.Y - (y * qHeight);
        }

        private void CreateBoard()
        {
			for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    CreateBlock(x, y);
                }
            }
        }
    }
}
