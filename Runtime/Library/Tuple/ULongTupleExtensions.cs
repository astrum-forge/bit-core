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
	/// Provides extension methods for combining and splitting 64‑bit unsigned integers using tuples.
	/// </summary>
	public static class ULongTupleExtensions
	{
		/// <summary>
		/// Combines a tuple of two <see cref="int"/> values into a 64‑bit unsigned integer.
		/// Each int is first cast to an unsigned 32‑bit integer, with the first element forming the upper 32 bits
		/// and the second element forming the lower 32 bits.
		/// </summary>
		/// <param name="tuple">A tuple containing two ints.</param>
		/// <returns>A 64‑bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong CombineToULong(this (int, int) tuple) =>
			((ulong)(uint)tuple.Item1 << 32) | (uint)tuple.Item2;

		/// <summary>
		/// Combines a tuple of two <see cref="uint"/> values into a 64‑bit unsigned integer.
		/// The first element forms the upper 32 bits and the second forms the lower 32 bits.
		/// </summary>
		/// <param name="tuple">A tuple containing two uints.</param>
		/// <returns>A 64‑bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong CombineToULong(this (uint, uint) tuple) =>
			((ulong)tuple.Item1 << 32) | tuple.Item2;

		/// <summary>
		/// Combines a tuple of four <see cref="short"/> values into a 64‑bit unsigned integer.
		/// Each short is cast to an unsigned 16‑bit integer before combining.
		/// The tuple elements are arranged so that the first element becomes the most significant 16 bits.
		/// </summary>
		/// <param name="tuple">A tuple containing four shorts.</param>
		/// <returns>A 64‑bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong CombineToULong(this (short, short, short, short) tuple) =>
			((ulong)(ushort)tuple.Item1 << 48) |
			((ulong)(ushort)tuple.Item2 << 32) |
			((ulong)(ushort)tuple.Item3 << 16) |
			(ushort)tuple.Item4;

		/// <summary>
		/// Combines a tuple of four <see cref="ushort"/> values into a 64‑bit unsigned integer.
		/// The first element forms the upper 16 bits of the upper 32 bits, and so on.
		/// </summary>
		/// <param name="tuple">A tuple containing four ushorts.</param>
		/// <returns>A 64‑bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong CombineToULong(this (ushort, ushort, ushort, ushort) tuple) =>
			((ulong)tuple.Item1 << 48) |
			((ulong)tuple.Item2 << 32) |
			((ulong)tuple.Item3 << 16) |
			tuple.Item4;

		/// <summary>
		/// Combines a tuple of eight <see cref="byte"/> values into a 64‑bit unsigned integer.
		/// The tuple elements are arranged in order from the most significant byte (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="tuple">A tuple containing eight bytes.</param>
		/// <returns>A 64‑bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong CombineToULong(this (byte, byte, byte, byte, byte, byte, byte, byte) tuple) =>
			((ulong)tuple.Item1 << 56) |
			((ulong)tuple.Item2 << 48) |
			((ulong)tuple.Item3 << 40) |
			((ulong)tuple.Item4 << 32) |
			((ulong)tuple.Item5 << 24) |
			((ulong)tuple.Item6 << 16) |
			((ulong)tuple.Item7 << 8) |
			tuple.Item8;

		/// <summary>
		/// Combines a tuple of eight <see cref="sbyte"/> values into a 64‑bit unsigned integer.
		/// Each sbyte is cast to a byte before combining.
		/// The elements are ordered from the most significant (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="tuple">A tuple containing eight sbytes.</param>
		/// <returns>A 64‑bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong CombineToULong(this (sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte) tuple) =>
			((ulong)(byte)tuple.Item1 << 56) |
			((ulong)(byte)tuple.Item2 << 48) |
			((ulong)(byte)tuple.Item3 << 40) |
			((ulong)(byte)tuple.Item4 << 32) |
			((ulong)(byte)tuple.Item5 << 24) |
			((ulong)(byte)tuple.Item6 << 16) |
			((ulong)(byte)tuple.Item7 << 8) |
			(byte)tuple.Item8;

		/// <summary>
		/// Splits a 64‑bit unsigned integer into a tuple of eight <see cref="byte"/> values.
		/// The resulting tuple contains bytes ordered from the most significant (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="value">The 64‑bit unsigned integer to split.</param>
		/// <returns>A tuple of eight bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte, byte, byte, byte, byte, byte, byte, byte) SplitIntoByte(this ulong value) =>
			(
				 (byte)(value >> 56),
				 (byte)(value >> 48),
				 (byte)(value >> 40),
				 (byte)(value >> 32),
				 (byte)(value >> 24),
				 (byte)(value >> 16),
				 (byte)(value >> 8),
				 (byte)value
			);

		/// <summary>
		/// Splits a 64‑bit unsigned integer into a tuple of eight <see cref="sbyte"/> values.
		/// The resulting tuple contains sbytes ordered from the most significant (Item1) to the least significant (Item8).
		/// </summary>
		/// <param name="value">The 64‑bit unsigned integer to split.</param>
		/// <returns>A tuple of eight sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte) SplitIntoSByte(this ulong value) =>
			(
				(sbyte)(value >> 56),
				(sbyte)(value >> 48),
				(sbyte)(value >> 40),
				(sbyte)(value >> 32),
				(sbyte)(value >> 24),
				(sbyte)(value >> 16),
				(sbyte)(value >> 8),
				(sbyte)value
			);

		/// <summary>
		/// Splits a 64‑bit unsigned integer into a tuple of four <see cref="short"/> values.
		/// The tuple elements are ordered from the most significant 16 bits (Item1) to the least (Item4).
		/// </summary>
		/// <param name="value">The 64‑bit unsigned integer to split.</param>
		/// <returns>A tuple of four shorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (short, short, short, short) SplitIntoShort(this ulong value) =>
			(
				(short)(value >> 48),
				(short)(value >> 32),
				(short)(value >> 16),
				(short)value
			);

		/// <summary>
		/// Splits a 64‑bit unsigned integer into a tuple of four <see cref="ushort"/> values.
		/// The resulting tuple contains ushorts ordered from the most significant (Item1) to the least significant (Item4).
		/// </summary>
		/// <param name="value">The 64‑bit unsigned integer to split.</param>
		/// <returns>A tuple of four ushorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (ushort, ushort, ushort, ushort) SplitIntoUShort(this ulong value) =>
			(
				(ushort)(value >> 48),
				(ushort)(value >> 32),
				(ushort)(value >> 16),
				(ushort)value
			);

		/// <summary>
		/// Splits a 64‑bit unsigned integer into a tuple of two <see cref="int"/> values.
		/// The first element represents the upper 32 bits, and the second represents the lower 32 bits.
		/// </summary>
		/// <param name="value">The 64‑bit unsigned integer to split.</param>
		/// <returns>A tuple of two ints.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (int, int) SplitIntoInt(this ulong value) =>
			(
				(int)(value >> 32),
				(int)value
			);

		/// <summary>
		/// Splits a 64‑bit unsigned integer into a tuple of two <see cref="uint"/> values.
		/// The first element represents the upper 32 bits, and the second represents the lower 32 bits.
		/// </summary>
		/// <param name="value">The 64‑bit unsigned integer to split.</param>
		/// <returns>A tuple of two uints.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (uint, uint) SplitIntoUInt(this ulong value) =>
			(
				(uint)(value >> 32),
				(uint)value
			);
	}
}