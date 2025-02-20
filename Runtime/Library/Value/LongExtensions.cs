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
	/// Provides extension methods for the <see cref="long"/> type.
	/// A long is a signed 64‑bit integer.
	/// <para>
	/// In editor or debug builds, error checks are enabled; these checks are removed in production.
	/// </para>
	/// <para>
	/// Critical Change: For .NET 4.6 targets, functions are hinted for aggressive inlining.
	/// </para>
	/// </summary>
	public static class LongExtensions
	{
		/// <summary>
		/// Returns <c>true</c> if the <see cref="long"/> value is greater than zero.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <returns><c>true</c> if the value is greater than zero; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this long data) => data > 0;

		/// <summary>
		/// Retrieves the bit (0 or 1) at the specified position.
		/// The position must be between 0 (least significant) and 63 (most significant).
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>The bit value (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"long.BitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return (int)((data >> pos) & 1);
		}

		/// <summary>
		/// Returns the inverted bit at the specified position.
		/// For a given position, if the bit is 1 it returns 0, and if 0 it returns 1.
		/// The position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>The inverted bit value (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this long data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"long.BitInvAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return 1 - (int)((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// The position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>A new long with the bit at the specified position set to 1.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long SetBitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"long.SetBitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return data | (1L << pos);
		}

		/// <summary>
		/// Clears (sets to 0) the bit at the specified position.
		/// The position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>A new long with the specified bit cleared.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long UnsetBitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"long.UnsetBitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return data & ~(1L << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position.
		/// The position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>A new long with the specified bit toggled.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long ToggleBitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"long.ToggleBitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return data ^ (1L << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position to the provided value (0 or 1).
		/// The position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <param name="bit">The bit value to set (0 or 1).</param>
		/// <returns>A new long with the specified bit updated.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long SetBit(this long data, int pos, long bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"long.SetBit(int, int) - position must be between 0 and 63 but was {pos}");
            }
            if (bit != 0 && bit != 1)
            {
                BitDebug.Throw($"long.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
            }
#endif
			long mask = 1L << pos;
			long m1 = (bit << pos) & mask;
			long m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Returns the number of bits set to 1 in the long value.
		/// Uses a Hamming Weight (popcount) algorithm.
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>The number of set bits.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this long value) =>
			((ulong)value).PopCount();

		/// <summary>
		/// Determines if the long value is a power of two.
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this long value) =>
			value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position from the long value.
		/// The long is treated as a 64‑bit number, with position 0 being the most significant byte and 7 the least.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The byte position (0–7).</param>
		/// <returns>The byte at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this long data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"long.ByteAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			return (byte)(data >> (56 - (pos * 8)));
		}

		/// <summary>
		/// Replaces the byte at the specified position in the long value with a new byte.
		/// The position must be between 0 (most significant) and 7 (least significant).
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="newData">The new byte value to set.</param>
		/// <param name="pos">The byte position (0–7).</param>
		/// <returns>A new long with the specified byte replaced.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long SetByteAt(this long data, byte newData, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"long.SetByteAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			int shift = 56 - (pos * 8);
			long mask = 0xFFL << shift;
			long m1 = ((long)newData << shift) & mask;
			long m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Returns a 64-character string representing the binary form of the long value.
		/// The string is constructed from bit 63 (MSB) to bit 0 (LSB).
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>A 64-character binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this long value)
		{
			var sb = new StringBuilder(64);
			for (int i = 63; i >= 0; i--)
			{
				sb.Append(value.BitAt(i));
			}
			return sb.ToString();
		}

		/// <summary>
		/// Converts 64 characters of a binary string, starting at the specified index, into a long value.
		/// </summary>
		/// <param name="data">A binary string representing bits.</param>
		/// <param name="readIndex">The starting index from which to read 64 characters.</param>
		/// <returns>A long corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long LongFromBitString(this string data, int readIndex) =>
			(long)data.ULongFromBitString(readIndex);

		/// <summary>
		/// Converts the first 64 characters of a binary string into a long value.
		/// </summary>
		/// <param name="data">A binary string representing 64 bits.</param>
		/// <returns>A long corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long LongFromBitString(this string data) => data.LongFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the long value.
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>A hexadecimal string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this long value) => value.ToString("X");
	}
}