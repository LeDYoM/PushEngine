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
        private int boardLeftBase;
        private int boardRightBase;
        private Quad player;
        int qWidth = 40;
        int qHeight = 50;
        int pWidth = 100;
        int pHeight = 50;

        internal Blocker()
            : base()
        {
            OnEventReceived = delegate(PEEvent event_)
            {
                if (event_.IsStartForProcess)
                {
                    context.sceneDirector.GetNewAndPush();

                    leftBase = context.viewPort.Left;
                    TopBase = context.viewPort.Top;
                    rightBase = context.viewPort.Right;

                    boardLeftBase = leftBase;
                    boardRightBase = rightBase;

                    CreateBoard();
                    CreatePlayer();
                    //CreateBall();
                }
            };
        }

        public override void Start()
        {
            base.Start();
        }

        private const double pressRate = 500.0;

        private void CreatePlayer()
        {
            player = context.sceneDirector.CurrentScene.GetNewDrawElement<Quad>();
            player.Width = pWidth;
            player.Height = pHeight;
            player.BaseColor = Color4.Aqua;
            player.PositionX = 100.0;
            player.PositionY = 200.0;

            player.OnEventReceived = delegate(PEEvent event_)
            {
                if (event_.isKeyEvent)
                {
                    if (event_.IsKeyPressing)
                    {
                        if (event_.EventKey == Key.A)
                        {
                            player.PositionX -= (context.frameData.ellapsedSinceLastFrame * pressRate);
                        }
                        else if (event_.EventKey == Key.D)
                        {
                            player.PositionX += (context.frameData.ellapsedSinceLastFrame * pressRate);
                        }

                        if (player.LeftPosition < boardLeftBase)
                        {
                            player.LeftPosition = boardLeftBase;
                        }
                        else if (player.RightPosition > boardRightBase)
                        {
                            player.RightPosition = boardRightBase;
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
            Quad block = context.sceneDirector.CurrentScene.GetNewDrawElement<Quad>();
            block.Width = qWidth;
            block.Height = qHeight;
            block.OnCreationCompleted = delegate(DrawElement self)
                {
                    self.LeftPosition = leftBase + (x * qWidth);
                    self.TopPosition = TopBase + (y * qHeight);
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
    }
}
