using System;

namespace PushEngine
{
	public static class RandomGenerator
	{
		private Random rand = new Random();

		public static int IntInRange(int min, int max)
		{
			PEDebug.Assert (max > min, "Max must be greatter than min");

			return (rand.Next () % (max - min)) + min;
		}

		public static int IntWithMax(int max)
		{
			return IntInRange (0, max);
		}
	}
}

