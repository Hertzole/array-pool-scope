using System;
using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class ThrowHelperTests
	{
		[Test]
		public void IsNull_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => ThrowHelper.ThrowIfNull(null, "param"));
		}

		[Test]
		public void IsNotNull_DoesNotThrow()
		{
			Assert.DoesNotThrow(() => ThrowHelper.ThrowIfNull(new object(), "param"));
		}
	}
}