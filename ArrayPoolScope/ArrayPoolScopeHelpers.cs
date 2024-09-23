using System;
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Hertzole.Buffers
{
	internal static class ArrayPoolScopeHelpers
	{
		public static readonly Random random = new Random();

		public static bool ShouldClear<T>(ArrayClearMode clearMode)
		{
			switch (clearMode)
			{
				case ArrayClearMode.Auto:
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
					return RuntimeHelpers.IsReferenceOrContainsReferences<T>();
#else
					return true;
#endif
				case ArrayClearMode.Always:
					return true;
				case ArrayClearMode.Never:
					return false;
				default:
					throw new ArgumentOutOfRangeException(nameof(clearMode), clearMode, null);
			}
		}
	}
}