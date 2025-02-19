#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

#if BITCORE_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif

using System.Text;

namespace BitCore
{
	/// <summary>
	/// Provides extension methods for the <see cref="uint"/> data type.
	/// <para>
	/// For more information, see:
	/// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/uint">uint keyword</see>.
	/// </para>
	/// <para>
	/// PERFORMANCE NOTICE:
	/// UNITY_EDITOR or DEBUG flags ensure that common errors are caught.
	/// These flags are removed in production mode, so don't rely on try/catch methods.
	/// If performing benchmarks, ensure that the flags are not taken into account.
	/// </para>
	/// <para>
	/// CRITICAL CHANGES:
	/// 20/12/2018 – For .NET 4.6 targets, all functions are hinted for AggressiveInlining.
	/// </para>
	/// </summary>
	public static class UIntExtensions
	{
		/// <summary>
		/// Converts the <see cref="uint"/> value to a boolean.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <returns><c>true</c> if <paramref name="data"/> is greater than zero; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this uint data) => data > 0;

		/// <summary>
		/// Gets the state of the bit (either 1 or 0) at the specified position.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (must be between 0 and 31).</param>
		/// <returns>The value of the bit at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"uint.BitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return (int)((data >> pos) & 1);
		}

		/// <summary>
		/// Gets the inverted state of the bit (either 1 or 0) at the specified position.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (must be between 0 and 31).</param>
		/// <returns>The inverted bit value.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"uint.BitInvAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return 1 - (int)((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (must be between 0 and 31).</param>
		/// <returns>A new uint with the bit at the specified position set to 1.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint SetBitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"uint.SetBitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return data | (1u << pos);
		}

		/// <summary>
		/// Clears the bit (sets to 0) at the specified position.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (must be between 0 and 31).</param>
		/// <returns>A new uint with the bit at the specified position cleared.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint UnsetBitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"uint.UnsetBitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return data & ~(1u << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (must be between 0 and 31).</param>
		/// <returns>A new uint with the bit at the specified position toggled.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint ToggleBitAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"uint.ToggleBitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return data ^ (1u << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">The bit position (must be between 0 and 31).</param>
		/// <param name="bit">The bit value (must be either 0 or 1).</param>
		/// <returns>A new uint with the specified bit set accordingly.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint SetBit(this uint data, int pos, int bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"uint.SetBit(int, int) - position must be between 0 and 31 but was {pos}");
            }
            if (bit != 0 && bit != 1)
            {
                BitDebug.Throw($"uint.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
            }
#endif
			uint mask = 1u << pos;
			uint m1 = ((uint)bit << pos) & mask;
			uint m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Counts the number of set bits (population count) in the uint.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <returns>The number of bits set to 1.</returns>
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
		/// Determines whether the specified uint is a power of two.
		/// </summary>
		/// <param name="value">The uint value.</param>
		/// <returns><c>true</c> if <paramref name="value"/> is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this uint value)
		{
			return value != 0 && (value & (value - 1)) == 0;
		}

		/// <summary>
		/// Retrieves the byte at the specified position within the uint.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="pos">
		/// The byte position (must be between 0 and 3), where 0 corresponds to the most significant byte.
		/// </param>
		/// <returns>The byte at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this uint data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 3)
            {
                BitDebug.Throw($"uint.ByteAt(int) - position must be between 0 and 3 but was {pos}");
            }
#endif
			int shift = 24 - (pos * 8);
			return (byte)(data >> shift);
		}

		/// <summary>
		/// Sets the byte at the specified position within the uint.
		/// </summary>
		/// <param name="data">The uint value.</param>
		/// <param name="newData">The new byte value.</param>
		/// <param name="pos">
		/// The byte position (must be between 0 and 3), where 0 corresponds to the most significant byte.
		/// </param>
		/// <returns>A new uint with the specified byte replaced.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint SetByteAt(this uint data, byte newData, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 3)
            {
                BitDebug.Throw($"uint.SetByteAt(int) - position must be between 0 and 3 but was {pos}");
            }
#endif
			int shift = 24 - (pos * 8);
			uint mask = 0xFFu << shift;
			uint m1 = ((uint)newData << shift) & mask;
			uint m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Returns the binary string representation of the uint (32 characters of 1s and 0s).
		/// </summary>
		/// <param name="value">The uint value.</param>
		/// <returns>A string representing the binary sequence.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this uint value)
		{
			var sb = new StringBuilder(32);
			for (int i = 31; i >= 0; i--)
			{
				sb.Append(value.BitAt(i));
			}
			return sb.ToString();
		}

		/// <summary>
		/// Converts a substring of binary characters into a uint.
		/// </summary>
		/// <param name="data">A string containing '0' and '1'.</param>
		/// <param name="readIndex">
		/// The starting index from which to read 32 characters.
		/// </param>
		/// <returns>The uint value represented by the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint UIntFromBitString(this string data, int readIndex)
		{
#if BITCORE_DEBUG
            if (readIndex < 0 || readIndex + 32 > data.Length)
            {
                BitDebug.Throw("string.UIntFromBitString(int) - read index and uint length exceed the string length");
            }
#endif
			uint value = 0;
			for (int i = readIndex, j = 31; i < readIndex + 32; i++, j--)
			{
				value = data[i] == '1' ? value.SetBitAt(j) : value.UnsetBitAt(j);
			}
			return value;
		}

		/// <summary>
		/// Converts the first 32 characters of a binary string into a uint.
		/// </summary>
		/// <param name="data">A string containing '0' and '1'.</param>
		/// <returns>The uint value represented by the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static uint UIntFromBitString(this string data)
		{
			return data.UIntFromBitString(0);
		}

		/// <summary>
		/// Returns the hexadecimal string representation of the uint.
		/// </summary>
		/// <param name="value">The uint value.</param>
		/// <returns>A string representing the hexadecimal value.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this uint value) => value.ToString("X");
	}
}
