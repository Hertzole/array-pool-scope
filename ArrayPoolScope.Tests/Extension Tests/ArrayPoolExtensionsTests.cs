using System.Buffers;
using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class ArrayPoolExtensionsTests
	{
		[Test]
		public void RentScopeFromPool_ReturnsCorrectLengthAndPool([Values(true, false)] bool clearArray, [Values(1, 10, 100)] int length)
		{
			// Arrange
			ArrayPool<int> pool = ArrayPool<int>.Shared;

			// Act
			using ArrayPoolScope<int> scope = pool.RentScope(length, clearArray);

			// Assert
			Assert.That(scope, Has.Count.EqualTo(length));
			Assert.That(scope.pool, Is.SameAs(pool));
			Assert.That(scope.clearArray, Is.EqualTo(clearArray));
		}
	}
}