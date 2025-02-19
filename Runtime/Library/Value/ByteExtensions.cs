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
using System.Text;

namespace BitCore
{
	/// <summary>
	/// Contains useful extension methods for the <see cref="byte"/> data type.
	/// <para>
	/// For more information see: 
	/// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/byte">C# byte keyword</see>.
	/// </para>
	/// <para>
	/// PERFORMANCE NOTICE: In Unity Editor or Debug builds the code performs extra range checks.
	/// These checks are stripped out in production builds.
	/// </para>
	/// <para>
	/// CRITICAL CHANGES:
	/// <list type="bullet">
	///   <item>
	///     <description>20/12/2018 – For .NET 4.6 targets, methods are hinted for AggressiveInlining.</description>
	///   </item>
	/// </list>
	/// </para>
	/// </summary>
	public static class ByteExtensions
	{
		/// <summary>
		/// Converts the byte to a boolean value.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <returns><c>true</c> if the byte is greater than 0; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this byte data)
		{
			return data > 0;
		}

		/// <summary>
		/// Returns the state of the bit (either 1 or 0) at the specified position.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (must be between 0 and 7).</param>
		/// <returns>1 if the bit is set; otherwise, 0.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"byte.BitAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Returns the inverted state of the bit (either 1 or 0) at the specified position.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (must be between 0 and 7).</param>
		/// <returns>0 if the bit is set; otherwise, 1.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"byte.BitInvAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			return 1 - ((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (must be between 0 and 7).</param>
		/// <returns>A new byte with the bit at the specified position set.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte SetBitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"byte.SetBitAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			return (byte)(data | (1 << pos));
		}

		/// <summary>
		/// Clears the bit (sets to 0) at the specified position.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (must be between 0 and 7).</param>
		/// <returns>A new byte with the bit at the specified position cleared.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte UnsetBitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"byte.UnsetBitAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			return (byte)(data & ~(1 << pos));
		}

		/// <summary>
		/// Toggles the bit at the specified position.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (must be between 0 and 7).</param>
		/// <returns>A new byte with the bit at the specified position toggled.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ToggleBitAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"byte.ToggleBitAt(int) - position must be between 0 and 7 but was {pos}");
            }
#endif
			return (byte)(data ^ (1 << pos));
		}

		/// <summary>
		/// Sets the bit at the specified position to the given bit value.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The bit position (must be between 0 and 7).</param>
		/// <param name="bit">The bit value (must be either 0 or 1).</param>
		/// <returns>A new byte with the bit at the specified position set accordingly.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte SetBit(this byte data, int pos, int bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
            {
                BitDebug.Throw($"byte.SetBit(int, int) - position must be between 0 and 7 but was {pos}");
            }

            if (bit != 0 && bit != 1)
            {
                BitDebug.Throw($"byte.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
            }
#endif
			int mask = 1 << pos;
			int bitValue = (bit << pos) & mask;
			int clearedData = data & ~mask;
			return (byte)(clearedData | bitValue);
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the byte.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <returns>The population count (number of 1 bits).</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this byte data)
		{
			// Assumes that an extension method for uint.PopCount() is defined elsewhere.
			return ((uint)data).PopCount();
		}

		/// <summary>
		/// Determines whether the byte is a power of two.
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this byte value)
		{
			return value != 0 && (value & (value - 1)) == 0;
		}

		/// <summary>
		/// Returns the byte at the specified index. Since a byte contains 8 bits,
		/// the only valid index is 0.
		/// </summary>
		/// <param name="data">The byte value.</param>
		/// <param name="pos">The position index (must be 0).</param>
		/// <returns>The original byte value.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this byte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos != 0)
            {
                BitDebug.Throw($"byte.ByteAt(int) - position must be 0 but was {pos}");
            }
#endif
			return data;
		}

		/// <summary>
		/// Sets and returns the byte at the specified index. Since a byte contains 8 bits,
		/// the only valid index is 0.
		/// </summary>
		/// <param name="data">The current byte value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">The position index (must be 0).</param>
		/// <returns>The new byte value.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte SetByteAt(this byte data, byte newData, int pos)
		{
#if BITCORE_DEBUG
            if (pos != 0)
            {
                BitDebug.Throw($"byte.SetByteAt(int) - position must be 0 but was {pos}");
            }
#endif
			return newData;
		}

		/// <summary>
		/// Returns the binary string representation of the byte (8 characters of '0' or '1').
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns>An 8-character string representing the bits.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this byte value)
		{
			var sb = new StringBuilder(8);
			for (int i = 7; i >= 0; i--)
			{
				sb.Append(value.BitAt(i));
			}
			return sb.ToString();
		}

		/// <summary>
		/// Converts a substring of binary characters starting at <paramref name="readIndex"/> into a byte.
		/// </summary>
		/// <param name="data">The binary string (containing '0' and '1').</param>
		/// <param name="readIndex">The starting index from which to read 8 characters.</param>
		/// <returns>The byte represented by the 8 binary characters.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
            if (readIndex < 0 || readIndex + 8 > data.Length)
            {
                BitDebug.Throw("string.ByteFromBitString(int) - readIndex + 8 exceeds the string length.");
            }
#endif
			byte value = 0;
			for (int i = 0, bitPos = 7; i < 8; i++, readIndex++, bitPos--)
			{
				value = data[readIndex] == '1' ? value.SetBitAt(bitPos) : value.UnsetBitAt(bitPos);
			}
			return value;
		}

		/// <summary>
		/// Converts the first 8 characters of a binary string into a byte.
		/// </summary>
		/// <param name="data">The binary string (containing '0' and '1').</param>
		/// <returns>The byte represented by the first 8 binary characters.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteFromBitString(this string data)
		{
			return data.ByteFromBitString(0);
		}

		/// <summary>
		/// Returns the hexadecimal string representation of the byte.
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns>A string containing the hexadecimal representation.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this byte value)
		{
			return value.ToString("X");
		}

		/// <summary>
		/// Reverses the bit order of the byte.
		/// </summary>
		/// <param name="value">The byte value.</param>
		/// <returns>A new byte with the bits in reverse order.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ReverseBits(this byte value)
		{
			// Bit reversal algorithm
			return (byte)(((value * 0x80200802UL) & 0x0884422110UL) * 0x0101010101UL >> 32);
		}
	}
}
