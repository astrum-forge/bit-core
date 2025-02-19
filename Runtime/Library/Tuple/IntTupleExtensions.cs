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
	/// Provides extension methods for converting between <see cref="int"/> values and tuples.
	/// These methods leverage existing unsigned tuple conversions.
	/// </summary>
	public static class IntTupleExtensions
	{
		/// <summary>
		/// Combines a tuple of four <see cref="byte"/> values into a single <see cref="int"/>.
		/// Each byte forms part of the unsigned 32‑bit representation, which is then cast to int.
		/// </summary>
		/// <param name="tuple">A tuple containing four bytes.</param>
		/// <returns>An <see cref="int"/> formed from the tuple.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int CombineToInt(this (byte, byte, byte, byte) tuple) =>
			(int)tuple.CombineToUInt();

		/// <summary>
		/// Combines a tuple of four <see cref="sbyte"/> values into a single <see cref="int"/>.
		/// Each sbyte is cast to a byte before forming the unsigned 32‑bit value which is then cast to int.
		/// </summary>
		/// <param name="tuple">A tuple containing four sbytes.</param>
		/// <returns>An <see cref="int"/> formed from the tuple.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int CombineToInt(this (sbyte, sbyte, sbyte, sbyte) tuple) =>
			(int)tuple.CombineToUInt();

		/// <summary>
		/// Combines a tuple of two <see cref="short"/> values into a single <see cref="int"/>.
		/// Each short is converted to an unsigned 16‑bit value before combining.
		/// </summary>
		/// <param name="tuple">A tuple containing two shorts.</param>
		/// <returns>An <see cref="int"/> formed from the tuple.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int CombineToInt(this (short, short) tuple) =>
			(int)tuple.CombineToUInt();

		/// <summary>
		/// Combines a tuple of two <see cref="ushort"/> values into a single <see cref="int"/>.
		/// The first element forms the upper 16 bits and the second the lower 16 bits, then cast to int.
		/// </summary>
		/// <param name="tuple">A tuple containing two ushorts.</param>
		/// <returns>An <see cref="int"/> formed from the tuple.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int CombineToInt(this (ushort, ushort) tuple) =>
			(int)tuple.CombineToUInt();

		/// <summary>
		/// Splits a 32‑bit signed integer into a tuple of four <see cref="byte"/> values.
		/// The int is cast to unsigned, then split into its constituent bytes.
		/// </summary>
		/// <param name="value">The int value to split.</param>
		/// <returns>A tuple containing four bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte, byte, byte, byte) SplitIntoByte(this int value) =>
			((uint)value).SplitIntoByte();

		/// <summary>
		/// Splits a 32‑bit signed integer into a tuple of four <see cref="sbyte"/> values.
		/// The int is cast to unsigned, then split into its constituent sbytes.
		/// </summary>
		/// <param name="value">The int value to split.</param>
		/// <returns>A tuple containing four sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte, sbyte, sbyte, sbyte) SplitIntoSByte(this int value) =>
			((uint)value).SplitIntoSByte();

		/// <summary>
		/// Splits a 32‑bit signed integer into a tuple of two <see cref="short"/> values.
		/// The int is cast to unsigned, then split into two shorts.
		/// </summary>
		/// <param name="value">The int value to split.</param>
		/// <returns>A tuple containing two shorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (short, short) SplitIntoShort(this int value) =>
			((uint)value).SplitIntoShort();

		/// <summary>
		/// Splits a 32‑bit signed integer into a tuple of two <see cref="ushort"/> values.
		/// The int is cast to unsigned, then split into two ushorts.
		/// </summary>
		/// <param name="value">The int value to split.</param>
		/// <returns>A tuple containing two ushorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (ushort, ushort) SplitIntoUShort(this int value) =>
			((uint)value).SplitIntoUShort();
	}
}