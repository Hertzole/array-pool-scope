#if NET5_0_OR_GREATER // Only applies to .NET 5 and above.
using System;
using System.Collections.Generic;
using System.Linq;
using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class EnumerableExtensionsTests
	{
		private readonly Random random = new Random();

		[Test]
		public void TryGetSpan_Array_ReturnsTrue()
		{
			int[] array = new int[5];
			Assert.That(array.TryGetSpan(out _), Is.True);
		}

		[Test]
		public void TryGetSpan_Array_ReturnsSpan()
		{
			int[] array = new int[5];
			for (int i = 0; i < 5; i++)
			{
				array[i] = random.Next();
			}

			Assert.That(array.TryGetSpan(out ReadOnlySpan<int> span), Is.True);
			Assert.That(span.Length, Is.EqualTo(5));
			for (int i = 0; i < 5; i++)
			{
				Assert.That(span[i], Is.EqualTo(array[i]));
			}
		}

		[Test]
		public void TryGetSpan_List_ReturnsTrue()
		{
			List<int> list = new List<int>();
			Assert.That(list.TryGetSpan(out _), Is.True);
		}

		[Test]
		public void TryGetSpan_List_ReturnsSpan()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < 5; i++)
			{
				list.Add(random.Next());
			}

			Assert.That(list.TryGetSpan(out ReadOnlySpan<int> span), Is.True);
			Assert.That(span.Length, Is.EqualTo(5));
			for (int i = 0; i < 5; i++)
			{
				Assert.That(span[i], Is.EqualTo(list[i]));
			}
		}

		[Test]
		public void TryGetSpan_CollectionClass_ReturnsFalse()
		{
			CollectionClass<int> collection = new CollectionClass<int>(5);
			Assert.That(collection.TryGetSpan(out _), Is.False);
		}

		[Test]
		public void TryGetSpan_Enumerable_ReturnsFalse()
		{
			IEnumerable<int> enumerable = Enumerable.Range(0, 10);
			Assert.That(enumerable.TryGetSpan(out _), Is.False);
		}
	}
}
#endif