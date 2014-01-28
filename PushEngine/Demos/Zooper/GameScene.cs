using System;
using PushEngine.Draw;
using OpenTK.Graphics;
using PushEngine.Events;
using PushEngine.Input;
using PushEngine.Draw.Components;

namespace PushEngine.Demos.Zooper
{
	public class GameScene : Scene
	{
        private Sprite player;
        float qWidth = 1f;
        float qHeight = 1f;
//        float qWidth = 0.05f;
//        float qHeight = 0.0625f;
        float pWidth = 0.125f;
        float pHeight = 0.0625f;
        float pPositionX = 0.125f;
        float pPositionY = 0.25f;

		int nLines = 10;
		int nCols = 10;

		public GameScene ()
		{
		}

		public override void OnStart ()
		{
			base.OnStart ();

            SceneView.TopLeft = new OpenTK.Vector2d(-0.5, 0.5);
            SceneView.DownRight = new OpenTK.Vector2d(0.5, -0.5);
            CreateBoard();
            CreatePlayer();

            //CreateBall();
		}

        private const float pressRate = 0.7f;

		public override void OnKey (KeyEventData kev_)
		{
			base.OnKey (kev_);
			if (kev_.kData.KState == KeyData.KeyState.Pressing)
			{
				if (kev_.kData.KeyId == Key.A)
				{
					player.PositionX -= (FrameData.EllapsedSinceLastFrame * pressRate);
				}
				else if (kev_.kData.KeyId == Key.D)
				{
					player.PositionX += (FrameData.EllapsedSinceLastFrame * pressRate);
				}

				if (player.LeftPosition < SceneView.TopLeft.X)
				{
					player.LeftPosition = SceneView.TopLeft.X;
				}
				else if (player.RightPosition > SceneView.DownRight.X)
				{
					player.RightPosition = SceneView.DownRight.X;
				}
			}
		}

        private void CreatePlayer()
        {
            player = GetNewDrawElement<Sprite>("playerSprite");
            player.CreateSprite(new System.Drawing.SizeF(pWidth, pHeight), Color4.Aqua);
            player.PositionX = pPositionX;
            player.PositionY = pPositionY;
        }

        private void CreateBall()
        {
        }

        private void CreateBlock(int x, int y)
        {
			Sprite block = GetNewDrawElement<Sprite>("block");
            block.CreateSprite(new System.Drawing.SizeF(qWidth, qHeight),
                x % 2 == 0 ?
                (y % 2 == 0 ? Color4.Red : Color4.Blue) : (y % 2 == 0 ? Color4.Yellow : Color4.Violet));
            block.Width = qWidth;
            block.Height = qHeight;
            block.LeftPosition = SceneView.TopLeft.X + (x * qWidth);
            block.TopPosition = SceneView.TopLeft.Y - (y * qHeight);
        }

        private void CreateBoard()
        {
            ImageRenderer iRenderer = GetNewDrawElement<ImageRenderer>("imageRenderer");
            iRenderer.Configure(nCols, nLines, new OpenTK.Vector2d(1, 1), false);
            for (int y = 0; y < nLines; y++)
            {
                for (int x = 0; x < nCols; x++)
                {
					iRenderer.setFormColor(x % 2 == 0 ? (y % 2 == 0 ? Color4.Red : Color4.Blue) : (y % 2 == 0 ? Color4.Yellow : Color4.Violet), x, y);
//                    iRenderer.setTopLeftFormPosition(new OpenTK.Vector3d(SceneView.TopLeft.X + (x * qWidth), SceneView.TopLeft.Y + (x * qHeight), 0), x, y);
                }
            }
        }
	}
}
