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
		public void CreateLength_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values(true, false)] bool clearArray)
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length, null, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(ArrayPool<int>.Shared));
			Assert.That(scope.clearArray, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateLength_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values(true, false)] bool clearArray)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(length, pool);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearArray, Is.False);
		}

		[Test]
		public void CreateFromArray_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values(true, false)] bool clearArray)
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
			Assert.That(scope.clearArray, Is.EqualTo(clearArray));
		}

		[Test]
		public void CreateFromArray_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values(true, false)] bool clearArray)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array, pool);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(array));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearArray, Is.False);
		}

		[Test]
		public void CreateFromList_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values(true, false)] bool clearArray)
		{
			// Arrange
			List<int> list = new List<int>(length);
			for (int i = 0; i < length; i++)
			{
				list.Add(random.Next(int.MinValue, int.MaxValue));
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(list);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(list));
			Assert.That(scope.pool, Is.SameAs(ArrayPool<int>.Shared));
			Assert.That(scope.clearArray, Is.False);
		}

		[Test]
		public void CreateFromList_WithPool_ReturnsArrayWithCorrectLength([Values(1, 5, 16, 30, 100)] int length, [Values(true, false)] bool clearArray)
		{
			// Arrange
			List<int> list = new List<int>(length);
			for (int i = 0; i < length; i++)
			{
				list.Add(random.Next(int.MinValue, int.MaxValue));
			}

			ArrayPool<int> pool = ArrayPool<int>.Create();
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(list, pool);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope, Is.EquivalentTo(list));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearArray, Is.False);
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
	}
}