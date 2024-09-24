using System;
using System.Runtime.CompilerServices;

namespace Hertzole.Buffers
{
	public static partial class ArrayPoolScopeExtensions
	{
		/// <inheritdoc cref="System.MemoryExtensions.AsSpan{T}(T[])" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArrayPoolScope<T> array)
		{
			return array.array.AsSpan(0, array.Length);
		}

		/// <inheritdoc cref="System.MemoryExtensions.AsSpan{T}(T[], int)" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArrayPoolScope<T> array, int start)
		{
			return array.array.AsSpan(start, array.Length - start);
		}

		/// <inheritdoc cref="System.MemoryExtensions.AsSpan{T}(T[], int, int)" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArrayPoolScope<T> array, int start, int length)
		{
			return array.array.AsSpan(start, length);
		}

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
		/// <inheritdoc cref="System.MemoryExtensions.AsSpan{T}(T[], Index)" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArrayPoolScope<T> array, Index startIndex)
		{
			return array.array.AsSpan(new Range(startIndex, array.Length));
		}

		/// <inheritdoc cref="System.MemoryExtensions.AsSpan{T}(T[], Range)" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArrayPoolScope<T> array, Range range)
		{
			return array.array.AsSpan(range);
		}
#endif

		/// <inheritdoc cref="System.MemoryExtensions.AsMemory{T}(T[])" />
		public static Memory<T> AsMemory<T>(this ArrayPoolScope<T> array)
		{
			return array.array.AsMemory(0, array.Length);
		}

		/// <inheritdoc cref="System.MemoryExtensions.AsMemory{T}(T[], int)" />
		public static Memory<T> AsMemory<T>(this ArrayPoolScope<T> array, int start)
		{
			return array.array.AsMemory(start, array.Length - start);
		}

		/// <inheritdoc cref="System.MemoryExtensions.AsMemory{T}(T[], int, int)" />
		public static Memory<T> AsMemory<T>(this ArrayPoolScope<T> array, int start, int length)
		{
			return array.array.AsMemory(start, length);
		}

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
		/// <inheritdoc cref="System.MemoryExtensions.AsMemory{T}(T[], Index)" />
		public static Memory<T> AsMemory<T>(this ArrayPoolScope<T> array, Index startIndex)
		{
			return array.array.AsMemory(new Range(startIndex, array.Length));
		}

		/// <inheritdoc cref="System.MemoryExtensions.AsMemory{T}(T[], Range)" />
		public static Memory<T> AsMemory<T>(this ArrayPoolScope<T> array, Range range)
		{
			return array.array.AsMemory(range);
		}
#endif

		/// <inheritdoc cref="System.MemoryExtensions.CopyTo{T}(T[], System.Span{T})" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this ArrayPoolScope<T> array, Span<T> destination)
		{
			Span<T> span = array.array.AsSpan(0, array.Length);
			span.CopyTo(destination);
		}

		/// <inheritdoc cref="System.MemoryExtensions.CopyTo{T}(T[], Memory{T})" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this ArrayPoolScope<T> array, Memory<T> destination)
		{
			Memory<T> memory = array.array.AsMemory(0, array.Length);
			memory.CopyTo(destination);
		}
	}
}