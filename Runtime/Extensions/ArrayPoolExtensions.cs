using System.Buffers;

namespace Hertzole.Buffers
{
	/// <summary>
	///     Extensions related to <see cref="ArrayPoolScope{T}" />
	/// </summary>
	public static partial class ArrayPoolScopeExtensions
	{
		/// <summary>
		///     Creates a new ArrayPoolScope with the given length from a pool.
		/// </summary>
		/// <param name="pool">The pool to rent from.</param>
		/// <param name="length">The length of the array.</param>
		/// <param name="clearArray">Indicates whether the contents of the buffer should be cleared when disposed.</param>
		/// <typeparam name="T">The type of the objects that are in the resource pool.</typeparam>
		/// <returns>An ArrayPoolScope of type T[].</returns>
		public static ArrayPoolScope<T> RentScope<T>(this ArrayPool<T> pool, int length, bool clearArray = false)
		{
			return new ArrayPoolScope<T>(length, pool, clearArray);
		}
	}
}