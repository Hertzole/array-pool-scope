#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
#define NULLABLES
#endif

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;

namespace Hertzole.Buffers
{
	/// <summary>
	///     A scope that rents an array from an <see cref="ArrayPool{T}" /> and returns it when disposed.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the array.</typeparam>
	public readonly struct ArrayPoolScope<T> : IDisposable, IReadOnlyList<T>
	{
		internal readonly T[] array;
		internal readonly ArrayPool<T> pool;
		internal readonly ArrayClearMode clearMode;

		/// <inheritdoc cref="IReadOnlyCollection{T}.Count" />
		public int Length { get; }

		int IReadOnlyCollection<T>.Count
		{
			get { return Length; }
		}

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> with the given length from a pool. It will use the
		///     <see cref="ArrayPool{T}.Shared" /> pool.
		/// </summary>
		/// <param name="length">The length of the array.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		public ArrayPoolScope(int length, ArrayClearMode clearMode = ArrayClearMode.Auto) : this(length, ArrayPool<T>.Shared, clearMode) { }

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> with the given length from the provided pool.
		/// </summary>
		/// <param name="length">The length of the array.</param>
		/// <param name="pool">The pool to get the array from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <exception cref="ArgumentNullException">If the pool is null.</exception>
		public ArrayPoolScope(int length, ArrayPool<T> pool, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			ThrowHelper.ThrowIfNull(pool, nameof(pool));

			Length = length;
			this.pool = pool;
			array = this.pool.Rent(length);
			this.clearMode = clearMode;
		}

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source array to
		///     the new pooled array. It will use the <see cref="ArrayPool{T}.Shared" /> pool.
		/// </summary>
		/// <param name="array">The source array that will be copied from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		public ArrayPoolScope(T[] array, ArrayClearMode clearMode = ArrayClearMode.Auto) : this(array, ArrayPool<T>.Shared, clearMode) { }

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source array to
		///     the new pooled array.
		/// </summary>
		/// <param name="array">The source array that will be copied from.</param>
		/// <param name="pool">The pool to get the array from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <exception cref="ArgumentNullException">If the array is null.</exception>
		/// <exception cref="ArgumentNullException">If the pool is null.</exception>
		public ArrayPoolScope(T[] array, ArrayPool<T> pool, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			ThrowHelper.ThrowIfNull(array, nameof(array));
			ThrowHelper.ThrowIfNull(pool, nameof(pool));

			Length = array.Length;
			this.pool = pool;
			this.array = this.pool.Rent(Length);
			this.clearMode = clearMode;

			Array.Copy(array, this.array, Length);
		}

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source list to
		///     the new pooled array. It will use the <see cref="ArrayPool{T}.Shared" /> pool.
		/// </summary>
		/// <param name="list">The source list that will be copied from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		public ArrayPoolScope(ICollection<T> list, ArrayClearMode clearMode = ArrayClearMode.Auto) : this(list, ArrayPool<T>.Shared, clearMode) { }

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source list to
		///     the new pooled array.
		/// </summary>
		/// <param name="list">The source list that will be copied from.</param>
		/// <param name="pool">The pool to get the array from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <exception cref="ArgumentNullException">If the list is null.</exception>
		/// <exception cref="ArgumentNullException">If the pool is null.</exception>
		public ArrayPoolScope(ICollection<T> list, ArrayPool<T> pool, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			ThrowHelper.ThrowIfNull(list, nameof(list));
			ThrowHelper.ThrowIfNull(pool, nameof(pool));

			Length = list.Count;
			this.pool = pool;
			array = this.pool.Rent(Length);
			this.clearMode = clearMode;

			list.CopyTo(array, 0);
		}

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source span to
		///     the new pooled array. It will use the <see cref="ArrayPool{T}.Shared" /> pool.
		/// </summary>
		/// <param name="span">The source span that will be copied from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		public ArrayPoolScope(ReadOnlySpan<T> span, ArrayClearMode clearMode = ArrayClearMode.Auto) : this(span, ArrayPool<T>.Shared, clearMode) { }

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source span to
		///     the new pooled array.
		/// </summary>
		/// <param name="span">The source span that will be copied from.</param>
		/// <param name="pool">The pool to get the array from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <exception cref="ArgumentNullException">If the pool is null.</exception>
		public ArrayPoolScope(ReadOnlySpan<T> span, ArrayPool<T> pool, ArrayClearMode clearMode = ArrayClearMode.Auto)
		{
			ThrowHelper.ThrowIfNull(pool, nameof(pool));

			Length = span.Length;
			this.pool = pool;
			array = this.pool.Rent(Length);
			this.clearMode = clearMode;

			span.CopyTo(array);
		}

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source memory to
		///     the new pooled array. It will use the <see cref="ArrayPool{T}.Shared" /> pool.
		/// </summary>
		/// <param name="memory">The source memory that will be copied from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		public ArrayPoolScope(ReadOnlyMemory<T> memory, ArrayClearMode clearMode = ArrayClearMode.Auto) : this(memory.Span, ArrayPool<T>.Shared, clearMode) { }

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source memory to
		///     the new pooled array.
		/// </summary>
		/// <param name="memory">The source memory that will be copied from.</param>
		/// <param name="pool">The pool to get the array from.</param>
		/// <param name="clearMode">Determines if the array should be cleared when returning it to the pool.</param>
		/// <exception cref="ArgumentNullException">If the pool is null.</exception>
		public ArrayPoolScope(ReadOnlyMemory<T> memory, ArrayPool<T> pool, ArrayClearMode clearMode = ArrayClearMode.Auto) :
			this(memory.Span, pool, clearMode) { }

		/// <inheritdoc cref="IReadOnlyList{T}.this" />
		/// <exception cref="ArgumentOutOfRangeException">If the index is below <c>0</c> or outside the provided length.</exception>
		public T this[int index]
		{
			get
			{
				// Casting to uint skips the bounds check.
				if (index < 0 || (uint) index >= (uint) Length)
				{
					throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
				}

				return array[index];
			}
			set
			{
				// Casting to uint skips the bounds check.
				if (index < 0 || (uint) index >= (uint) Length)
				{
					throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
				}

				array[index] = value;
			}
		}

		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
		public Enumerator GetEnumerator()
		{
			return new Enumerator(array, Length);
		}

		/// <inheritdoc />
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		///     Copies the elements of the array to a new array.
		/// </summary>
		/// <param name="targetArray">The array to copy the elements to.</param>
		public void CopyTo(Array targetArray)
		{
			Array.Copy(array, 0, targetArray, 0, Length);
		}

		/// <inheritdoc cref="System.Array.CopyTo(Array, int)" />
		public void CopyTo(Array targetArray, int index)
		{
			Array.Copy(array, index, targetArray, 0, Length);
		}

#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
		/// <inheritdoc cref="System.Array.CopyTo(Array, long)" />
		public void CopyTo(Array targetArray, long index)
		{
			Array.Copy(array, index, targetArray, 0, Length);
		}
#endif

		/// <summary>
		///     Determines whether an element is in the array.
		/// </summary>
		/// <param name="item">The object to locate in the array.</param>
		/// <returns><c>true</c> if <c>item</c> is found in the array; otherwise, <c>false</c>.</returns>
		public bool Contains(T item)
		{
			return Array.IndexOf(array, item, 0, Length) >= 0;
		}

		/// <summary>
		///     Determines whether an element is in the array using the specified comparer.
		/// </summary>
		/// <param name="item">The object to locate in the array.</param>
		/// <param name="comparer">The comparer to use when comparing elements.</param>
		/// <returns><c>true</c> if <c>item</c> is found in the array; otherwise, <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException">comparer is null.</exception>
		public bool Contains(T item, IEqualityComparer<T> comparer)
		{
			ThrowHelper.ThrowIfNull(comparer, nameof(comparer));

			for (int i = 0; i < Length; i++)
			{
				if (comparer.Equals(array[i], item))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		///     Returns the zero-based index of the first occurrence of a value in the array.
		/// </summary>
		/// <param name="item">The object to locate in the array.</param>
		/// <returns>The zero-based index of the first occurrence of <c>item</c> within the entire array, if found; otherwise, -1.</returns>
		public int IndexOf(T item)
		{
			return Array.IndexOf(array, item, 0, Length);
		}

		/// <summary>
		///     Returns the zero-based index of the first occurrence of a value in the array using the specified comparer.
		/// </summary>
		/// <param name="item">The object to locate in the array.</param>
		/// <param name="comparer">The comparer to use when comparing elements.</param>
		/// <returns>The zero-based index of the first occurrence of <c>item</c> within the entire array, if found; otherwise, -1.</returns>
		/// <exception cref="ArgumentNullException">comparer is null.</exception>
		public int IndexOf(T item, IEqualityComparer<T> comparer)
		{
			ThrowHelper.ThrowIfNull(comparer, nameof(comparer));

			for (int i = 0; i < Length; i++)
			{
				if (comparer.Equals(array[i], item))
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		///     Reverses the sequence of the elements in a range of elements in the array.
		/// </summary>
		public void Reverse()
		{
			Array.Reverse(array, 0, Length);
		}

		/// <summary>
		///     Sorts the elements in the array. The sort compares the elements to each other using the default comparer.
		/// </summary>
		public void Sort()
		{
			Array.Sort(array, 0, Length, Comparer<T>.Default);
		}

		/// <summary>
		///     Sorts the elements in the array. The sort compares the elements to each other using the specified comparer.
		/// </summary>
		/// <param name="comparer">The comparer to use when comparing elements.</param>
		public void Sort(IComparer<T> comparer)
		{
			Array.Sort(array, 0, Length, comparer);
		}

		/// <summary>
		///     Sorts the elements in the array. The sort compares the elements to each other using the specified comparison.
		/// </summary>
		/// <param name="comparison">The comparison to use when comparing elements.</param>
		/// <exception cref="ArgumentNullException">comparison is null.</exception>
		public void Sort(Comparison<T> comparison)
		{
#if NET5_0_OR_GREATER
			Span<T> span = array.AsSpan(0, Length);
			span.Sort(comparison);
			span.CopyTo(array);
#else
			// The reason there's no check in .NET 5+ is because the span.Sort method already does it.
			// Here we do it to avoid creating a box for the comparison object if there's no need to sort.
			if (Length <= 1)
			{
				return;
			}

			Array.Sort(array, 0, Length, new Comparer(comparison));
#endif
		}

		/// <summary>
		///     Randomly shuffles the elements in the array.
		/// </summary>
		public void Shuffle()
		{
			Shuffle(ArrayPoolScopeHelpers.random);
		}

		/// <summary>
		///     Randomly shuffles the elements in the array using the provided random generator.
		/// </summary>
		/// <param name="random">The random generator.</param>
		/// <exception cref="ArgumentNullException">random is null.</exception>
		public void Shuffle(Random random)
		{
			ThrowHelper.ThrowIfNull(random, nameof(random));

			for (int i = Length - 1; i > 0; i--)
			{
				int j = random.Next(i + 1);
				T temp = array[i];
				array[i] = array[j];
				array[j] = temp;
			}
		}

		/// <summary>
		///     Determines whether every element in the array matches the conditions defined by the specified predicate.
		/// </summary>
		/// <param name="match">The predicate that defines the conditions to check against the elements.</param>
		/// <returns>
		///     <c>true</c> if every element in array matches the conditions defined by the specified predicate; otherwise,
		///     <c>false</c>. If there are no elements in the array, the return value is true.
		/// </returns>
		/// <exception cref="ArgumentNullException">match is null.</exception>
		public bool TrueForAll(Predicate<T> match)
		{
			ThrowHelper.ThrowIfNull(match, nameof(match));

			for (int i = 0; i < Length; i++)
			{
				if (!match(array[i]))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		///     Disposes this scope and returns the array to the pool.
		/// </summary>
		public void Dispose()
		{
			pool.Return(array, ArrayPoolScopeHelpers.ShouldClear<T>(clearMode));
		}

		/// <summary>
		///     An enumerator for the pooled array.
		/// </summary>
		public struct Enumerator : IEnumerator<T>
		{
			private readonly T[] array;
			private readonly int length;
			private int index;
			private T current;

			/// <inheritdoc />
			public T Current
			{
#if NULLABLES
				get { return current!; }
#else
				get { return current; }
#endif
			}

			object IEnumerator.Current
			{
#if NULLABLES
				get { return Current!; }
#else
				get { return Current; }
#endif
			}

			/// <summary>
			///     Creates a new <c>Enumerator</c> for the given array with a set length.
			/// </summary>
			/// <param name="array">The array to enumerate.</param>
			/// <param name="length">The length of the array.</param>
			public Enumerator(T[] array, int length)
			{
				this.array = array;
				this.length = length;
				index = 0;
#if NULLABLES
				current = default!;
#else
				current = default;
#endif
			}

			/// <inheritdoc />
			public bool MoveNext()
			{
				if (index < length)
				{
					current = array[index];
					index++;
					return true;
				}

				return false;
			}

			/// <inheritdoc />
			public void Reset()
			{
				index = 0;
#if NULLABLES
				current = default!;
#else
				current = default;
#endif
			}

			/// <inheritdoc />
			/// <remarks>This does nothing for this enumerator.</remarks>
			public void Dispose() { }
		}

#if !NET5_0_OR_GREATER
		private readonly struct Comparer : IComparer<T>
		{
			private readonly Comparison<T> comparison;

			public Comparer(Comparison<T> comparison)
			{
				this.comparison = comparison;
			}

			public int Compare(T x, T y)
			{
				return comparison(x, y);
			}
		}
#endif
	}
}