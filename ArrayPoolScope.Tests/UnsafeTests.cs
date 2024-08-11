using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class UnsafeTests
	{
		[Test]
		public void GetArray_ReturnsArray()
		{
			// Arrange
			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(10);
			for (int i = 0; i < 10; i++)
			{
				scope[i] = i;
			}

			// Act
			int[] array = UnsafeArrayScope.GetArray(scope);

			// Assert
			Assert.That(array, Is.EqualTo(scope.array));
			Assert.That(array, Is.SameAs(scope.array));
		}
	}
}