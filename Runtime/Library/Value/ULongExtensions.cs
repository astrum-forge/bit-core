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
	/// Provides extension methods for the <see cref="ulong"/> type.
	/// <para>
	/// These methods offer bit-level operations, string conversions, and bit counting on a 64‑bit unsigned value.
	/// </para>
	/// <para>
	/// Performance Note: In editor or debug builds, error checks are enabled; these are removed in production.
	/// </para>
	/// <para>
	/// Critical Changes: 20/12/2018 – Functions are hinted for aggressive inlining on .NET 4.6 targets.
	/// </para>
	/// </summary>
	public static class ULongExtensions
	{
		/// <summary>
		/// Returns <c>true</c> if the <see cref="ulong"/> value is greater than zero.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <returns><c>true</c> if the value is greater than zero; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this ulong data) => data > 0;

		/// <summary>
		/// Retrieves the bit (0 or 1) at the specified position in the ulong value.
		/// Position must be between 0 (least significant) and 63 (most significant).
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>The bit value (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"ulong.BitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return (int)((data >> pos) & 1);
		}

		/// <summary>
		/// Returns the inverted bit (1 becomes 0 and 0 becomes 1) at the specified position.
		/// Position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>The inverted bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"ulong.BitInvAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return 1 - (int)((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// Position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>A new ulong with the specified bit set.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong SetBitAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"ulong.SetBitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return data | (1ul << pos);
		}

		/// <summary>
		/// Clears (sets to 0) the bit at the specified position.
		/// Position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>A new ulong with the specified bit cleared.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong UnsetBitAt(this ulong data, int pos)
		{
#if BITCORE_METHOD_INLINE
            // Check in debug mode.
#endif
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"ulong.UnsetBitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return data & ~(1ul << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position.
		/// Position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <returns>A new ulong with the specified bit toggled.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong ToggleBitAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"ulong.ToggleBitAt(int) - position must be between 0 and 63 but was {pos}");
            }
#endif
			return data ^ (1ul << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position to the given bit value (0 or 1).
		/// Position must be between 0 and 63.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0–63).</param>
		/// <param name="bit">The bit value (0 or 1) to set.</param>
		/// <returns>A new ulong with the specified bit updated.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong SetBit(this ulong data, int pos, long bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 63)
            {
                BitDebug.Throw($"ulong.SetBit(int, int) - position must be between 0 and 63 but was {pos}");
            }
            if (bit != 0 && bit != 1)
            {
                BitDebug.Throw($"ulong.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
            }
#endif
			ulong mask = 1ul << pos;
			ulong m1 = ((ulong)bit << pos) & mask;
			ulong m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Returns the number of bits set to 1 in the ulong value.
		/// Uses a general purpose Hamming Weight algorithm.
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns>The population count (number of set bits).</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this ulong value)
		{
			ulong value0 = value - ((value >> 1) & 0x5555555555555555);
			ulong value1 = (value0 & 0x3333333333333333) + ((value0 >> 2) & 0x3333333333333333);
			ulong value2 = (value1 + (value1 >> 4)) & 0x0f0f0f0f0f0f0f0f;
			return (int)((value2 * 0x0101010101010101) >> 56);
		}

		/// <summary>
		/// Determines whether the ulong value is a power of two.
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this ulong value) =>
			value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position from the ulong value.
		/// The position must be between 0 (most significant byte) and 7 (least significant byte).
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The byte position (0–7).</param>
		/// <returns>The byte at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"ulong.ByteAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			return (byte)(data >> (56 - (pos * 8)));
		}

		/// <summary>
		/// Replaces the byte at the specified position in the ulong value with a new byte.
		/// The position must be between 0 (most significant byte) and 7 (least significant byte).
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="newData">The new byte to set.</param>
		/// <param name="pos">The byte position (0–7).</param>
		/// <returns>A new ulong with the specified byte replaced.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong SetByteAt(this ulong data, byte newData, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"ulong.SetByteAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			int shift = 56 - (pos * 8);
			ulong mask = 0xFFul << shift;
			ulong m1 = ((ulong)newData << shift) & mask;
			ulong m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Returns a 64-character string representing the binary form of the ulong value.
		/// Each character is '0' or '1', corresponding to each bit (from bit 63 to bit 0).
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns>A 64-character binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this ulong value)
		{
			var sb = new StringBuilder(64);
			for (int i = 63; i >= 0; i--)
			{
				sb.Append(value.BitAt(i));
			}
			return sb.ToString();
		}

		/// <summary>
		/// Converts 64 characters from a binary string (starting at the specified index) into a ulong.
		/// The method reads 64 characters from the string to form the value.
		/// </summary>
		/// <param name="data">A binary string representing bits.</param>
		/// <param name="readIndex">The starting index in the string.</param>
		/// <returns>A ulong corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong ULongFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
            if ((readIndex + 64) > data.Length)
            {
                BitDebug.Throw("string.ULongFromBitString(int) - read index and ulong length exceed string length");
            }
#endif
			ulong value = 0;
			for (int i = readIndex, j = 63; i < readIndex + 64; i++, j--)
			{
				value = data[i] == '1' ? value.SetBitAt(j) : value.UnsetBitAt(j);
			}
			return value;
		}

		/// <summary>
		/// Converts the first 64 characters of a binary string into a ulong.
		/// </summary>
		/// <param name="data">A binary string representing 64 bits.</param>
		/// <returns>A ulong corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong ULongFromBitString(this string data) =>
			data.ULongFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the ulong value.
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns>A hexadecimal string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this ulong value) => value.ToString("X");
	}
}