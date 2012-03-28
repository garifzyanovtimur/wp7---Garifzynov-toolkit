using System;
using System.Collections.Generic;

namespace Garifzyanov.Toolkit.Linq
{
	public static class EnumerableExtension
	{
		public static void Each<T>(this IEnumerable<T> list, Action<T> action)
		{
			foreach (var obj in list)
			{
				action(obj);
			}
		}

		public static void Each<T>(this IEnumerable<T> list, Action<T, int> action)
		{
			var ind = 0;
			foreach (var obj in list)
			{
				action(obj, ind);
				ind++;
			}
		}

		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> list, int chunkLength)
		{
			var counter = 0;
			var currentChunk = new List<T>();
			
			foreach (var obj in list)
			{
				counter++;
				currentChunk.Add(obj);

				if (counter == chunkLength)
				{
					yield return currentChunk;
					currentChunk = new List<T>();
					counter = 0;
				}
			}

			if (currentChunk.Count > 0)
			{
				yield return currentChunk;
			}
		}

	}
}
