namespace Hertzole.Buffers
{
	/// <summary>
	///     Decides if they array should be cleared when returning it to the pool.
	/// </summary>
	public enum ArrayClearMode : byte
	{
		/// <summary>
		///     Clears the array if the type is a reference type or contains a reference type.
		///     <remarks>If you're using .NET Standard 2.0, this will be the same as <see cref="Always" />.</remarks>
		/// </summary>
		Auto = 0,
		/// <summary>
		///     Always clears the array.
		/// </summary>
		Always = 1,
		/// <summary>
		///     Doesn't clear the array.
		/// </summary>
		Never = 2
	}
}