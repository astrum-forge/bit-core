#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

using System;
#if BITCORE_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif
using System.Text;

namespace BitCore
{
	/// <summary>
	/// Provides extension methods for the <see cref="sbyte"/> type.
	/// <para>sbyte is a signed 8‑bit integer.</para>
	/// <para>
	/// In debug builds, error checks are enabled; these are removed in production.
	/// For .NET 4.6 targets, methods are hinted for aggressive inlining.
	/// </para>
	/// </summary>
	public static class SByteExtensions
	{
		/// <summary>
		/// Returns <c>true</c> if the sbyte value is greater than zero.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <returns><c>true</c> if greater than zero; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool Bool(this sbyte data) => data > 0;

		/// <summary>
		/// Retrieves the bit (0 or 1) at the specified position.
		/// The position must be between 0 and 7.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0–7).</param>
		/// <returns>The bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
                BitDebug.Throw($"sbyte.BitAt(int) - position must be between 0 and 7 but was {pos}");
#endif
			return (data >> pos) & 1;
		}

		/// <summary>
		/// Returns the inverted bit at the specified position.
		/// If the bit is 1, returns 0; if 0, returns 1.
		/// The position must be between 0 and 7.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0–7).</param>
		/// <returns>The inverted bit (0 or 1) at the specified position.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int BitInvAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
                BitDebug.Throw($"sbyte.BitInvAt(int) - position must be between 0 and 7 but was {pos}");
#endif
			return 1 - ((data >> pos) & 1);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// The position must be between 0 and 7.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0–7).</param>
		/// <returns>A new sbyte with the specified bit set to 1.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SetBitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
                BitDebug.Throw($"sbyte.SetBitAt(int) - position must be between 0 and 7 but was {pos}");
#endif
			return (sbyte)(((byte)data) | (1 << pos));
		}

		/// <summary>
		/// Clears (sets to 0) the bit at the specified position.
		/// The position must be between 0 and 7.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0–7).</param>
		/// <returns>A new sbyte with the specified bit cleared.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte UnsetBitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
                BitDebug.Throw($"sbyte.UnsetBitAt(int) - position must be between 0 and 7 but was {pos}");
#endif
			return (sbyte)(data & ~(1 << pos));
		}

		/// <summary>
		/// Toggles the bit at the specified position.
		/// The position must be between 0 and 7.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0–7).</param>
		/// <returns>A new sbyte with the specified bit toggled.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte ToggleBitAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
                BitDebug.Throw($"sbyte.ToggleBitAt(int) - position must be between 0 and 7 but was {pos}");
#endif
			return (sbyte)(data ^ (1 << pos));
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// The position must be between 0 and 7.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The bit position (0–7).</param>
		/// <param name="bit">The bit value to set (0 or 1).</param>
		/// <returns>A new sbyte with the specified bit updated.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SetBit(this sbyte data, int pos, int bit)
		{
#if BITCORE_DEBUG
            if (pos < 0 || pos > 7)
                BitDebug.Throw($"sbyte.SetBit(int, int) - position must be between 0 and 7 but was {pos}");
            if (bit != 0 && bit != 1)
                BitDebug.Throw($"sbyte.SetBit(int, int) - bit value must be either 0 or 1 but was {bit}");
#endif
			int mask = 1 << pos;
			int m1 = (bit << pos) & mask;
			int m2 = data & ~mask;
			return (sbyte)(m2 | m1);
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the sbyte value.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <returns>The number of set bits.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PopCount(this sbyte data) => ((byte)data).PopCount();

		/// <summary>
		/// Determines whether the sbyte value is a power of two.
		/// </summary>
		/// <param name="value">The sbyte value.</param>
		/// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsPowerOfTwo(this sbyte value) =>
			value != 0 && (value & (value - 1)) == 0;

		/// <summary>
		/// Retrieves the byte corresponding to the sbyte value.
		/// For sbyte, the only valid index is 0.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="pos">The byte position, must be 0.</param>
		/// <returns>The byte representation of the sbyte.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static byte ByteAt(this sbyte data, int pos)
		{
#if BITCORE_DEBUG
            if (pos != 0)
                BitDebug.Throw($"sbyte.ByteAt(int) - position must be 0 but was {pos}");
#endif
			return (byte)data;
		}

		/// <summary>
		/// Replaces the sbyte with a new byte value.
		/// For sbyte, the only valid index is 0.
		/// </summary>
		/// <param name="data">The sbyte value.</param>
		/// <param name="newData">The new byte to set.</param>
		/// <param name="pos">The byte position, must be 0.</param>
		/// <returns>A new sbyte with the value replaced.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SetByteAt(this sbyte data, byte newData, int pos)
		{
#if BITCORE_DEBUG
            if (pos != 0)
                BitDebug.Throw($"sbyte.SetByteAt(int) - position must be 0 but was {pos}");
#endif
			return (sbyte)newData;
		}

		/// <summary>
		/// Returns an 8-character string representing the binary form of the sbyte value.
		/// The string is constructed from bit 7 (most significant) to bit 0 (least significant).
		/// </summary>
		/// <param name="value">The sbyte value.</param>
		/// <returns>An 8-character binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string BitString(this sbyte value) =>
			((byte)value).BitString();

		/// <summary>
		/// Converts 8 characters from a binary string, starting at the specified index, into an sbyte.
		/// </summary>
		/// <param name="data">A binary string representing 8 bits.</param>
		/// <param name="readIndex">The starting index in the string.</param>
		/// <returns>An sbyte corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SByteFromBitString(this string data, int readIndex) =>
			(sbyte)data.ByteFromBitString(readIndex);

		/// <summary>
		/// Converts the first 8 characters of a binary string into an sbyte.
		/// </summary>
		/// <param name="data">A binary string representing 8 bits.</param>
		/// <returns>An sbyte corresponding to the binary string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static sbyte SByteFromBitString(this string data) =>
			data.SByteFromBitString(0);

		/// <summary>
		/// Returns the hexadecimal string representation of the sbyte value.
		/// </summary>
		/// <param name="value">The sbyte value.</param>
		/// <returns>A hexadecimal string.</returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static string HexString(this sbyte value) => value.ToString("X");
	}
}