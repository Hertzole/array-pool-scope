using System;
using System.Buffers;
using System.Collections.Generic;
using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class ArrayPoolExtensionsTests
	{
		private readonly Random random = new Random();

		[Test]
		public void RentScopeFromPool_ReturnsCorrectLengthAndPool([Values] ArrayClearMode clearArray, [Values(1, 10, 100)] int length)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Shared;

			// Act
			using ArrayPoolScope<int> scope = pool.RentScope(length, clearArray);

			// Assert
			Assert.That(scope, Has.Length.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
		}

		[Test]
		public void RentScopeFromPool_Array_ReturnsCorrectLengthAndPool([Values] ArrayClearMode clearArray, [Values(1, 10, 100)] int length)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Shared;
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next();
			}

			// Act
			using ArrayPoolScope<int> scope = pool.RentScope(array, clearArray);

			// Assert
			Assert.That(scope, Has.Length.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
			Assert.That(scope, Is.EquivalentTo(array));
		}

		[Test]
		public void RentScopeFromPool_Collection_ReturnsCorrectLengthAndPool([Values] ArrayClearMode clearArray, [Values(1, 10, 100)] int length)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Shared;
			List<int> list = new List<int>(length);
			for (int i = 0; i < length; i++)
			{
				list.Add(random.Next());
			}

			// Act
			using ArrayPoolScope<int> scope = pool.RentScope(list, clearArray);

			// Assert
			Assert.That(scope, Has.Length.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
			Assert.That(scope, Is.EquivalentTo(list));
		}

		[Test]
		public void RentScopeFromPool_Span_ReturnsCorrectLengthAndPool([Values] ArrayClearMode clearArray, [Values(1, 10, 100)] int length)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Shared;
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next();
			}

			// Act
			using ArrayPoolScope<int> scope = pool.RentScope(array.AsSpan(), clearArray);

			// Assert
			Assert.That(scope, Has.Length.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
			Assert.That(scope, Is.EquivalentTo(array));
		}

		[Test]
		public void RentScopeFromPool_Memory_ReturnsCorrectLengthAndPool([Values] ArrayClearMode clearArray, [Values(1, 10, 100)] int length)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Shared;
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next();
			}

			// Act
			using ArrayPoolScope<int> scope = pool.RentScope(array.AsMemory(), clearArray);

			// Assert
			Assert.That(scope, Has.Length.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearMode, Is.EqualTo(clearArray));
			Assert.That(scope, Is.EquivalentTo(array));
		}
	}
}