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
	/// Provides high-performance extension methods for the <see cref="sbyte"/> type, optimized for bit manipulation and conversions.
	/// <para>An <see cref="sbyte"/> is a signed 8-bit integer (-128 to 127).</para>
	/// <para>See also: <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sbyte">sbyte keyword</see>.</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds. Debug checks are included in development builds and stripped in release for maximum speed.</para>
	/// <para><b>Change History:</b>
	/// <list type="bullet">
	///   <item>20/12/2018: Added AggressiveInlining for .NET 4.6 targets.</item>
	/// </list></para>
	/// </summary>
	public static class SByteExtensions
	{
		/// <summary>
		/// Converts the <see cref="sbyte"/> value to a boolean.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <returns>True if the value is greater than zero; otherwise, false.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this sbyte data) => data > 0;

		/// <summary>
		/// Gets the bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>1 if the bit is set; 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte BitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (sbyte)((data >> pos) & 1);
		}

		/// <summary>
		/// Gets the inverted bit value (0 or 1) at the specified position.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>0 if the bit is set; 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte BitInvAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (sbyte)((~data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>A new sbyte with the bit set.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SetBitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (sbyte)((byte)data | (1 << pos));
		}

		/// <summary>
		/// Clears the bit at the specified position to 0.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>A new sbyte with the bit cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte ClearBitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (sbyte)(data & ~(1 << pos));
		}

		/// <summary>
		/// Toggles the bit at the specified position (1 to 0, or 0 to 1).
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <returns>A new sbyte with the bit toggled.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte ToggleBitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
#endif
			return (sbyte)(data ^ (1 << pos));
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0 to 7).</param>
		/// <param name="bit">The bit value (0 or 1).</param>
		/// <returns>A new sbyte with the bit set to the specified value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0-7 or bit is not 0/1.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SetBitValueAt(this sbyte data, int pos, int bit)
		{
#if BITCORE_DEBUG
			if (pos < 0 || pos > 7) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0-7, was {pos}.");
			if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException(nameof(bit), $"Bit value must be 0 or 1, was {bit}.");
#endif
			return (sbyte)(data & ~(1 << pos) | (bit << pos));
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the sbyte (population count).
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <returns>The number of 1 bits (0 to 8).</returns>
		/// <remarks>Uses a parallel bit summation algorithm for efficiency.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this sbyte data)
		{
			uint v = (uint)(byte)data;
			v = v - ((v >> 1) & 0x55);
			v = (v & 0x33) + ((v >> 2) & 0x33);
			return (int)((v + (v >> 4)) & 0x0F);
		}

		/// <summary>
		/// Determines if the sbyte is a power of two (positive only).
		/// </summary>
		/// <param name="value">The sbyte value.</param>
		/// <returns>True if the value is a positive power of 2 (1, 2, 4, 8, etc.); otherwise, false.</returns>
		/// <remarks>Always returns false for negative values due to signed integer representation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this sbyte value) => value > 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte representation of the sbyte value (position must be 0).
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The byte position (must be 0).</param>
		/// <returns>The byte representation of the sbyte.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0.</exception>
		/// <remarks>This method is trivial for an 8-bit type and may be deprecated in future versions.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
			if (pos != 0) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0, was {pos}.");
#endif
			return (byte)data;
		}

		/// <summary>
		/// Replaces the sbyte with a new byte value (position must be 0).
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The byte position (must be 0).</param>
		/// <returns>A new sbyte with the value replaced.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is not 0.</exception>
		/// <remarks>This method is trivial for an 8-bit type and may be deprecated in future versions.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SetByteAt(this sbyte data, byte newData, int pos)
		{
#if BITCORE_DEBUG
			if (pos != 0) throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be 0, was {pos}.");
#endif
			return (sbyte)newData;
		}

		/// <summary>
		/// Returns the binary string representation of the sbyte (8 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The sbyte value.</param>
		/// <returns>An 8-character string of bits, e.g., "00001111" for 15.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation. Negative values are represented in two's complement.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this sbyte value)
		{
			char[] bits = new char[8];
			for (int i = 7; i >= 0; i--)
			{
				bits[7 - i] = (char)('0' + ((value >> i) & 1));
			}
			return new string(bits);
		}

		/// <summary>
		/// Converts an 8-character substring of binary digits starting at <paramref name="readIndex"/> into an sbyte.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <param name="readIndex">The starting index (must allow 8 characters).</param>
		/// <returns>The sbyte value represented by the substring.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if readIndex is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SByteFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
			if (readIndex < 0 || readIndex + 8 > data.Length)
				throw new ArgumentOutOfRangeException(nameof(readIndex), $"readIndex + 8 ({readIndex + 8}) exceeds string length ({data.Length}).");
#endif
			sbyte value = 0;
			for (int i = 0; i < 8; i++)
			{
				value = (sbyte)((value << 1) | (data[readIndex + i] - '0'));
			}
			return value;
		}

		/// <summary>
		/// Converts the first 8 characters of a binary string into an sbyte.
		/// </summary>
		/// <param name="data">The string of '0' and '1' characters.</param>
		/// <returns>The sbyte value from the first 8 characters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if string is too short.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SByteFromBitString(this string data) => data.SByteFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the sbyte.
		/// </summary>
		/// <param name="value">The sbyte value.</param>
		/// <returns>A string like "FF" for -1 or "7F" for 127.</returns>
		/// <remarks>For performance-critical code, consider avoiding string allocation.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this sbyte value) => value.ToString("X");
	}
}