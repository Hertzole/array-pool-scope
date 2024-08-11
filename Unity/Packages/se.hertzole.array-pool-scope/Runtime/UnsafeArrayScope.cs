namespace Hertzole.Buffers
{
	/// <summary>
	///     Contains unsafe utilities for working with <see cref="ArrayPoolScope{T}" />.
	/// </summary>
	public static class UnsafeArrayScope
	{
		/// <summary>
		///     Gets the array from the scope. This is unsafe and should only be used if you know what you're doing and explicitly
		///     want the array.
		/// </summary>
		/// <param name="scope">The scope to get the array from.</param>
		/// <typeparam name="T">The type of the array.</typeparam>
		/// <returns>The array from the scope.</returns>
		public static T[] GetArray<T>(in ArrayPoolScope<T> scope)
		{
			return scope.array;
		}
	}
}