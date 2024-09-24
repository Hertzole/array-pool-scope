#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
#define NULLABLES
#endif

using System;
#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace Hertzole.Buffers
{
	internal static class ThrowHelper
	{
#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
		// This method has full test coverage, but because of the Throw method, it won't reach the closing bracket.
		// That means the method will not be 100% covered. So let's just exclude it from coverage.
		[ExcludeFromCodeCoverage]
#endif
		public static void ThrowIfNull(
#if NULLABLES
			[NotNull] object? argument,
#else
			object argument,
#endif
			string paramName)
		{
#if NET6_0_OR_GREATER
			ArgumentNullException.ThrowIfNull(argument, paramName);
#else
			if (argument is null)
			{
				Throw(paramName);
			}
#endif
		}

#if !NET6_0_OR_GREATER
#if NULLABLES
		[DoesNotReturn]
#endif
		private static void Throw(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}
#endif
	}
}