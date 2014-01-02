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
        int nLines = 5;
        int nCols = 20;

        internal Blocker()
            : base()
        {
            this.OnStart += delegate()
            {
                sceneDirector.addScenes(new string[] { "gamePlay" }, 0);

                sceneDirector.getByName("gamePlay");

                CreateBoard();
                CreatePlayer();
                //CreateBall();
            };
        }

        public override string  Name()
        {
            return "Blocker";
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

                        if (player.LeftPosition < sceneDirector.CurrentScene.TopLeft.X)
                        {
                            player.LeftPosition = sceneDirector.CurrentScene.TopLeft.X;
                        }
                        else if (player.RightPosition > sceneDirector.CurrentScene.DownRight.X)
                        {
                            player.RightPosition = sceneDirector.CurrentScene.DownRight.X;
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
            block.LeftPosition = sceneDirector.CurrentScene.TopLeft.X + (x * qWidth);
            block.TopPosition = sceneDirector.CurrentScene.TopLeft.Y - (y * qHeight);
        }

        private void CreateBoard()
        {
			for (int y = 0; y < nLines; y++)
            {
                for (int x = 0; x < nCols; x++)
                {
                    CreateBlock(x, y);
                }
            }
        }
    }
}
