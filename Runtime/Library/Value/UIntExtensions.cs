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
	/// Provides high-performance extension methods for the <see cref="uint"/> type, optimized for bit manipulation.
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/uint">uint keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds. Debug checks are included in development builds and stripped in release for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class UIntExtensions
	{
		/// <summary>
		/// Converts the <see cref="uint"/> value to a boolean.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <returns>True if the value is greater than zero; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this uint data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint BitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return (data >> pos) & 1u;
		}

		/// <summary>
		/// Gets the inverted bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint BitInvAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return (~data >> pos) & 1u;
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new uint with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint SetBitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return data | (1u << pos);
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new uint with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint ClearBitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return data & ~(1u << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new uint with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint ToggleBitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
#endif
			return data ^ (1u << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new uint with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-31 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint SetBitValueAt(this uint data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 31) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-31, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return data & ~(1u << pos) | ((uint)bit << pos);
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the uint (population count).
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <returns>The number of 1 bits (0 to 32).</returns>
		/// <remarks>Uses a parallel bit summation algorithm for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this uint data)
		{
			uint temp = data - ((data >> 1) & 0x55555555);
			uint temp2 = (temp & 0x33333333) + ((temp >> 2) & 0x33333333);
			return (int)((((temp2 + (temp2 >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
		}

		/// <summary>
		/// Determines if the uint is a power of two.
		/// </summary>
		/// <param name="value">The uint value.</param>
		/// <returns>True if the value is a power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this uint value) => value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position within the uint (big-endian order).
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The byte position (0 to 3, where 0 is the most significant byte).</param>
		/// <returns>The byte at the specified position.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-3.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 3) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-3, was {pos}.");
#endif
			return (byte)(data >> (24 - (pos * 8)));
		}

		/// <summary>
		/// Sets the byte at the specified position within the uint (big-endian order).
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The byte position (0 to 3, where 0 is the most significant byte).</param>
		/// <returns>A new uint with the byte replaced.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-3.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint SetByteAt(this uint data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 3) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-3, was {pos}.");
#endif
			int shift = 24 - (pos * 8);
			uint mask = 0xFFu << shift;
			return (data & ~mask) | ((uint)newData << shift);
		}

		/// <summary>
		/// Returns the binary string representation of the uint (32 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The uint value.</param>
		/// <returns>A 32-character string of bits, e.g., "00000000000000000000000000001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this uint value)
		{
			char[] bits = new char[32];
			for (int i = 31; i >= 0; i--)
			{
				bits[31 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts a 32-character substring of binary digits starting at <paramref name="readIndex"/> into a uint.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 32 characters).</param>
		/// <returns>The uint value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint UIntFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 32 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 32 ({readIndex + 32}) exceeds string length ({data.Length}).");
#endif
			uint value = 0;
			for (int i = 0; i < 32; i++)
			{
				value = (value << 1) | (uint)(data[readIndex + i] - '0');
			}
			return value;
		}

		/// <summary>
		/// Converts the first 32 characters of a binary string into a uint.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The uint value from the first 32 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint UIntFromBitString(this string data) => data.UIntFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the uint.
		/// </summary>
		/// <param name="value">The uint value.</param>
		/// <returns>A string like "FFFFFFFF" for 4294967295.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this uint value) => value.ToString("X");
	}
}