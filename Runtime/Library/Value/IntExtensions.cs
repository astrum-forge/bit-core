#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

using System;

namespace BitCore
{
	/// <summary>
	/// Provides extension methods for the <see cref="int"/> value type.
	/// The int is a signed 32‑bit integer.
	/// <para>
	/// Performance Note: In the Unity Editor or debug builds, error checks are enabled;
	/// these checks are removed in production builds.
	/// </para>
	/// <para>
	/// Critical Changes: 20/12/2018 – For .NET 4.6 targets, functions are hinted to use AggressiveInlining.
	/// </para>
	/// </summary>
	public static class IntExtensions
	{
		/// <summary>
		/// Returns <c>true</c> if the int value is greater than zero.
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <returns><c>true</c> if the value is greater than zero; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this int data) => data > 0;

		/// <summary>
		/// Retrieves the bit value (0 or 1) at the specified bit position [0, 31].
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 = least significant, 31 = most significant).</param>
		/// <returns>The bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"int.BitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Retrieves the inverted bit (1 becomes 0 and 0 becomes 1) at the specified position [0, 31].
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>The inverted bit value (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this int data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"int.BitInvAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return 1 - ((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position [0, 31] to 1.
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new int with the specified bit set to 1.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int SetBitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"int.SetBitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return data | (1 << pos);
		}

		/// <summary>
		/// Clears the bit at the specified position [0, 31] (sets it to 0).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new int with the specified bit cleared.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int UnsetBitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"int.UnsetBitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return data & ~(1 << pos);
		}

		/// <summary>
		/// Toggles the bit at the specified position [0, 31].
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <returns>A new int with the specified bit toggled.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int ToggleBitAt(this int data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"int.ToggleBitAt(int) - position must be between 0 and 31 but was {pos}");
            }
#endif
			return data ^ (1 << pos);
		}

		/// <summary>
		/// Sets the bit at the specified position [0, 31] to the given bit value (0 or 1).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The bit position (0 to 31).</param>
		/// <param name="bit">The bit value (0 or 1) to set.</param>
		/// <returns>A new int with the specified bit updated.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int SetBit(this int data, int pos, int bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 31)
            {
                BitDebug.Throw($"int.SetBit(int, int) - position must be between 0 and 31 but was {pos}");
            }
            if (bit != 0 && bit != 1)
            {
                BitDebug.Throw($"int.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
            }
#endif
			int mask = 1 << pos;
			int m1 = (bit << pos) & mask;
			int m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the int value (population count).
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns>The number of bits set to 1.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this int value) =>
			((uint)value).PopCount();

		/// <summary>
		/// Determines whether the int value is a power of two.
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this int value) =>
			value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte at the specified position from the int value.
		/// The int is treated as a 32‑bit number, and the position must be between 0 (most significant byte)
		/// and 3 (least significant byte).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="pos">The byte position (0 to 3).</param>
		/// <returns>The byte at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this int data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 3)
            {
                BitDebug.Throw($"int.ByteAt(int) - position must be between 0 and 3 but was {pos}");
            }
#endif
			return (byte)(data >> (24 - (pos * 8)));
		}

		/// <summary>
		/// Replaces the byte at the specified position in the int value with a new byte value.
		/// The position must be between 0 (most significant byte) and 3 (least significant byte).
		/// </summary>
		/// <param name="data">The int value.</param>
		/// <param name="newData">The new byte to insert.</param>
		/// <param name="pos">The byte position (0 to 3).</param>
		/// <returns>A new int with the specified byte replaced.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int SetByteAt(this int data, byte newData, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 3)
            {
                BitDebug.Throw($"int.SetByteAt(int) - position must be between 0 and 3 but was {pos}");
            }
#endif
			int shift = 24 - (pos * 8);
			int mask = 0xFF << shift;
			int m1 = (newData << shift) & mask;
			int m2 = data & ~mask;
			return m2 | m1;
		}

		/// <summary>
		/// Returns a 32-character string representing the binary sequence of the int value.
		/// Each character is either '0' or '1', corresponding to each bit.
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns>A binary string of 32 characters.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this int value) =>
			((uint)value).BitString();

		/// <summary>
		/// Converts a binary string starting at the specified index into an int.
		/// The method reads 32 characters from the string to form the int value.
		/// </summary>
		/// <param name="data">A binary string representing 32 bits.</param>
		/// <param name="readIndex">The starting index in the string.</param>
		/// <returns>An int corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int IntFromBitString(this string data, int readIndex) =>
			(int)data.UIntFromBitString(readIndex);

		/// <summary>
		/// Converts the first 32 characters of a binary string into an int.
		/// </summary>
		/// <param name="data">A binary string representing 32 bits.</param>
		/// <returns>An int corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int IntFromBitString(this string data) => data.IntFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the int value.
		/// </summary>
		/// <param name="value">The int value.</param>
		/// <returns>A hexadecimal string representation.</returns>
#if BITCORE_METHOD_INLINE
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this int value) => value.ToString("X");
	}
}