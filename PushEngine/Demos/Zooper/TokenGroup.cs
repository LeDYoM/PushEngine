using System;
using PushEngine.Draw.Components;
using PushEngine.Draw;
using System.Collections.Generic;
using System.Drawing;

namespace PushEngine.Demos.Zooper
{
	public class TokenGroup
	{
		private DynamicImageRenderer diRenderer;
		private List<List<int>> model = new List<List<int>>();
		private int width = 0;
		private int height = 0;

		public enum Direction
		{
			Left,
			Right,
			Up,
			Down
		};

		private Direction direction;

		public TokenGroup (Scene parent, int x_, int y_, Direction direction_)
		{
			parent.GetNewDrawElement<DynamicImageRenderer> ("TokenGroup");

			width = x_;
			height = y_;
			direction = direction_;

			ResetModel ();
		}

		private void ResetModel()
		{
			model.Clear ();

			for (int x_ = width; x_ < width; ++x_) {
				model.Add (new List<int> ());
				for (int y_ = height; y_ < height; ++y_) {
					model [width].Add (0);
				}
			}
		}

		private bool isHorizontal { get { return direction == Direction.Left || direction == Direction.Right; } }
		private bool isVertical { get { return !isHorizontal; } }

		private void EndReached(Point entered)
		{
		}

		public void setTokenInModel(Point p_, int type_)
		{
			model [p_.X] [p_.Y] = type_;
		}

		public void moveModelToken(Point origin_, Point dest_)
		{
			int temp = model [origin_.X] [origin_.Y];
			PEDebug.Assert (model [origin_.X] [origin_.Y] != 0, "No valid type in source position: " + origin_);
			PEDebug.Assert (model [dest_.X] [dest_.Y] == 0, "Destionation is not empty: " + dest_+": "+ model[dest_.X][dest_.Y]);

			model [dest_.X] [dest_.Y] = model [origin_.X] [origin_.Y];
			model [origin_.X] [origin_.Y] = 0;
		}

		private void AddToRight(int line, int type_)
		{
			setTokenInModel (new Point (width - 1, line), type_);

			for (int x = 0; x <  width; ++x) {
				if (model [x] [line] != 0) {
					if (x == 0) {
						EndReached (new Point (x, line));
						model [x] [line] = 0;
					}
					moveModelToken(new Point(x, line), new Point(x - 1, line));
				}
			}
		}

		private void AddToLeft(int line, int type_)
		{
			setTokenInModel (new Point (0, line), type_);

			for (int x = width - 1; x > -1 ; --x) {
				if (model [x] [line] != 0) {
					if (x == (width - 1)) {
						EndReached (new Point (x, line));
						model [x] [line] = 0;
					}
					moveModelToken(new Point(x, line), new Point(x + 1, line));
				}
			}
		}


		private void AddToBottom(int column, int type_)
		{
			setTokenInModel (new Point (column, height - 1), type_);

			for (int y = 0; y <  height; ++y) {
				if (model [column] [y] != 0) {
					if (y == 0) {
						EndReached (new Point (column, y));
						model [column] [y] = 0;
					}
					moveModelToken(new Point(column, y), new Point(column, y - 1));
				}
			}
		}

		private void AddToTop(int column, int type_)
		{
			setTokenInModel (new Point (column, 0), type_);

			for (int y = height - 1; y > -1; --y) {
				if (model [column] [y] != 0) {
					if (y == (height - 1)) {
						EndReached (new Point (column, y));
						model [column] [y] = 0;
					}
					moveModelToken(new Point(column, y), new Point(column, y - 1));
				}
			}
		}

		public void Add(int p, int type_)
		{
			PEDebug.Log ("Going to add: " + p + " type: " + type_);
			switch (direction)
			{
				case Direction.Left:
					AddToLeft (p, type_);
					break;
				case Direction.Right:
					AddToRight (p, type_);
					break;
				case Direction.Up:
					AddToBottom (p, type_);
					break;
				case Direction.Down:
					AddToTop (p, type_);
					break;
			}
		}

		public void AddRandom(int type_)
		{
			int max = isVertical ? width : height;

			Add (RandomGenerator.IntWithMax (max));
		}
	}
}
