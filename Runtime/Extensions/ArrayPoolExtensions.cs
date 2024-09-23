using System;
using System.Buffers;
using System.Collections.Generic;

namespace Hertzole.Buffers
{
	/// <summary>
	///     Extensions related to <see cref="ArrayPoolScope{T}" />
	/// </summary>
	public static partial class ArrayPoolScopeExtensions
	{
		/// <summary>
		///     Creates a new ArrayPoolScope with the given length from a pool.
		/// </summary>
		/// <param name="pool">The pool to rent from.</param>
		/// <param name="length">The length of the array.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <typeparam name="T">The type of the objects that are in the resource pool.</typeparam>
		/// <returns>An ArrayPoolScope of type T[].</returns>
		public static ArrayPoolScope<T> RentScope<T>(this ArrayPool<T> pool, int length, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			return new ArrayPoolScope<T>(length, pool, clearMode);
		}

		/// <summary>
		///     Creates a new ArrayPoolScope based on the given array.
		/// </summary>
		/// <param name="pool">The pool to rent from.</param>
		/// <param name="array">The array to copy from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <typeparam name="T">The type of the objects that are in the resource pool.</typeparam>
		/// <returns>An ArrayPoolScope of type T[].</returns>
		public static ArrayPoolScope<T> RentScope<T>(this ArrayPool<T> pool, T[] array, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			return new ArrayPoolScope<T>(array, pool, clearMode);
		}

		/// <summary>
		///     Creates a new ArrayPoolScope based on the collection.
		/// </summary>
		/// <param name="pool">The pool to rent from.</param>
		/// <param name="collection">The collection to copy from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <typeparam name="T">The type of the objects that are in the resource pool.</typeparam>
		/// <returns>An ArrayPoolScope of type T[].</returns>
		public static ArrayPoolScope<T> RentScope<T>(this ArrayPool<T> pool, ICollection<T> collection, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			return new ArrayPoolScope<T>(collection, pool, clearMode);
		}

		/// <summary>
		///     Creates a new ArrayPoolScope based on the given span.
		/// </summary>
		/// <param name="pool">The pool to rent from.</param>
		/// <param name="span">The span to copy from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <typeparam name="T">The type of the objects that are in the resource pool.</typeparam>
		/// <returns>An ArrayPoolScope of type T[].</returns>
		public static ArrayPoolScope<T> RentScope<T>(this ArrayPool<T> pool, ReadOnlySpan<T> span, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			return new ArrayPoolScope<T>(span, pool, clearMode);
		}

		/// <summary>
		///     Creates a new ArrayPoolScope based on the given memory.
		/// </summary>
		/// <param name="pool">The pool to rent from.</param>
		/// <param name="memory">The memory to copy from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <typeparam name="T">The type of the objects that are in the resource pool.</typeparam>
		/// <returns>An ArrayPoolScope of type T[].</returns>
		public static ArrayPoolScope<T> RentScope<T>(this ArrayPool<T> pool, ReadOnlyMemory<T> memory, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			return new ArrayPoolScope<T>(memory, pool, clearMode);
		}
	}
}