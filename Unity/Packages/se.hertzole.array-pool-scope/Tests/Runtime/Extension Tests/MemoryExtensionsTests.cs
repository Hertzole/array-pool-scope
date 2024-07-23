using System;
using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class MemoryExtensionsTests
	{
		private static readonly Random random = new Random();
		private static readonly object[] lengthSource = { 5, 16, 30, 100 };

		[Test]
		public void AsSpan([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Span<int> span = scope.AsSpan();

			// Assert
			Assert.That(span.Length, Is.EqualTo(length));
			for (int i = 0; i < length; i++)
			{
				Assert.That(span[i], Is.EqualTo(array[i]));
			}
		}

		[Test]
		public void AsSpan_Start([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Span<int> span = scope.AsSpan(1);

			// Assert
			Assert.That(span.Length, Is.EqualTo(length - 1));
			for (int i = 0; i < length - 1; i++)
			{
				Assert.That(span[i], Is.EqualTo(array[i + 1]));
			}
		}

		[Test]
		public void AsSpan_StartLength([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Span<int> span = scope.AsSpan(1, length - 2);

			// Assert
			Assert.That(span.Length, Is.EqualTo(length - 2));
			for (int i = 0; i < length - 2; i++)
			{
				Assert.That(span[i], Is.EqualTo(array[i + 1]));
			}
		}

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
		[Test]
		public void AsSpan_Index([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Span<int> span = scope.AsSpan(new Index(1));

			// Assert
			Assert.That(span.Length, Is.EqualTo(length - 1));
			for (int i = 0; i < length - 1; i++)
			{
				Assert.That(span[i], Is.EqualTo(array[i + 1]));
			}
		}

		[Test]
		public void AsSpan_Range([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Span<int> span = scope.AsSpan(new Range(1, length - 2));

			// Assert
			Assert.That(span.Length, Is.EqualTo(length - 3));
			for (int i = 0; i < length - 3; i++)
			{
				Assert.That(span[i], Is.EqualTo(array[i + 1]));
			}
		}
#endif

		[Test]
		public void AsMemory([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Memory<int> memory = scope.AsMemory();

			// Assert
			Assert.That(memory.Length, Is.EqualTo(length));
			for (int i = 0; i < length; i++)
			{
				Assert.That(memory.Span[i], Is.EqualTo(array[i]));
			}
		}

		[Test]
		public void AsMemory_Start([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Memory<int> memory = scope.AsMemory(1);

			// Assert
			Assert.That(memory.Length, Is.EqualTo(length - 1));
			for (int i = 0; i < length - 1; i++)
			{
				Assert.That(memory.Span[i], Is.EqualTo(array[i + 1]));
			}
		}

		[Test]
		public void AsMemory_StartLength([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Memory<int> memory = scope.AsMemory(1, length - 2);

			// Assert
			Assert.That(memory.Length, Is.EqualTo(length - 2));
			for (int i = 0; i < length - 2; i++)
			{
				Assert.That(memory.Span[i], Is.EqualTo(array[i + 1]));
			}
		}

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
		[Test]
		public void AsMemory_Index([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Memory<int> memory = scope.AsMemory(new Index(1));

			// Assert
			Assert.That(memory.Length, Is.EqualTo(length - 1));
			for (int i = 0; i < length - 1; i++)
			{
				Assert.That(memory.Span[i], Is.EqualTo(array[i + 1]));
			}
		}

		[Test]
		public void AsMemory_Range([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			// Act
			Memory<int> memory = scope.AsMemory(new Range(1, length - 2));

			// Assert
			Assert.That(memory.Length, Is.EqualTo(length - 3));
			for (int i = 0; i < length - 3; i++)
			{
				Assert.That(memory.Span[i], Is.EqualTo(array[i + 1]));
			}
		}
#endif

		[Test]
		public void CopyTo_Span([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			Span<int> destination = new int[length];

			// Act
			scope.CopyTo(destination);

			// Assert
			for (int i = 0; i < length; i++)
			{
				Assert.That(destination[i], Is.EqualTo(array[i]));
			}
		}

		[Test]
		public void CopyTo_Memory([ValueSource(nameof(lengthSource))] int length)
		{
			// Arrange
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = random.Next(int.MinValue, int.MaxValue);
			}

			using ArrayPoolScope<int> scope = new ArrayPoolScope<int>(array);

			Memory<int> destination = new int[length];

			// Act
			scope.CopyTo(destination);

			// Assert
			for (int i = 0; i < length; i++)
			{
				Assert.That(destination.Span[i], Is.EqualTo(array[i]));
			}
		}
	}
}