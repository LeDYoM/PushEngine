using System;
using System.Collections.Generic;
using System.Drawing;
using PushEngine.Draw;

namespace PushEngine.Demos.Zooper
{
	public struct LevelCreationData
	{
		public List<Rectangle> rect;
		public List<TokenGroup.Direction> direction;
		public int numTypes;
	}

	public class Level
	{
		private List<TokenGroup> tokenGroups = new List<TokenGroup>();
		private int numTokenTypes;

		public Level (LevelCreationData data_, Scene scene_)
		{
			PEDebug.Assert (data_.numTypes > 0, "The number of types hass to be > 0");
			PEDebug.Assert (data_.rect.Count > 0, "The number of token groups has to be > 0");
            PEDebug.Assert (data_.rect.Count == data_.direction.Count, "The number of rects and directions must be the same");

			for (int i = 0; i < data_.rect.Count; ++i)
			{
				TokenGroup temp = new TokenGroup (scene_, data_.rect [i], data_.direction[i]);
			}
		}
	}
}
