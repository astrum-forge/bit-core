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
	/// Provides extension methods for the <see cref="ushort"/> type.
	/// A <see cref="ushort"/> is an unsigned 16‑bit integer.
	/// <para>
	/// In debug or editor builds, error checks are enabled; these are removed in production.
	/// </para>
	/// <para>
	/// Critical Change (20/12/2018): For .NET 4.6 targets, functions are hinted for aggressive inlining.
	/// </para>
	/// </summary>
	public static class UShortExtensions
	{
		/// <summary>
		/// Returns <c>true</c> if the <see cref="ushort"/> value is greater than zero.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <returns><c>true</c> if the value is greater than zero; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this ushort data) => data > 0;

		/// <summary>
		/// Retrieves the bit (0 or 1) at the specified position in the ushort value.
		/// The position must be between 0 (least significant) and 15 (most significant).
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>The bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"ushort.BitAt(int) - position must be between 0 and 15 but was {pos}");
            }
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Returns the inverted bit at the specified position.
		/// For a given position, if the bit is 1, it returns 0; if 0, it returns 1.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>The inverted bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"ushort.BitInvAt(int) - position must be between 0 and 15 but was {pos}");
            }
#endif
			return 1 - ((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>A new ushort with the bit at the specified position set to 1.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort SetBitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"ushort.SetBitAt(int) - position must be between 0 and 15 but was {pos}");
            }
#endif
			return (ushort)(data | (1u << pos));
		}

		/// <summary>
		/// Clears (sets to 0) the bit at the specified position.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>A new ushort with the specified bit cleared.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort UnsetBitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"ushort.UnsetBitAt(int) - position must be between 0 and 15 but was {pos}");
            }
#endif
			return (ushort)(data & ~(1u << pos));
		}

		/// <summary>
		/// Toggles the bit at the specified position.
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <returns>A new ushort with the specified bit toggled.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort ToggleBitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"ushort.ToggleBitAt(int) - position must be between 0 and 15 but was {pos}");
            }
#endif
			return (ushort)(data ^ (1u << pos));
		}

		/// <summary>
		/// Sets the bit at the specified position to the provided value (0 or 1).
		/// The position must be between 0 and 15.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0–15).</param>
		/// <param name="bit">The bit value to set (0 or 1).</param>
		/// <returns>A new ushort with the specified bit updated.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort SetBit(this ushort data, int pos, int bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 15)
            {
                BitDebug.Throw($"ushort.SetBit(int, int) - position must be between 0 and 15 but was {pos}");
            }
            if (bit != 0 && bit != 1)
            {
                BitDebug.Throw($"ushort.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
            }
#endif
			int mask = 1 << pos;
			int m1 = (bit << pos) & mask;
			int m2 = data & ~mask;
			return (ushort)(m2 | m1);
		}

		/// <summary>
		/// Returns the number of bits set to 1 in the ushort value.
		/// Uses a general-purpose Hamming Weight (popcount) algorithm.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <returns>The population count (number of set bits).</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this ushort data) =>
			((uint)data).PopCount();

		/// <summary>
		/// Determines whether the ushort value is a power of two.
		/// </summary>
		/// <param name="value">The ushort value.</param>
		/// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this ushort value) =>
			value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position from the ushort value.
		/// Position 0 returns the high byte, and 1 returns the low byte.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The byte position (0–1).</param>
		/// <returns>The byte at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 1)
            {
                BitDebug.Throw($"ushort.ByteAt(int) - position must be between 0 and 1 but was {pos}");
            }
#endif
			return (byte)(data >> (8 - (pos * 8)));
		}

		/// <summary>
		/// Replaces the byte at the specified position in the ushort value with a new byte.
		/// Position 0 replaces the high byte, and 1 replaces the low byte.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="newData">The new byte to set.</param>
		/// <param name="pos">The byte position (0–1).</param>
		/// <returns>A new ushort with the specified byte replaced.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort SetByteAt(this ushort data, byte newData, int pos)
		{
#if BITCORE_METHOD_INLINE
            // (Using BITCORE_DEBUG check)
#endif
#if BITCORE_DEBUG
            if (pos < 0 || pos > 1)
            {
                BitDebug.Throw($"ushort.SetByteAt(int) - position must be between 0 and 1 but was {pos}");
            }
#endif
			int shift = 8 - (pos * 8);
			int mask = 0xFF << shift;
			int m1 = (newData << shift) & mask;
			int m2 = data & ~mask;
			return (ushort)(m2 | m1);
		}

		/// <summary>
		/// Returns a 16-character string representing the binary form of the ushort value.
		/// The string is composed of '0' and '1' characters for bits 15 (MSB) to 0 (LSB).
		/// </summary>
		/// <param name="value">The ushort value.</param>
		/// <returns>A 16-character binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this ushort value)
		{
			var sb = new StringBuilder(16);
			for (int i = 15; i >= 0; i--)
			{
				sb.Append(value.BitAt(i));
			}
			return sb.ToString();
		}

		/// <summary>
		/// Converts 16 characters from a binary string, starting at the specified index, into a ushort value.
		/// </summary>
		/// <param name="data">A binary string representing bits.</param>
		/// <param name="readIndex">The starting index from which to read 16 characters.</param>
		/// <returns>A ushort corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort UShortFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
            if ((readIndex + 16) > data.Length)
            {
                BitDebug.Throw("string.UShortFromBitString(int) - read index and ushort length exceed string length");
            }
#endif
			ushort value = 0;
			for (int i = readIndex, j = 15; i < readIndex + 16; i++, j--)
			{
				value = data[i] == '1' ? value.SetBitAt(j) : value.UnsetBitAt(j);
			}
			return value;
		}

		/// <summary>
		/// Converts the first 16 characters of a binary string into a ushort value.
		/// </summary>
		/// <param name="data">A binary string representing 16 bits.</param>
		/// <returns>A ushort corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort UShortFromBitString(this string data) =>
			data.UShortFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the ushort value.
		/// </summary>
		/// <param name="value">The ushort value.</param>
		/// <returns>A hexadecimal string representation.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this ushort value) => value.ToString("X");
	}
}