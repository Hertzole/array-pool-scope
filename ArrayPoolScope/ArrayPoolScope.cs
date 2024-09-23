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
		internal readonly bool clearArray;
		
		/// <inheritdoc cref="IReadOnlyCollection{T}.Count" />
		public int Count { get; }

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> with the given length from a pool.
		/// </summary>
		/// <param name="length">The length of the array.</param>
		/// <param name="pool">If provided, it will get an array from that pool. Otherwise, it will use the <c>Shared</c> pool.</param>
		/// <param name="clearArray">Indicates whether the contents of the buffer should be cleared when disposed.</param>
		public ArrayPoolScope(int length,
#if NULLABLES
			ArrayPool<T>? pool = null,
#else
			ArrayPool<T> pool = null,
#endif
			bool clearArray = false)
		{
			Count = length;
			this.pool = pool ?? ArrayPool<T>.Shared;
			array = this.pool.Rent(length);
			this.clearArray = clearArray;
		}

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source array to
		///     the new pooled array.
		/// </summary>
		/// <param name="array">The source array that will be copied from.</param>
		/// <param name="pool">If provided, it will get an array from that pool. Otherwise, it will use the <c>Shared</c> pool.</param>
		/// <param name="clearArray">Indicates whether the contents of the buffer should be cleared when disposed.</param>
		public ArrayPoolScope(T[] array,
#if NULLABLES
			ArrayPool<T>? pool = null,
#else
			ArrayPool<T> pool = null,
#endif
			bool clearArray = false) : this((ICollection<T>) array, pool, clearArray) { }

		/// <summary>
		///     Creates a new <c>ArrayPoolScope</c> based on an existing array by copying all the values from the source list to
		///     the new pooled array.
		/// </summary>
		/// <param name="list">The source list that will be copied from.</param>
		/// <param name="pool">If provided, it will get an array from that pool. Otherwise, it will use the <c>Shared</c> pool.</param>
		/// <param name="clearArray">Indicates whether the contents of the buffer should be cleared when disposed.</param>
		public ArrayPoolScope(ICollection<T> list,
#if NULLABLES
			ArrayPool<T>? pool = null,
#else
			ArrayPool<T> pool = null,
#endif
			bool clearArray = false)
		{
			Count = list.Count;
			this.pool = pool ?? ArrayPool<T>.Shared;
			array = this.pool.Rent(Count);
			this.clearArray = clearArray;

			list.CopyTo(array, 0);
		}

		/// <inheritdoc cref="IReadOnlyList{T}.this" />
		/// <exception cref="ArgumentOutOfRangeException">If the index is below <c>0</c> or outside the provided length.</exception>
		public T this[int index]
		{
			get
			{
				// Casting to uint skips the bounds check.
				if (index < 0 || (uint) index >= (uint) Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
				}

				return array[index];
			}
			set
			{
				// Casting to uint skips the bounds check.
				if (index < 0 || (uint) index >= (uint) Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
				}

				array[index] = value;
			}
		}

		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
		public Enumerator GetEnumerator()
		{
			return new Enumerator(array, Count);
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

		/// <inheritdoc cref="System.Array.CopyTo(Array, int)" />
		public void CopyTo(Array targetArray, int index)
		{
			Array.Copy(array, index, targetArray, 0, Count);
		}

		/// <inheritdoc cref="System.Array.CopyTo(Array, long)" />
		public void CopyTo(Array targetArray, long index)
		{
			Array.Copy(array, index, targetArray, 0, Count);
		}

		/// <summary>
		///     Determines whether an element is in the array.
		/// </summary>
		/// <param name="item">The object to locate in the array.</param>
		/// <returns><c>true</c> if <c>item</c> is found in the array; otherwise, <c>false</c>.</returns>
		public bool Contains(T item)
		{
			return Array.IndexOf(array, item, 0, Count) >= 0;
		}

		/// <summary>
		///     Determines whether an element is in the array using the specified comparer.
		/// </summary>
		/// <param name="item">The object to locate in the array.</param>
		/// <param name="comparer">The comparer to use when comparing elements.</param>
		/// <returns><c>true</c> if <c>item</c> is found in the array; otherwise, <c>false</c>.</returns>
		public bool Contains(T item, IEqualityComparer<T> comparer)
		{
			for (int i = 0; i < Count; i++)
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
			return Array.IndexOf(array, item, 0, Count);
		}

		/// <summary>
		///     Returns the zero-based index of the first occurrence of a value in the array using the specified comparer.
		/// </summary>
		/// <param name="item">The object to locate in the array.</param>
		/// <param name="comparer">The comparer to use when comparing elements.</param>
		/// <returns>The zero-based index of the first occurrence of <c>item</c> within the entire array, if found; otherwise, -1.</returns>
		public int IndexOf(T item, IEqualityComparer<T> comparer)
		{
			for (int i = 0; i < Count; i++)
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
			Array.Reverse(array, 0, Count);
		}

		/// <summary>
		///     Sorts the elements in the array. The sort compares the elements to each other using the default comparer.
		/// </summary>
		public void Sort()
		{
			Array.Sort(array, 0, Count, Comparer<T>.Default);
		}

		/// <summary>
		///     Sorts the elements in the array. The sort compares the elements to each other using the specified comparer.
		/// </summary>
		/// <param name="comparer">The comparer to use when comparing elements.</param>
		public void Sort(IComparer<T> comparer)
		{
			Array.Sort(array, 0, Count, comparer);
		}

		/// <summary>
		/// Randomly shuffles the elements in the array.
		/// </summary>
		public void Shuffle()
		{
			Shuffle(ArrayPoolScopeGlobals.random);
		}

		/// <summary>
		/// Randomly shuffles the elements in the array using the provided random generator.
		/// </summary>
		/// <param name="random">The random generator.</param>
		public void Shuffle(Random random)
		{
			for (int i = Count - 1; i > 0; i--)
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
#if NET6_0_OR_GREATER
			ArgumentNullException.ThrowIfNull(match, nameof(match));
#else
			if (match == null)
			{
				throw new ArgumentNullException(nameof(match));
			}
#endif

			for (int i = 0; i < Count; i++)
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
			pool.Return(array, clearArray);
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
	}
}