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
	/// Provides high-performance extension methods for the <see cref="short"/> type, optimized for bit manipulation and conversions.
	/// <para>A <see cref="short"/> is a signed 16-bit integer (-32,768 to 32,767).</para>
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/short">short keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds. Debug checks are included in development builds and stripped in release for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class ShortExtensions
	{
		/// <summary>
		/// Converts the <see cref="short"/> value to a boolean.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <returns>True if the value is greater than zero; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this short data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short BitAt(this short data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (short)((data >> pos) & 1);
		}

		/// <summary>
		/// Gets the inverted bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short BitInvAt(this short data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (short)((~data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>A new short with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short SetBitAt(this short data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (short)((ushort)data | (1 << pos));
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>A new short with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short ClearBitAt(this short data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (short)(data & ~(1 << pos));
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>A new short with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short ToggleBitAt(this short data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (short)(data ^ (1 << pos));
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new short with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short SetBitValueAt(this short data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return (short)(data & ~(1 << pos) | (bit << pos));
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the short (population count).
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <returns>The number of 1 bits (0 to 16).</returns>
		/// <remarks>Uses a parallel bit summation algorithm (Hamming Weight) for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this short data)
		{
			ushort v = (ushort)data; // Cast to ushort to avoid sign extension issues
			v = (ushort)(v - ((v >> 1) & 0x5555)); // Count pairs
			v = (ushort)((v & 0x3333) + ((v >> 2) & 0x3333)); // Count nibbles
			v = (ushort)((v + (v >> 4)) & 0x0F0F); // Sum 8-bit halves
			return (v + (v >> 8)) & 0x0F; // Sum the two 8-bit counts
		}

		/// <summary>
		/// Determines if the short is a power of two (positive only).
		/// </summary>
		/// <param name="value">The short value.</param>
		/// <returns>True if the value is a positive power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
		/// <remarks>Always returns false for negative values due to signed integer representation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this short value) => value > 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position within the short (big-endian order).
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="pos">The byte position (0 or 1, where 0 is the most significant byte).</param>
		/// <returns>The byte at the specified position.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this short data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 1) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-1, was {pos}.");
#endif
			return (byte)(data >> (8 - (pos * 8)));
		}

		/// <summary>
		/// Sets the byte at the specified position within the short (big-endian order).
		/// </summary>
		/// <param name="data">The short value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The byte position (0 or 1, where 0 is the most significant byte).</param>
		/// <returns>A new short with the byte replaced.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short SetByteAt(this short data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 1) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-1, was {pos}.");
#endif
			int shift = 8 - (pos * 8);
			short mask = (short)(0xFF << shift);
			return (short)((data & ~mask) | (newData << shift));
		}

		/// <summary>
		/// Returns the binary string representation of the short (16 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The short value.</param>
		/// <returns>A 16-character string of bits, e.g., "0000000000001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation. Negative values are represented in two's complement.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this short value)
		{
			char[] bits = new char[16];
			for (int i = 15; i >= 0; i--)
			{
				bits[15 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts a 16-character substring of binary digits starting at <paramref name="readIndex"/> into a short.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 16 characters).</param>
		/// <returns>The short value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short ShortFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 16 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 16 ({readIndex + 16}) exceeds string length ({data.Length}).");
#endif
			short value = 0;
			for (int i = 0; i < 16; i++)
			{
				value = (short)((value << 1) | (data[readIndex + i] - '0'));
			}
			return value;
		}

		/// <summary>
		/// Converts the first 16 characters of a binary string into a short.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The short value from the first 16 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short ShortFromBitString(this string data) => data.ShortFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the short.
		/// </summary>
		/// <param name="value">The short value.</param>
		/// <returns>A string like "FFFF" for -1 or "7FFF" for 32767.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this short value) => value.ToString("X");
	}
}