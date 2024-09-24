using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class ScopeTests
	{
		private static readonly Random random = new Random();

		[Test]
		public void CreateLength_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length, null, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(ArrayPool<int>.Shared));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateLength_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length, pool, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromArray_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array, null, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(array));
			Assert.That(scope.pool, Is.SameAs(ArrayPool<int>.Shared));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromArray_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array, pool, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(array));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromList_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			List<int> list = new List<int>(length);
			for (int i = 0; i < length; i++)
			{
				list.Add(random.Next(int.MinValue, int.MaxValue));
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(list, clearMode: clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(list));
			Assert.That(scope.pool, Is.SameAs(ArrayPool<int>.Shared));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromList_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			List<int> list = new List<int>(length);
			for (int i = 0; i < length; i++)
			{
				list.Add(random.Next(int.MinValue, int.MaxValue));
			}

			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(list, pool, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(list));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromSpan_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			Span<int> span = new int[length];
			for (int i = 0; i < length; i++)
			{
				span[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(span, clearMode: clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(span.ToArray()));
			Assert.That(scope.pool, Is.SameAs(ArrayPool<int>.Shared));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromSpan_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			Span<int> span = new int[length];
			for (int i = 0; i < length; i++)
			{
				span[i] = random.Next(int.MinValue, int.MaxValue);
			}

			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(span, pool, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(span.ToArray()));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromMemory_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			Memory<int> memory = new int[length];
			for (int i = 0; i < length; i++)
			{
				memory.Span[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(memory, clearMode: clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(memory.ToArray()));
			Assert.That(scope.pool, Is.SameAs(ArrayPool<int>.Shared));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromMemory_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values] ArrayClearMode clearArray)
		{
			// Arrange
			Memory<int> memory = new int[length];
			for (int i = 0; i < length; i++)
			{
				memory.Span[i] = random.Next(int.MinValue, int.MaxValue);
			}

			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(memory, pool, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(memory.ToArray()));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void GetSetIndexer_ReturnsCorrectValue([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			// Assert
			for (int i = 0; i < length; i++)
			{
				Assert.That(scope[i], Is.EqualTo(i));
			}
		}

		[Test]
		public void GetIndexer_OutOfRange_ThrowsArgumentOutOfRangeException([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => _ = scope[length]); // Test equals to length.
			Assert.Throws<ArgumentOutOfRangeException>(() => _ = scope[length + 1]); // Test greater than length.
			Assert.Throws<ArgumentOutOfRangeException>(() => _ = scope[-1]); // Test less than 0.
		}

		[Test]
		public void SetIndexer_OutOfRange_ThrowsArgumentOutOfRangeException([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => scope[length] = 0); // Test equals to length.
			Assert.Throws<ArgumentOutOfRangeException>(() => scope[length + 1] = 0); // Test greater than length.
			Assert.Throws<ArgumentOutOfRangeException>(() => scope[-1] = 0); // Test less than 0.
		}

		[Test]
		public void Foreach_ReturnsCorrectValues([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			// Assert
			int index = 0;
			foreach (int value in scope)
			{
				Assert.That(value, Is.EqualTo(index));
				index++;
			}
		}

		[Test]
		public void Foreach_IEnumerableT_ReturnsCorrectValues([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			// Assert
			int index = 0;
			foreach (int value in (IEnumerable<int>) scope)
			{
				Assert.That(value, Is.EqualTo(index));
				index++;
			}
		}

		[Test]
		public void Foreach_IEnumerable_ReturnsCorrectValues([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			// Assert
			int index = 0;
			foreach (int value in (IEnumerable) scope)
			{
				Assert.That(value, Is.EqualTo(index));
				index++;
			}
		}

		[Test]
		public void CopyTo_Array_ReturnsCorrectValues([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			int[] array = new int[length];

			// Act
			scope.CopyTo(array);

			// Assert
			Assert.That(array, Is.EquivalentTo(scope));
		}

		[Test]
		public void CopyTo_Array_Int_ReturnsCorrectValues([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			int[] array = new int[length];

			// Act
			scope.CopyTo(array, 0);

			// Assert
			Assert.That(array, Is.EquivalentTo(scope));
		}

		[Test]
		public void CopyTo_Array_Long_ReturnsCorrectValues([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			int[] array = new int[length];

			// Act
			scope.CopyTo(array, 0L);

			// Assert
			Assert.That(array, Is.EquivalentTo(scope));
		}

		[Test]
		public void Dispose_ReturnsArrayToPool([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Create();
			ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length, pool);
			int[] originalArray = scope.array;

			// Act
			scope.Dispose();
			using ArrayPoolScope<int> newScope = new ArrayPoolScope<int>(length, pool);
			int[] newArray = newScope.array;

			// Assert
			Assert.That(newArray, Is.SameAs(originalArray));
		}

		[Test]
		public void Enumerator_Reset_ReturnsCorrectValues([Values(1, 5, 16, 30, 100)] int length)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length);
			for (int i = 0; i < length; i++)
			{
				scope[i] = i;
			}

			// Act
			using ArrayPoolScope<int>.Enumerator enumerator = scope.GetEnumerator();
			enumerator.MoveNext();
			enumerator.MoveNext();
			enumerator.Reset();

			// Assert
			Assert.That(enumerator.Current, Is.EqualTo(0));
		}

		[Test]
		public void Contains_ReturnsTrue()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = random.Next(int.MinValue, int.MaxValue);
			}

			int value = scope[50];

			// Act
			bool contains = scope.Contains(value);

			// Assert
			Assert.That(contains, Is.True);
		}

		[Test]
		public void Contains_WithComparer_ReturnsTrue()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = Guid.NewGuid().ToString();
			}

			string value = scope[50];

			// Act
			bool contains = scope.Contains(value, StringComparer.Ordinal);

			// Assert
			Assert.That(contains, Is.True);
		}

		[Test]
		public void Contains_ReturnsFalse()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = random.Next(1, int.MaxValue);
			}

			// Act
			bool contains = scope.Contains(0);

			// Assert
			Assert.That(contains, Is.False);
		}

		[Test]
		public void Contains_WithComparer_ReturnsFalse()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = Guid.NewGuid().ToString();
			}

			// Act
			bool contains = scope.Contains(Guid.NewGuid().ToString(), StringComparer.Ordinal);

			// Assert
			Assert.That(contains, Is.False);
		}

		[Test]
		public void Contains_NullComparer_ThrowsException()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);

			// Assert
			Assert.Throws<ArgumentNullException>(() => scope.Contains(Guid.NewGuid().ToString(), null!));
		}

		[Test]
		public void IndexOf_ReturnsCorrectIndex()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = random.Next(int.MinValue, int.MaxValue);
			}

			int value = scope[50];

			// Act
			int index = scope.IndexOf(value);

			// Assert
			Assert.That(index, Is.EqualTo(50));
		}

		[Test]
		public void IndexOf_WithComparer_ReturnsCorrectIndex()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = Guid.NewGuid().ToString();
			}

			string value = scope[50];

			// Act
			int index = scope.IndexOf(value, StringComparer.Ordinal);

			// Assert
			Assert.That(index, Is.EqualTo(50));
		}

		[Test]
		public void IndexOf_ReturnsNegativeOne()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = random.Next(1, int.MaxValue);
			}

			// Act
			int index = scope.IndexOf(0);

			// Assert
			Assert.That(index, Is.EqualTo(-1));
		}

		[Test]
		public void IndexOf_WithComparer_ReturnsNegativeOne()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = Guid.NewGuid().ToString();
			}

			// Act
			int index = scope.IndexOf(Guid.NewGuid().ToString(), StringComparer.Ordinal);

			// Assert
			Assert.That(index, Is.EqualTo(-1));
		}

		[Test]
		public void IndexOf_NullComparer_ThrowsException()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);

			// Assert
			Assert.Throws<ArgumentNullException>(() => scope.IndexOf(Guid.NewGuid().ToString(), null!));
		}

		[Test]
		public void Reverse_ReturnsReversedArray()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = i;
			}

			// Act
			scope.Reverse();

			// Assert
			for (int i = 0; i < 100; i++)
			{
				Assert.That(scope[i], Is.EqualTo(99 - i));
			}
		}

		[Test]
		public void Reverse_DoesNotReverseOutOfBounds()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			// The array should be 128 in size and therefor the last 28 elements should not be reversed.
			for (int i = 0; i < scope.array.Length; i++)
			{
				scope.array[i] = i;
			}

			// Act
			scope.Reverse();

			// Assert
			for (int i = 0; i < 100; i++)
			{
				Assert.That(scope.array[i], Is.EqualTo(99 - i));
			}

			for (int i = 100; i < 128; i++)
			{
				Assert.That(scope.array[i], Is.EqualTo(i));
			}
		}

		[Test]
		public void Sort_ReturnsSortedArray()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = random.Next(int.MinValue, int.MaxValue);
			}

			// Act
			scope.Sort();

			// Assert
			for (int i = 1; i < 100; i++)
			{
				Assert.That(scope[i - 1], Is.LessThanOrEqualTo(scope[i]));
			}
		}

		[Test]
		public void Sort_WithComparer_ReturnsSortedArray()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = Guid.NewGuid().ToString();
			}

			// Act
			scope.Sort(StringComparer.Ordinal);

			// Assert
			for (int i = 1; i < 100; i++)
			{
				Assert.That(string.Compare(scope[i - 1], scope[i], StringComparison.Ordinal), Is.LessThanOrEqualTo(0));
			}
		}

		[Test]
		public void Sort_DoesNotSortOutOfBounds()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			// The array should be 128 in size and therefor the last 28 elements should not be sorted.
			for (int i = 0; i < scope.array.Length; i++)
			{
				scope.array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			int[] originalLeftOver = new int[28];
			Array.Copy(scope.array, 100, originalLeftOver, 0, 28);

			// Act
			scope.Sort();

			// Assert
			for (int i = 1; i < 100; i++)
			{
				Assert.That(scope.array[i - 1], Is.LessThanOrEqualTo(scope.array[i]));
			}

			for (int i = 100; i < 128; i++)
			{
				Assert.That(scope.array[i], Is.EqualTo(originalLeftOver[i - 100]));
			}
		}

		[Test]
		public void Sort_WithComparer_DoesNotSortOutOfBounds()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			// The array should be 128 in size and therefor the last 28 elements should not be sorted.
			for (int i = 0; i < scope.array.Length; i++)
			{
				scope.array[i] = Guid.NewGuid().ToString();
			}

			string[] originalLeftOver = new string[28];
			Array.Copy(scope.array, 100, originalLeftOver, 0, 28);

			// Act
			scope.Sort(StringComparer.Ordinal);

			// Assert
			for (int i = 1; i < 100; i++)
			{
				Assert.That(string.Compare(scope.array[i - 1], scope.array[i], StringComparison.Ordinal), Is.LessThanOrEqualTo(0));
			}

			for (int i = 100; i < 128; i++)
			{
				Assert.That(scope.array[i], Is.EqualTo(originalLeftOver[i - 100]));
			}
		}

		[Test]
		public void Sort_WithComparison_ReturnsSortedArray()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = Guid.NewGuid().ToString();
			}

			// Act
			scope.Sort((x, y) => string.Compare(x, y, StringComparison.Ordinal));

			// Assert
			for (int i = 1; i < 100; i++)
			{
				Assert.That(string.Compare(scope[i - 1], scope[i], StringComparison.Ordinal), Is.LessThanOrEqualTo(0));
			}
		}

		[Test]
		public void Sort_WithComparison_DoesNotSortOutOfBounds()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(100);
			// The array should be 128 in size and therefor the last 28 elements should not be sorted.
			for (int i = 0; i < scope.array.Length; i++)
			{
				scope.array[i] = Guid.NewGuid().ToString();
			}

			string[] originalLeftOver = new string[28];
			Array.Copy(scope.array, 100, originalLeftOver, 0, 28);

			// Act
			scope.Sort((x, y) => string.Compare(x, y, StringComparison.Ordinal));

			// Assert
			for (int i = 1; i < 100; i++)
			{
				Assert.That(string.Compare(scope[i - 1], scope[i], StringComparison.Ordinal), Is.LessThanOrEqualTo(0));
			}

			for (int i = 100; i < 128; i++)
			{
				Assert.That(scope.array[i], Is.EqualTo(originalLeftOver[i - 100]));
			}
		}

		[Test]
		public void Sort_WithComparison_NotEnoughElements_DoesNothing()
		{
			// Arrange
			using ArrayPoolScope<string> scope = new ArrayPoolScope<string>(1);
			scope[0] = "a";

			// Act
			scope.Sort((x, y) => string.Compare(x, y, StringComparison.Ordinal));

			// Assert
			Assert.That(scope[0], Is.EqualTo("a"));
		}

		[Test]
		public void Shuffle_ReturnsShuffledArray()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = i;
			}

			// Act
			scope.Shuffle();

			// Assert
			bool isShuffled = false;
			for (int i = 1; i < 100; i++)
			{
				if (scope[i - 1] != i - 1)
				{
					isShuffled = true;
					break;
				}
			}

			Assert.That(isShuffled, Is.True);
		}

		[Test]
		public void Shuffle_NullRandom_ThrowsException()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);

			// Assert
			Assert.Throws<ArgumentNullException>(() => scope.Shuffle(null!));
		}

		[Test]
		public void TrueForAll_ReturnsTrue()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = i;
			}

			// Act
			bool all = scope.TrueForAll(i => i < 100);

			// Assert
			Assert.That(all, Is.True);
		}

		[Test]
		public void TrueForAll_ReturnsFalse()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);
			for (int i = 0; i < 100; i++)
			{
				scope[i] = i;
			}

			// Act
			bool all = scope.TrueForAll(i => i < 99);

			// Assert
			Assert.That(all, Is.False);
		}

		[Test]
		public void TrueForAll_NullPredicate_ThrowsException()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(100);

			// Assert
			Assert.Throws<ArgumentNullException>(() => scope.TrueForAll(null!));
		}
	}
}