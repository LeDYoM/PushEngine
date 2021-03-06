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
		private Rectangle rect;

		public enum Direction
		{
			Left,
			Right,
			Up,
			Down,
		};


		private Direction direction;

		public TokenGroup (Scene parent, Rectangle rect_, Direction direction_)
		{
			parent.GetNewDrawElement<DynamicImageRenderer> ("TokenGroup");

			rect = rect_;
			direction = direction_;

			ResetModel ();
		}

		private void ResetModel()
		{
			model.Clear ();

			for (int x_ = 0; x_ < rect.Width; ++x_) {
				model.Add (new List<int> ());
				for (int y_ = 0; y_ < rect.Height; ++y_) {
					model [x].Add (0);
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
			setTokenInModel (new Point (rect.Width - 1, line), type_);

			for (int x = 0; x <  rect.Width; ++x) {
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

			for (int x = rect.Width - 1; x > -1 ; --x) {
				if (model [x] [line] != 0) {
					if (x == (rect.Width - 1)) {
						EndReached (new Point (x, line));
						model [x] [line] = 0;
					}
					moveModelToken(new Point(x, line), new Point(x + 1, line));
				}
			}
		}


		private void AddToBottom(int column, int type_)
		{
			setTokenInModel (new Point (column, rect.Height - 1), type_);

			for (int y = 0; y <  rect.Height; ++y) {
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

			for (int y = rect.Height - 1; y > -1; --y) {
				if (model [column] [y] != 0) {
					if (y == (rect.Height - 1)) {
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
			int max = isVertical ? rect.Width : rect.Height;

			Add (RandomGenerator.IntWithMax (max));
		}
	}
}
