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
	/// Provides extension methods for converting between <see cref="short"/> values and tuple representations.
	/// </summary>
	public static class ShortTupleExtensions
	{
		/// <summary>
		/// Combines a tuple of two <see cref="byte"/> values into a <see cref="short"/>.
		/// The first byte forms the high 8 bits, and the second forms the low 8 bits.
		/// </summary>
		/// <param name="tuple">A tuple containing two bytes.</param>
		/// <returns>A <see cref="short"/> constructed from the two bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short CombineToShort(this (byte, byte) tuple) =>
			(short)tuple.CombineToUShort();

		/// <summary>
		/// Combines a tuple of two <see cref="sbyte"/> values into a <see cref="short"/>.
		/// Each sbyte is cast to a byte before combining.
		/// </summary>
		/// <param name="tuple">A tuple containing two sbytes.</param>
		/// <returns>A <see cref="short"/> constructed from the two sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short CombineToShort(this (sbyte, sbyte) tuple) =>
			(short)tuple.CombineToUShort();

		/// <summary>
		/// Splits a <see cref="short"/> into a tuple of two <see cref="byte"/> values.
		/// The first element is the high byte, and the second is the low byte.
		/// </summary>
		/// <param name="value">The <see cref="short"/> value to split.</param>
		/// <returns>A tuple containing two bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte, byte) SplitIntoByte(this short value) =>
			((ushort)value).SplitIntoByte();

		/// <summary>
		/// Splits a <see cref="short"/> into a tuple of two <see cref="sbyte"/> values.
		/// The first element is the high sbyte, and the second is the low sbyte.
		/// </summary>
		/// <param name="value">The <see cref="short"/> value to split.</param>
		/// <returns>A tuple containing two sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte, sbyte) SplitIntoSByte(this short value) =>
			((ushort)value).SplitIntoSByte();
	}
}