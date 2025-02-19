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
	/// Provides extension methods for combining and splitting 32‐bit unsigned integers using tuples.
	/// </summary>
	public static class UIntTupleExtensions
	{
		/// <summary>
		/// Combines a tuple of four bytes into a single 32‐bit unsigned integer.
		/// </summary>
		/// <param name="tuple">A tuple containing four bytes.</param>
		/// <returns>A 32‐bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint CombineToUInt(this (byte, byte, byte, byte) tuple) =>
			(uint)tuple.Item1 << 24 |
			(uint)tuple.Item2 << 16 |
			(uint)tuple.Item3 << 8 |
			tuple.Item4;

		/// <summary>
		/// Combines a tuple of four signed bytes into a single 32‐bit unsigned integer.
		/// </summary>
		/// <param name="tuple">A tuple containing four sbytes.</param>
		/// <returns>A 32‐bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint CombineToUInt(this (sbyte, sbyte, sbyte, sbyte) tuple) =>
			(uint)(byte)tuple.Item1 << 24 |
			(uint)(byte)tuple.Item2 << 16 |
			(uint)(byte)tuple.Item3 << 8 |
			(byte)tuple.Item4;

		/// <summary>
		/// Combines a tuple of two signed shorts into a single 32‐bit unsigned integer.
		/// </summary>
		/// <param name="tuple">A tuple containing two shorts.</param>
		/// <returns>A 32‐bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint CombineToUInt(this (short, short) tuple) =>
			(uint)(ushort)tuple.Item1 << 16 |
			(uint)(ushort)tuple.Item2;

		/// <summary>
		/// Combines a tuple of two unsigned shorts into a single 32‐bit unsigned integer.
		/// </summary>
		/// <param name="tuple">A tuple containing two ushorts.</param>
		/// <returns>A 32‐bit unsigned integer.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint CombineToUInt(this (ushort, ushort) tuple) =>
			(uint)tuple.Item1 << 16 |
			tuple.Item2;

		/// <summary>
		/// Splits a 32‐bit unsigned integer into a tuple of four bytes.
		/// </summary>
		/// <param name="value">The unsigned integer value.</param>
		/// <returns>A tuple containing four bytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte, byte, byte, byte) SplitIntoByte(this uint value) =>
			((byte)(value >> 24),
			 (byte)(value >> 16),
			 (byte)(value >> 8),
			 (byte)value);

		/// <summary>
		/// Splits a 32‐bit unsigned integer into a tuple of four signed bytes.
		/// </summary>
		/// <param name="value">The unsigned integer value.</param>
		/// <returns>A tuple containing four sbytes.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte, sbyte, sbyte, sbyte) SplitIntoSByte(this uint value) =>
			((sbyte)(value >> 24),
			 (sbyte)(value >> 16),
			 (sbyte)(value >> 8),
			 (sbyte)value);

		/// <summary>
		/// Splits a 32‐bit unsigned integer into a tuple of two shorts.
		/// </summary>
		/// <param name="value">The unsigned integer value.</param>
		/// <returns>A tuple containing two shorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (short, short) SplitIntoShort(this uint value) =>
			((short)(value >> 16),
			 (short)value);

		/// <summary>
		/// Splits a 32‐bit unsigned integer into a tuple of two unsigned shorts.
		/// </summary>
		/// <param name="value">The unsigned integer value.</param>
		/// <returns>A tuple containing two ushorts.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (ushort, ushort) SplitIntoUShort(this uint value) =>
			((ushort)(value >> 16),
			 (ushort)value);
	}
}
