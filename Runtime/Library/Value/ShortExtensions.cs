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
using System.Text;

namespace BitCore
{
	/// <summary>
	/// Provides extension methods for the <see cref="short"/> type.
	/// A <see cref="short"/> is a signed 16‑bit integer.
	/// <para>
	/// In debug builds, error checks are enabled; these checks are omitted in production.
	/// </para>
	/// <para>
	/// Critical Change: For .NET 4.6 targets, methods are hinted for aggressive inlining.
	/// </para>
	/// </summary>
	public static class ShortExtensions
	{
		/// <summary>
		/// Returns <c>true</c> if the short value is greater than zero.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <returns><c>true</c> if the value is greater than zero; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this short data) => data > 0;

		/// <summary>
		/// Retrieves the bit (0 or 1) at the specified position in the short value.
		/// The position must be between 0 (least significant) and 15 (most significant).
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>The bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this short data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"short.BitAt(int) - position must be between 0 and 15 but was {pos}");
            }
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Returns the inverted bit at the specified position.
		/// If the bit is 1, returns 0; if 0, returns 1.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>The inverted bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this short data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"short.BitInvAt(int) - position must be between 0 and 15 but was {pos}");
            }
#endif
			return 1 - ((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>A new short with the bit at the specified position set to 1.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short SetBitAt(this short data, int pos) =>
			(short)(((ushort)data) | (1u << pos));

		/// <summary>
		/// Clears (sets to 0) the bit at the specified position.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>A new short with the specified bit cleared.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short UnsetBitAt(this short data, int pos) =>
			(short)(data & ~(1 << pos));

		/// <summary>
		/// Toggles the bit at the specified position.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>A new short with the specified bit toggled.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short ToggleBitAt(this short data, int pos) =>
			(short)(data ^ (1 << pos));

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <param name="bit">The bit value (0 or 1) to set.</param>
		/// <returns>A new short with the specified bit updated.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short SetBit(this short data, int pos, int bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"short.SetBit(int, int) - position must be between 0 and 15 but was {pos}");
            }
            if (bit != 0 && bit != 1)
            {
                BitDebug.Throw($"short.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
            }
#endif
			int mask = 1 << pos;
			int m1 = (bit << pos) & mask;
			int m2 = data & ~mask;
			return (short)(m2 | m1);
		}

		/// <summary>
		/// Returns the number of bits set to 1 in the short value.
		/// Uses a Hamming Weight (popcount) algorithm on the corresponding ushort.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <returns>The population count (number of set bits).</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this short data) =>
			((ushort)data).PopCount();

		/// <summary>
		/// Determines whether the short value is a power of two.
		/// </summary>
		/// <param name="value">The short value.</param>
		/// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this short value) =>
			value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position from the short value.
		/// Position 0 returns the high byte, and position 1 returns the low byte.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The byte position (0–1).</param>
		/// <returns>The byte at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this short data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 1)
            {
                BitDebug.Throw($"short.ByteAt(int) - position must be between 0 and 1 but was {pos}");
            }
#endif
			return (byte)(data >> (8 - (pos * 8)));
		}

		/// <summary>
		/// Replaces the byte at the specified position in the short value with a new byte.
		/// Position 0 replaces the high byte, and position 1 replaces the low byte.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="newData">The new byte to set.</param>
		/// <param name="pos">The byte position (0–1).</param>
		/// <returns>A new short with the specified byte replaced.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short SetByteAt(this short data, byte newData, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 1)
            {
                BitDebug.Throw($"short.SetByteAt(int) - position must be between 0 and 1 but was {pos}");
            }
#endif
			int shift = 8 - (pos * 8);
			int mask = 0xFF << shift;
			int m1 = (newData << shift) & mask;
			int m2 = data & ~mask;
			return (short)(m2 | m1);
		}

		/// <summary>
		/// Returns a 16-character string representing the binary form of the short value.
		/// The string represents bits 15 (most significant) to 0 (least significant).
		/// </summary>
		/// <param name="value">The short value.</param>
		/// <returns>A 16-character binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this short value) =>
			((ushort)value).BitString();

		/// <summary>
		/// Converts 16 characters from a binary string, starting at the specified index, into a short value.
		/// </summary>
		/// <param name="data">A binary string representing bits.</param>
		/// <param name="readIndex">The starting index from which to read 16 characters.</param>
		/// <returns>A short corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short ShortFromBitString(this string data, int readIndex) =>
			(short)data.UShortFromBitString(readIndex);

		/// <summary>
		/// Converts the first 16 characters of a binary string into a short value.
		/// </summary>
		/// <param name="data">A binary string representing 16 bits.</param>
		/// <returns>A short corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short ShortFromBitString(this string data) =>
			data.ShortFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the short value.
		/// </summary>
		/// <param name="value">The short value.</param>
		/// <returns>A hexadecimal string representation.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this short value) => value.ToString("X");
	}
}