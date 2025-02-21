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
	/// Provides high-performance extension methods for the <see cref="byte"/> type, optimized for bit manipulation.
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/byte">C# byte keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined where applicable in .NET 4.6+. Debug builds include range checks, stripped in release builds for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class ByteExtensions
	{
		/// <summary>
		/// Converts the byte to a boolean value.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <returns>True if the byte is greater than 0; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this byte data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Gets the inverted bit value (1 or 0) at the specified position.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (~data >> pos) & 1;
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>A new byte with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte SetBitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (byte)(data | (1 << pos));
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>A new byte with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ClearBitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (byte)(data & ~(1 << pos));
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>A new byte with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ToggleBitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (byte)(data ^ (1 << pos));
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new byte with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte SetBitValueAt(this byte data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return (byte)(data & ~(1 << pos) | (bit << pos));
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the byte (population count).
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <returns>The number of 1 bits (0 to 8).</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this byte data)
		{
			// Fast bit counting using parallel bit summation
			uint v = data;
			v = v - ((v >> 1) & 0x55);
			v = (v & 0x33) + ((v >> 2) & 0x33);
			return (int)((v + (v >> 4)) & 0xF);
		}

		/// <summary>
		/// Determines if the byte is a power of two.
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns>True if the value is a power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this byte value) => value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Returns the byte value at the specified index (always 0 for a single byte).
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The index (must be 0).</param>
		/// <returns>The original byte value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0.</exception>
		/// <remarks>This method is trivial for a single byte and may be deprecated in future versions.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos != 0) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0, was {pos}.");
#endif
			return data;
		}

		/// <summary>
		/// Replaces the byte value at the specified index (always 0 for a single byte).
		/// </summary>
		/// <param name="data">The current byte value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The index (must be 0).</param>
		/// <returns>The new byte value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0.</exception>
		/// <remarks>This method is trivial for a single byte and may be deprecated in future versions.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte SetByteAt(this byte data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos != 0) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0, was {pos}.");
#endif
			return newData;
		}

		/// <summary>
		/// Returns the binary string representation of the byte (8 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns>An 8-character string of bits, e.g., "00001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this byte value)
		{
			// Pre-allocated array for performance over StringBuilder
			char[] bits = new char[8];
			for (int i = 7; i >= 0; i--)
			{
				bits[7 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts an 8-character substring of binary digits starting at <paramref name="readIndex"/> into a byte.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 8 characters).</param>
		/// <returns>The byte value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 8 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 8 ({readIndex + 8}) exceeds string length ({data.Length}).");
#endif
			byte value = 0;
			for (int i = 0; i < 8; i++)
			{
				value = (byte)(value << 1 | (data[readIndex + i] - '0'));
			}
			return value;
		}

		/// <summary>
		/// Converts the first 8 characters of a binary string into a byte.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The byte value from the first 8 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteFromBitString(this string data) => data.ByteFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the byte.
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns>A string like "FF" for 255.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this byte value)
		{
			// Faster hex conversion without allocation could be added if needed
			return value.ToString("X2"); // Ensures 2 digits
		}

		/// <summary>
		/// Reverses the bit order of the byte (e.g., 0b1100 becomes 0b0011).
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns>A new byte with bits reversed.</returns>
		/// <remarks>Uses a multiplicative bit reversal algorithm for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ReverseBits(this byte value)
		{
			return (byte)(((value * 0x80200802UL) & 0x0884422110UL) * 0x0101010101UL >> 32);
		}
	}
}