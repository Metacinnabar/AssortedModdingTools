using System;
using System.Collections.Generic;

namespace AssortedModdingTools.Extensions
{
	public static class OverhaulExtensions
	{
		public static IEnumerable<T> DependencySort<T>(this IEnumerable<T> source)
		{
			List<T> list = new List<T>();
			HashSet<T> visited = new HashSet<T>();

			foreach (T item in source)
			{
				Visit(item, visited, list);
			}

			return list;
		}

		private static void Visit<T>(T item, HashSet<T> visited, List<T> sorted)
		{
			if (!visited.Contains(item))
			{
				visited.Add(item);
				sorted.Add(item);
				return;
			}

			if (sorted.Contains(item))
				return;

			throw new Exception("Cyclic dependency found");
		}
	}
}
