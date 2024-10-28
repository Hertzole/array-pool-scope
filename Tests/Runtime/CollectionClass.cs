using System;
using System.Collections;
using System.Collections.Generic;

namespace ArrayPoolScope.Tests
{
	public class CollectionClass<T> : ICollection, IEnumerable<T>
	{
		private readonly T[] items;

		public int Count { get; }
		public bool IsSynchronized
		{
			get { return false; }
		}
		public object SyncRoot
		{
			get { return this; }
		}

		public CollectionClass(int count)
		{
			Count = count;
			items = new T[count];
		}

		public T this[int index]
		{
			get { return items[index]; }
			set { items[index] = value; }
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return (IEnumerator<T>)GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public void CopyTo(Array array, int index)
		{
			Array.Copy(items, 0, array, index, Count);
		}
	}
}