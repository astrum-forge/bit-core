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
	/// Provides extension methods for converting between <see cref="long"/> values and tuples.
	/// These methods convert tuples of various numeric types into a signed 64‑bit integer and vice versa.
	/// </summary>
	public static class LongTupleExtensions
	{
		/// <summary>
		/// Combines a tuple of two <see cref="int"/> values into a signed 64‑bit integer.
		/// Each int is converted to an unsigned 32‑bit value before combining.
		/// </summary>
		/// <param name="tuple">A tuple containing two ints.</param>
		/// <returns>A signed 64‑bit integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long CombineToLong(this (int, int) tuple) =>
			(long)tuple.CombineToULong();

		/// <summary>
		/// Combines a tuple of two <see cref="uint"/> values into a signed 64‑bit integer.
		/// The first element forms the upper 32 bits and the second the lower 32 bits.
		/// </summary>
		/// <param name="tuple">A tuple containing two uints.</param>
		/// <returns>A signed 64‑bit integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long CombineToLong(this (uint, uint) tuple) =>
			(long)tuple.CombineToULong();

		/// <summary>
		/// Combines a tuple of four <see cref="short"/> values into a signed 64‑bit integer.
		/// Each short is converted to an unsigned 16‑bit value before combining.
		/// </summary>
		/// <param name="tuple">A tuple containing four shorts.</param>
		/// <returns>A signed 64‑bit integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long CombineToLong(this (short, short, short, short) tuple) =>
			(long)tuple.CombineToULong();

		/// <summary>
		/// Combines a tuple of four <see cref="ushort"/> values into a signed 64‑bit integer.
		/// The first element becomes the upper 16 bits and the second the lower 16 bits.
		/// </summary>
		/// <param name="tuple">A tuple containing four ushorts.</param>
		/// <returns>A signed 64‑bit integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long CombineToLong(this (ushort, ushort, ushort, ushort) tuple) =>
			(long)tuple.CombineToULong();

		/// <summary>
		/// Combines a tuple of eight <see cref="byte"/> values into a signed 64‑bit integer.
		/// The tuple elements are arranged from the most significant (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="tuple">A tuple containing eight bytes.</param>
		/// <returns>A signed 64‑bit integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long CombineToLong(this (byte, byte, byte, byte, byte, byte, byte, byte) tuple) =>
			(long)tuple.CombineToULong();

		/// <summary>
		/// Combines a tuple of eight <see cref="sbyte"/> values into a signed 64‑bit integer.
		/// Each sbyte is cast to a byte before combining.
		/// The tuple elements are arranged from the most significant (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="tuple">A tuple containing eight sbytes.</param>
		/// <returns>A signed 64‑bit integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long CombineToLong(this (sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte) tuple) =>
			(long)tuple.CombineToULong();

		/// <summary>
		/// Splits a signed 64‑bit integer into a tuple of eight <see cref="byte"/> values.
		/// The resulting tuple contains bytes ordered from the most significant (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="value">The signed 64‑bit integer to split.</param>
		/// <returns>A tuple containing eight bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte, byte, byte, byte, byte, byte, byte, byte) SplitIntoByte(this long value) =>
			((ulong)value).SplitIntoByte();

		/// <summary>
		/// Splits a signed 64‑bit integer into a tuple of eight <see cref="sbyte"/> values.
		/// The resulting tuple contains sbytes ordered from the most significant (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="value">The signed 64‑bit integer to split.</param>
		/// <returns>A tuple containing eight sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte) SplitIntoSByte(this long value) =>
			((ulong)value).SplitIntoSByte();

		/// <summary>
		/// Splits a signed 64‑bit integer into a tuple of four <see cref="short"/> values.
		/// </summary>
		/// <param name="value">The signed 64‑bit integer to split.</param>
		/// <returns>A tuple containing four shorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (short, short, short, short) SplitIntoShort(this long value) =>
			((ulong)value).SplitIntoShort();

		/// <summary>
		/// Splits a signed 64‑bit integer into a tuple of four <see cref="ushort"/> values.
		/// </summary>
		/// <param name="value">The signed 64‑bit integer to split.</param>
		/// <returns>A tuple containing four ushorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (ushort, ushort, ushort, ushort) SplitIntoUShort(this long value) =>
			((ulong)value).SplitIntoUShort();

		/// <summary>
		/// Splits a signed 64‑bit integer into a tuple of two <see cref="int"/> values.
		/// The first element represents the upper 32 bits and the second the lower 32 bits.
		/// </summary>
		/// <param name="value">The signed 64‑bit integer to split.</param>
		/// <returns>A tuple containing two ints.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (int, int) SplitIntoInt(this long value) =>
			((ulong)value).SplitIntoInt();

		/// <summary>
		/// Splits a signed 64‑bit integer into a tuple of two <see cref="uint"/> values.
		/// The first element represents the upper 32 bits and the second the lower 32 bits.
		/// </summary>
		/// <param name="value">The signed 64‑bit integer to split.</param>
		/// <returns>A tuple containing two uints.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (uint, uint) SplitIntoUInt(this long value) =>
			((ulong)value).SplitIntoUInt();
	}
}