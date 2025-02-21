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
	/// Provides high-performance extension methods for the <see cref="ushort"/> type, optimized for bit manipulation and conversions.
	/// <para>A <see cref="ushort"/> is an unsigned 16-bit integer (0 to 65535).</para>
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ushort">ushort keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds. Debug checks are included in development builds and stripped in release for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class UShortExtensions
	{
		/// <summary>
		/// Converts the <see cref="ushort"/> value to a boolean.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <returns>True if the value is greater than zero; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this ushort data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort BitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (ushort)((data >> pos) & 1);
		}

		/// <summary>
		/// Gets the inverted bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort BitInvAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (ushort)((~data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>A new ushort with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort SetBitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (ushort)(data | (1 << pos));
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>A new ushort with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort ClearBitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (ushort)(data & ~(1 << pos));
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <returns>A new ushort with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort ToggleBitAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
#endif
			return (ushort)(data ^ (1 << pos));
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The bit position (0 to 15).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new ushort with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-15 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort SetBitValueAt(this ushort data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 15) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-15, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return (ushort)(data & ~(1 << pos) | (bit << pos));
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the ushort (population count).
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <returns>The number of 1 bits (0 to 16).</returns>
		/// <remarks>Uses a parallel bit summation algorithm (Hamming Weight) for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this ushort data)
		{
			return ((uint)data).PopCount();
		}

		/// <summary>
		/// Determines if the ushort is a power of two.
		/// </summary>
		/// <param name="value">The ushort value.</param>
		/// <returns>True if the value is a power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this ushort value) => value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position within the ushort (big-endian order).
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="pos">The byte position (0 or 1, where 0 is the most significant byte).</param>
		/// <returns>The byte at the specified position.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this ushort data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 1) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-1, was {pos}.");
#endif
			return (byte)(data >> (8 - (pos * 8)));
		}

		/// <summary>
		/// Sets the byte at the specified position within the ushort (big-endian order).
		/// </summary>
		/// <param name="data">The ushort value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The byte position (0 or 1, where 0 is the most significant byte).</param>
		/// <returns>A new ushort with the byte replaced.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort SetByteAt(this ushort data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 1) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-1, was {pos}.");
#endif
			int shift = 8 - (pos * 8);
			ushort mask = (ushort)(0xFF << shift);
			return (ushort)((data & ~mask) | (newData << shift));
		}

		/// <summary>
		/// Returns the binary string representation of the ushort (16 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The ushort value.</param>
		/// <returns>A 16-character string of bits, e.g., "0000000000001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this ushort value)
		{
			char[] bits = new char[16];
			for (int i = 15; i >= 0; i--)
			{
				bits[15 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts a 16-character substring of binary digits starting at <paramref name="readIndex"/> into a ushort.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 16 characters).</param>
		/// <returns>The ushort value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort UShortFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 16 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 16 ({readIndex + 16}) exceeds string length ({data.Length}).");
#endif
			ushort value = 0;
			for (int i = 0; i < 16; i++)
			{
				value = (ushort)((value << 1) | (data[readIndex + i] - '0'));
			}
			return value;
		}

		/// <summary>
		/// Converts the first 16 characters of a binary string into a ushort.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The ushort value from the first 16 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static ushort UShortFromBitString(this string data) => data.UShortFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the ushort.
		/// </summary>
		/// <param name="value">The ushort value.</param>
		/// <returns>A string like "FFFF" for 65535.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this ushort value) => value.ToString("X");
	}
}