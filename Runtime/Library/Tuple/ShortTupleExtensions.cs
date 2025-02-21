#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

#if BITCORE_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif

namespace BitCore
{
	/// <summary>
	/// Provides high-performance extension methods for packing tuples into and unpacking 16-bit signed integers.
	/// <para>All operations use big-endian byte order (most significant byte first).</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds for minimal overhead.</para>
	/// </summary>
	public static class ShortTupleExtensions
	{
		/// <summary>
		/// Packs a tuple of two bytes into a 16-bit signed integer.
		/// </summary>
		/// <param name="bytes">A tuple containing two bytes (b1, b2).</param>
		/// <returns>A 16-bit signed integer with bytes packed as b1:b2 (big-endian).</returns>
		/// <remarks>Bits are arranged as [b1:15-8, b2:7-0]. Useful for data packing or serialization.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short PackToShort(this (byte b1, byte b2) bytes) =>
			(short)(bytes.b1 << 8 | bytes.b2);

		/// <summary>
		/// Packs a tuple of two signed bytes into a 16-bit signed integer.
		/// </summary>
		/// <param name="sbytes">A tuple containing two sbytes (sb1, sb2).</param>
		/// <returns>A 16-bit signed integer with sbytes packed as sb1:sb2 (big-endian, treated as unsigned bytes).</returns>
		/// <remarks>Each sbyte is cast to a byte, preserving its bit pattern (e.g., -1 becomes 255). Bits are [sb1:15-8, sb2:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static short PackToShort(this (sbyte sb1, sbyte sb2) sbytes) =>
			(short)((byte)sbytes.sb1 << 8 | (byte)sbytes.sb2);

		/// <summary>
		/// Unpacks a 16-bit signed integer into a tuple of two bytes.
		/// </summary>
		/// <param name="shortValue">The 16-bit signed integer value.</param>
		/// <returns>A tuple containing two bytes (b1, b2) in big-endian order.</returns>
		/// <remarks>Bytes are extracted as [b1:15-8, b2:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte b1, byte b2) UnpackToBytes(this short shortValue) =>
			((byte)(shortValue >> 8), (byte)shortValue);

		/// <summary>
		/// Unpacks a 16-bit signed integer into a tuple of two signed bytes.
		/// </summary>
		/// <param name="shortValue">The 16-bit signed integer value.</param>
		/// <returns>A tuple containing two sbytes (sb1, sb2) in big-endian order.</returns>
		/// <remarks>Bytes are interpreted as signed values (e.g., 255 becomes -1). Bits are [sb1:15-8, sb2:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte sb1, sbyte sb2) UnpackToSBytes(this short shortValue) =>
			((sbyte)(shortValue >> 8), (sbyte)shortValue);
	}
}