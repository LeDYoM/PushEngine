using System;
using PushEngine.Draw;
using OpenTK.Graphics;

namespace PushEngine.Demos.Zooper
{
	public class GameScene : Scene
	{
        private Sprite player;
        float qWidth = 0.05f;
        float qHeight = 0.0625f;
        float pWidth = 0.125f;
        float pHeight = 0.0625f;
        float pPositionX = 0.125f;
        float pPositionY = 0.25f;

        int nLines = 5;
        int nCols = 20;

		public GameScene ()
		{
		}

		internal override void OnStart ()
		{
			base.OnStart ();

            SceneView.TopLeft = new OpenTK.Vector2d(-0.5, 0.5);
            SceneView.DownRight = new OpenTK.Vector2d(0.5, -0.5);
            CreateBoard();
            CreatePlayer();

            //CreateBall();
		}

        private const float pressRate = 0.7f;

        private void CreatePlayer()
        {
            player = GetNewDrawElement<Sprite>("playerSprite");
            player.CreateSprite(new System.Drawing.SizeF(pWidth, pHeight), Color4.Aqua);
            player.PositionX = pPositionX;
            player.PositionY = pPositionY;

            player.OnKey = delegate(KeyEventData kev_)
            {
                if (kev_.kData.KState == KeyData.KeyState.Pressing)
                {
                    if (kev_.kData.KeyId == Key.A)
                    {
                        player.PositionX -= (frameData.ellapsedSinceLastFrame * pressRate);
                    }
                    else if (kev_.kData.KeyId == Key.D)
                    {
                        player.PositionX += (frameData.ellapsedSinceLastFrame * pressRate);
                    }

                    if (player.LeftPosition < sceneDirector.CurrentScene.SceneView.TopLeft.X)
                    {
                        player.LeftPosition = sceneDirector.CurrentScene.SceneView.TopLeft.X;
                    }
                    else if (player.RightPosition > sceneDirector.CurrentScene.SceneView.DownRight.X)
                    {
                        player.RightPosition = sceneDirector.CurrentScene.SceneView.DownRight.X;
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
            block.CreateSprite(new System.Drawing.SizeF(qWidth, qHeight),
                x % 2 == 0 ?
                (y % 2 == 0 ? Color4.Red : Color4.Blue) : (y % 2 == 0 ? Color4.Yellow : Color4.Violet));
            block.Width = qWidth;
            block.Height = qHeight;
            block.LeftPosition = sceneDirector.CurrentScene.SceneView.TopLeft.X + (x * qWidth);
            block.TopPosition = sceneDirector.CurrentScene.SceneView.TopLeft.Y - (y * qHeight);
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

