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
	/// Provides high-performance extension methods for the <see cref="int"/> type, optimized for bit manipulation and conversions.
	/// <para>An <see cref="int"/> is a signed 32-bit integer (-2,147,483,648 to 2,147,483,647).</para>
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/int">int keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds. Debug checks are included in development builds and stripped in release for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class IntExtensions
	{
		/// <summary>
		/// Converts the <see cref="int"/> value to a boolean.
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <returns>True if the value is greater than zero; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this int data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Gets the inverted bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this int data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return (~data >> pos) & 1;
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new int with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int SetBitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return data | (1 << pos);
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new int with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int ClearBitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return data & ~(1 << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new int with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int ToggleBitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return data ^ (1 << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new int with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int SetBitValueAt(this int data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return data & ~(1 << pos) | (bit << pos);
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the int (population count).
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns>The number of 1 bits (0 to 32).</returns>
		/// <remarks>Uses a parallel bit summation algorithm (Hamming Weight) for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this int value)
		{
			uint v = (uint)value;

			v = v - ((v >> 1) & 0x55555555);
			v = (v & 0x33333333) + ((v >> 2) & 0x33333333);

			return (int)(((v + (v >> 4)) & 0x0F0F0F0F) * 0x01010101 >> 24);
		}

		/// <summary>
		/// Determines if the int is a power of two (positive only).
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns>True if the value is a positive power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
		/// <remarks>Always returns false for negative values due to signed integer representation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this int value) => value > 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position within the int (big-endian order).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The byte position (0 to 3, where 0 is the most significant byte).</param>
		/// <returns>The byte at the specified position.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-3.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this int data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 3) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-3, was {pos}.");
#endif
			return (byte)(data >> (24 - (pos * 8)));
		}

		/// <summary>
		/// Sets the byte at the specified position within the int (big-endian order).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The byte position (0 to 3, where 0 is the most significant byte).</param>
		/// <returns>A new int with the byte replaced.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-3.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int SetByteAt(this int data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 3) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-3, was {pos}.");
#endif
			int shift = 24 - (pos * 8);
			int mask = 0xFF << shift;
			return (data & ~mask) | (newData << shift);
		}

		/// <summary>
		/// Returns the binary string representation of the int (32 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns>A 32-character string of bits, e.g., "00000000000000000000000000001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this int value)
		{
			char[] bits = new char[32];
			for (int i = 31; i >= 0; i--)
			{
				bits[31 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts a 32-character substring of binary digits starting at <paramref name="readIndex"/> into an int.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 32 characters).</param>
		/// <returns>The int value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int IntFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 32 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 32 ({readIndex + 32}) exceeds string length ({data.Length}).");
#endif
			int value = 0;
			for (int i = 0; i < 32; i++)
			{
				value = (value << 1) | (data[readIndex + i] - '0');
			}
			return value;
		}

		/// <summary>
		/// Converts the first 32 characters of a binary string into an int.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The int value from the first 32 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int IntFromBitString(this string data) => data.IntFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the int.
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns>A string like "FFFFFFFF" for -1 or "7FFFFFFF" for 2147483647.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this int value) => value.ToString("X");
	}
}