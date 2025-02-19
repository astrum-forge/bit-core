#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

using System;
#if BITCORE_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif

namespace BitCore
{
	/// <summary>
	/// Provides extension methods for combining and splitting <see cref="ushort"/> values using tuples.
	/// </summary>
	public static class UShortTupleExtensions
	{
		/// <summary>
		/// Combines a tuple of two <see cref="byte"/> values into a single <see cref="ushort"/>.
		/// The first element becomes the high byte and the second the low byte.
		/// </summary>
		/// <param name="tuple">A tuple containing two bytes.</param>
		/// <returns>A <see cref="ushort"/> constructed from the two bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort CombineToUShort(this (byte, byte) tuple) =>
			(ushort)(((ushort)tuple.Item1 << 8) | tuple.Item2);

		/// <summary>
		/// Combines a tuple of two <see cref="sbyte"/> values into a single <see cref="ushort"/>.
		/// Each sbyte is cast to a byte, with the first becoming the high byte and the second the low byte.
		/// </summary>
		/// <param name="tuple">A tuple containing two sbytes.</param>
		/// <returns>A <see cref="ushort"/> constructed from the two sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort CombineToUShort(this (sbyte, sbyte) tuple) =>
			(ushort)(((ushort)(byte)tuple.Item1 << 8) | (byte)tuple.Item2);

		/// <summary>
		/// Splits a <see cref="ushort"/> into a tuple of two <see cref="byte"/> values.
		/// The first element is the high byte and the second is the low byte.
		/// </summary>
		/// <param name="value">The <see cref="ushort"/> value to split.</param>
		/// <returns>A tuple containing the high and low bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte, byte) SplitIntoByte(this ushort value) =>
			((byte)(value >> 8), (byte)value);

		/// <summary>
		/// Splits a <see cref="ushort"/> into a tuple of two <see cref="sbyte"/> values.
		/// The first element is the high byte and the second is the low byte, cast to sbyte.
		/// </summary>
		/// <param name="value">The <see cref="ushort"/> value to split.</param>
		/// <returns>A tuple containing the high and low sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte, sbyte) SplitIntoSByte(this ushort value) =>
			((sbyte)(value >> 8), (sbyte)value);
	}
}