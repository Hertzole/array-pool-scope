using System;
using Hertzole.Buffers;
using NUnit.Framework;

namespace ArrayPoolScope.Tests
{
	public class ClearModeTests
	{
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
		[Test]
		public void Auto_ValueType_ReturnsFalse()
		{
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStruct>(ArrayClearMode.Auto), Is.False);
		}

		[Test]
		public void Auto_ValueTypeWithReference_ReturnsTrue()
		{
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStructWithReference>(ArrayClearMode.Auto), Is.True);
		}

		[Test]
		public void Auto_ReferenceType_ReturnsTrue()
		{
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleClass>(ArrayClearMode.Auto), Is.True);
		}
#else
		[Test]
		public void Auto_ReturnsTrue()
		{
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStruct>(ArrayClearMode.Auto), Is.True);
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStructWithReference>(ArrayClearMode.Auto), Is.True);
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleClass>(ArrayClearMode.Auto), Is.True);
		}
#endif

		[Test]
		public void Always_ReturnsTrue()
		{
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStruct>(ArrayClearMode.Always), Is.True);
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStructWithReference>(ArrayClearMode.Always), Is.True);
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleClass>(ArrayClearMode.Always), Is.True);
		}

		[Test]
		public void Never_ReturnsFalse()
		{
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStruct>(ArrayClearMode.Never), Is.False);
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleStructWithReference>(ArrayClearMode.Never), Is.False);
			Assert.That(ArrayPoolScopeHelpers.ShouldClear<SimpleClass>(ArrayClearMode.Never), Is.False);
		}

		[Test]
		public void InvalidClearMode_ThrowsException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => ArrayPoolScopeHelpers.ShouldClear<SimpleStruct>((ArrayClearMode) 3));
		}

		public struct SimpleStruct
		{
			public int value;
		}

		public struct SimpleStructWithReference
		{
			public int value;
			public object reference;
		}

		public class SimpleClass
		{
			public int value;
		}
	}
}