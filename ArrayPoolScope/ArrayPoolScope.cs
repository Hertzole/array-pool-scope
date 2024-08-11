#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
#define NULLABLES
#endif

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;

namespace Hertzole.Buffers
{
	public readonly struct ArrayPoolScope<T> : IDisposable, IReadOnlyList<T>
	{
		internal readonly T[] array;
		internal readonly ArrayPool<T> pool;
		internal readonly bool clearArray;

		public int Count { get; }

		/// <summary>
		///     Creates a new ArrayPoolScope with the given length from a pool.
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

		public ArrayPoolScope(T[] array,
#if NULLABLES
			ArrayPool<T>? pool = null,
#else
			ArrayPool<T> pool = null,
#endif
			bool clearArray = false) : this((ICollection<T>) array, pool, clearArray) { }

		/// <summary>
		///     Creates a new ArrayPoolScope with the given array from a pool.
		/// </summary>
		/// <param name="list">The list the array will be filled with from the start.</param>
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
		///     Disposes this scope and returns the array to the pool.
		/// </summary>
		public void Dispose()
		{
			pool.Return(array, clearArray);
		}

		public struct Enumerator : IEnumerator<T>
		{
			private readonly T[] array;
			private readonly int length;
			private int index;
			private T current;

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

			public void Reset()
			{
				index = 0;
#if NULLABLES
				current = default!;
#else
				current = default;
#endif
			}

			public void Dispose() { }
		}
	}
}