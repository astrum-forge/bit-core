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
	/// Provides high-performance extension methods for the <see cref="ulong"/> type, optimized for bit manipulation and conversions.
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ulong">ulong keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds. Debug checks are included in development builds and stripped in release for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class ULongExtensions
	{
		/// <summary>
		/// Converts the <see cref="ulong"/> value to a boolean.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <returns>True if the value is greater than zero; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this ulong data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong BitAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return (data >> pos) & 1ul;
		}

		/// <summary>
		/// Gets the inverted bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong BitInvAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return (~data >> pos) & 1ul;
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>A new ulong with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong SetBitAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return data | (1ul << pos);
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>A new ulong with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong ClearBitAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return data & ~(1ul << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <returns>A new ulong with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong ToggleBitAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
#endif
			return data ^ (1ul << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The bit position (0 to 63).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new ulong with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-63 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong SetBitValueAt(this ulong data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 63) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-63, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return data & ~(1ul << pos) | ((ulong)bit << pos);
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the ulong (population count).
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns>The number of 1 bits (0 to 64).</returns>
		/// <remarks>Uses a parallel bit summation algorithm (Hamming Weight) for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this ulong value)
		{
			ulong value0 = value - ((value >> 1) & 0x5555555555555555ul);
			ulong value1 = (value0 & 0x3333333333333333ul) + ((value0 >> 2) & 0x3333333333333333ul);
			ulong value2 = (value1 + (value1 >> 4)) & 0x0f0f0f0f0f0f0f0ful;

			return (int)((value2 * 0x0101010101010101ul) >> 56);
		}

		/// <summary>
		/// Determines if the ulong is a power of two.
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns>True if the value is a power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this ulong value) => value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position within the ulong (big-endian order).
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="pos">The byte position (0 to 7, where 0 is the most significant byte).</param>
		/// <returns>The byte at the specified position.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this ulong data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (byte)(data >> (56 - (pos * 8)));
		}

		/// <summary>
		/// Sets the byte at the specified position within the ulong (big-endian order).
		/// </summary>
		/// <param name="data">The ulong value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The byte position (0 to 7, where 0 is the most significant byte).</param>
		/// <returns>A new ulong with the byte replaced.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong SetByteAt(this ulong data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			int shift = 56 - (pos * 8);
			ulong mask = 0xFFul << shift;
			return (data & ~mask) | ((ulong)newData << shift);
		}

		/// <summary>
		/// Returns the binary string representation of the ulong (64 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns>A 64-character string of bits, e.g., "000...001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this ulong value)
		{
			char[] bits = new char[64];
			for (int i = 63; i >= 0; i--)
			{
				bits[63 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts a 64-character substring of binary digits starting at <paramref name="readIndex"/> into a ulong.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 64 characters).</param>
		/// <returns>The ulong value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong ULongFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 64 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 64 ({readIndex + 64}) exceeds string length ({data.Length}).");
#endif
			ulong value = 0;
			for (int i = 0; i < 64; i++)
			{
				value = (value << 1) | (ulong)(data[readIndex + i] - '0');
			}
			return value;
		}

		/// <summary>
		/// Converts the first 64 characters of a binary string into a ulong.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The ulong value from the first 64 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ulong ULongFromBitString(this string data) => data.ULongFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the ulong.
		/// </summary>
		/// <param name="value">The ulong value.</param>
		/// <returns>A string like "FFFFFFFFFFFFFFFF" for 18446744073709551615.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this ulong value) => value.ToString("X"); // Ensures 16 digits
	}
}