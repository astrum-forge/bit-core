#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

#if BITCORE_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif

using System;

namespace BitCore
{
	/// <summary>
	/// Provides high-performance extension methods for the <see cref="long"/> type, optimized for bit manipulation and conversions.
	/// <para>A <see cref="long"/> is a signed 64-bit integer (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807).</para>
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/long">long keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds. Debug checks are included in development builds and stripped in release for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class LongExtensions
	{
		/// <summary>
		/// Converts the <see cref="long"/> value to a boolean.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <returns>True if the value is greater than zero; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this long data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long BitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Gets the inverted bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long BitInvAt(this long data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return (~data >> pos) & 1;
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>A new long with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long SetBitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return data | (1L << pos);
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>A new long with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long ClearBitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return data & ~(1L << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>A new long with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long ToggleBitAt(this long data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return data ^ (1L << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new long with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long SetBitValueAt(this long data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return data & ~(1L << pos) | ((long)bit << pos);
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the long (population count).
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>The number of 1 bits (0 to 64).</returns>
		/// <remarks>Uses a parallel bit summation algorithm (Hamming Weight) for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this long value)
		{
			ulong v = (ulong)value;
			v = v - ((v >> 1) & 0x5555555555555555ul);
			v = (v & 0x3333333333333333ul) + ((v >> 2) & 0x3333333333333333ul);
			v = (v + (v >> 4)) & 0x0f0f0f0f0f0f0f0ful;
			return (int)((v * 0x0101010101010101ul) >> 56);
		}

		/// <summary>
		/// Determines if the long is a power of two (positive only).
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>True if the value is a positive power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
		/// <remarks>Always returns false for negative values due to signed integer representation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this long value) => value > 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position within the long (big-endian order).
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="pos">The byte position (0 to 7, where 0 is the most significant byte).</param>
		/// <returns>The byte at the specified position.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this long data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (byte)(data >> (56 - (pos * 8)));
		}

		/// <summary>
		/// Sets the byte at the specified position within the long (big-endian order).
		/// </summary>
		/// <param name="data">The long value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The byte position (0 to 7, where 0 is the most significant byte).</param>
		/// <returns>A new long with the byte replaced.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long SetByteAt(this long data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			int shift = 56 - (pos * 8);
			long mask = 0xFFL << shift;
			return (data & ~mask) | ((long)newData << shift);
		}

		/// <summary>
		/// Returns the binary string representation of the long (64 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>A 64-character string of bits, e.g., "000...001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this long value)
		{
			char[] bits = new char[64];
			for (int i = 63; i >= 0; i--)
			{
				bits[63 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts a 64-character substring of binary digits starting at <paramref name="readIndex"/> into a long.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 64 characters).</param>
		/// <returns>The long value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long LongFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 64 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 64 ({readIndex + 64}) exceeds string length ({data.Length}).");
#endif
			long value = 0;
			for (int i = 0; i < 64; i++)
			{
				value = (value << 1) | (data[readIndex + i] - '0');
			}
			return value;
		}

		/// <summary>
		/// Converts the first 64 characters of a binary string into a long.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The long value from the first 64 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long LongFromBitString(this string data) => data.LongFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the long.
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>A string like "FFFFFFFFFFFFFFFF" for -1 or "7FFFFFFFFFFFFFFF" for 9223372036854775807.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this long value) => value.ToString("X");
	}
}