using System;
namespace PocSocketMaui.Commons.Extensions
{
	public static class EnumerableExtension
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable is null)
				return true;

			return !enumerable.Any();
		}

		public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return !(enumerable?.IsNullOrEmpty() ?? true);
		}

		public static IEnumerable<TSource> DistinctBy<TSource, TKey>
			(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			var seenKeys = new HashSet<TKey>();
			foreach (var element in source)
			{
				if (seenKeys.Add(keySelector(element)))
					yield return element;
			}
		}

		public static IEnumerable<T> AggregateSafe<T>(IEnumerable<T> firstEnumerable, IEnumerable<T> secondEnumerable)
		{
			var result = new List<T>();
			if (firstEnumerable.IsNotNullOrEmpty())
				result.AddRange(firstEnumerable);

			if (secondEnumerable.IsNotNullOrEmpty())
				result.AddRange(secondEnumerable);

			return result;
		}

		public static IEnumerable<IEnumerable<T>> GroupInto<T>(
			this IEnumerable<T> source,
			int count)
		{
			using (var e = source.GetEnumerator())
			{
				while (e.MoveNext())
					yield return GroupIntoHelper(e, count);
			}
		}

		private static IEnumerable<T> GroupIntoHelper<T>(
			IEnumerator<T> e,
			int count)
		{
			do
			{
				yield return e.Current;

				count--;
			}
			while (count > 0 && e.MoveNext());
		}
	}
}

