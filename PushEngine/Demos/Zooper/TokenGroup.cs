using System;
using PushEngine.Draw.Components;
using PushEngine.Draw;
using System.Collections.Generic;

namespace PushEngine.Demos.Zooper
{
	public class TokenGroup
	{
		private DynamicImageRenderer diRenderer;
		private List<List<int>> model = new List<List<int>>();
		private int x = 0;
		private int y = 0;

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

			x = x_;
			y = y_;
			direction = direction_;

			ResetModel ();
		}

		private void ResetModel()
		{
			model.Clear ();

			for (int x_ = x; x_ < x; ++x_) {
				model.Add (new List<int> ());
				for (int y_ = y; y_ < y; ++y_) {
					model [x].Add (0);
				}
			}
		}

		public void setTokenInModel(int x, int y, int type_)
		{
			model [x] [y] = type_;
		}

		public void moveModelToken(int xCurrent, int yCurrent)
		{
			int temp = model [x] [y];

		}
	}
}

