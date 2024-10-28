#if NET5_0_OR_GREATER // In anything below .NET 5, we can't get the span from anything other than arrays and lists.
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Hertzole.Buffers
{
	internal static class EnumerableExtensions
	{
		// Basically a backport of the .NET 9 TryGetSpan extension method.
		public static bool TryGetSpan<T>([NoEnumeration] this IEnumerable<T> source, out ReadOnlySpan<T> span)
		{
			Type sourceType = source.GetType();

			// Try to get from an array.
			if (sourceType == typeof(T[]))
			{
				span = Unsafe.As<T[]>(source);
				return true;
			}

			// Try to get from a list.
			if (sourceType == typeof(List<T>))
			{
				span = CollectionsMarshal.AsSpan(Unsafe.As<List<T>>(source));
				return true;
			}

			// We could not get a span.
			span = default;
			return false;
		}
	}
}
#endif