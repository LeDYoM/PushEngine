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
            OnEventReceived = delegate(PEEvent event_)
            {
                if (event_.isAction(PEEvent.ActionStartProcess))
                {
                    Context.sceneDirector.GetNewAndPush();

                    CreateBoard();
                    CreatePlayer();
                    //CreateBall();
                }
            };
        }

        private const double pressRate = 500.0;

        private void CreatePlayer()
        {
            player = Context.sceneDirector.CurrentScene.GetNewDrawElement<Sprite>();
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
                            player.PositionX -= (Context.frameData.ellapsedSinceLastFrame * pressRate);
                        }
                        else if (event_.EventKey == Key.D)
                        {
                            player.PositionX += (Context.frameData.ellapsedSinceLastFrame * pressRate);
                        }

                        if (player.LeftPosition < Context.parentContainer.TopLeft.X)
                        {
                            player.LeftPosition = Context.parentContainer.TopLeft.X;
                        }
                        else if (player.RightPosition > Context.parentContainer.DownRight.X)
                        {
                            player.RightPosition = Context.parentContainer.DownRight.X;
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
            Sprite block = Context.sceneDirector.CurrentScene.GetNewDrawElement<Sprite>();
            block.CreateSprite(new System.Drawing.SizeF(qWidth,qHeight), 
                x % 2 == 0 ? 
                (y % 2 == 0 ? Color4.Red : Color4.Blue) : (y % 2 == 0 ?Color4.Yellow : Color4.Violet));
            block.Width = qWidth;
            block.Height = qHeight;
            block.LeftPosition = Context.parentContainer.TopLeft.X + (x * qWidth);
            block.TopPosition = /*Context.parentContainer.TopLeft.Y*/0 + (y * qHeight);
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
    }
}
