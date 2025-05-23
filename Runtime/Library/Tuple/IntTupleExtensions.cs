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
	/// Provides high-performance extension methods for packing tuples into and unpacking 32-bit signed integers.
	/// <para>All operations use big-endian byte order (most significant byte first).</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds for minimal overhead.</para>
	/// </summary>
	public static class IntTupleExtensions
	{
		/// <summary>
		/// Packs a tuple of four bytes into a 32-bit signed integer.
		/// </summary>
		/// <param name="bytes">A tuple containing four bytes (b1, b2, b3, b4).</param>
		/// <returns>A 32-bit signed integer with bytes packed as b1:b2:b3:b4 (big-endian).</returns>
		/// <remarks>Bits are arranged as [b1:31-24, b2:23-16, b3:15-8, b4:7-0]. Useful for data packing or serialization.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PackToInt(this (byte b1, byte b2, byte b3, byte b4) bytes) =>
			(int)((uint)bytes.b1 << 24 | (uint)bytes.b2 << 16 | (uint)bytes.b3 << 8 | bytes.b4);

		/// <summary>
		/// Packs a tuple of four signed bytes into a 32-bit signed integer.
		/// </summary>
		/// <param name="sbytes">A tuple containing four sbytes (sb1, sb2, sb3, sb4).</param>
		/// <returns>A 32-bit signed integer with sbytes packed as sb1:sb2:sb3:sb4 (big-endian, treated as unsigned bytes).</returns>
		/// <remarks>Each sbyte is cast to a byte, preserving its bit pattern (e.g., -1 becomes 255). Bits are [sb1:31-24, sb2:23-16, sb3:15-8, sb4:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PackToInt(this (sbyte sb1, sbyte sb2, sbyte sb3, sbyte sb4) sbytes) =>
			(int)((uint)(byte)sbytes.sb1 << 24 | (uint)(byte)sbytes.sb2 << 16 | (uint)(byte)sbytes.sb3 << 8 | (byte)sbytes.sb4);

		/// <summary>
		/// Packs a tuple of two signed shorts into a 32-bit signed integer.
		/// </summary>
		/// <param name="shorts">A tuple containing two shorts (s1, s2).</param>
		/// <returns>A 32-bit signed integer with shorts packed as s1:s2 (big-endian, treated as unsigned).</returns>
		/// <remarks>Each short is cast to a ushort, preserving its bit pattern (e.g., -1 becomes 65535). Bits are [s1:31-16, s2:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PackToInt(this (short s1, short s2) shorts) =>
			(int)((uint)(ushort)shorts.s1 << 16 | (uint)(ushort)shorts.s2);

		/// <summary>
		/// Packs a tuple of two unsigned shorts into a 32-bit signed integer.
		/// </summary>
		/// <param name="ushorts">A tuple containing two ushorts (us1, us2).</param>
		/// <returns>A 32-bit signed integer with ushorts packed as us1:us2 (big-endian).</returns>
		/// <remarks>Bits are arranged as [us1:31-16, us2:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int PackToInt(this (ushort us1, ushort us2) ushorts) =>
			(int)((uint)ushorts.us1 << 16 | ushorts.us2);

		/// <summary>
		/// Unpacks a 32-bit signed integer into a tuple of four bytes.
		/// </summary>
		/// <param name="intValue">The 32-bit signed integer value.</param>
		/// <returns>A tuple containing four bytes (b1, b2, b3, b4) in big-endian order.</returns>
		/// <remarks>Bytes are extracted as [b1:31-24, b2:23-16, b3:15-8, b4:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte b1, byte b2, byte b3, byte b4) UnpackToBytes(this int intValue) =>
			((byte)(intValue >> 24),
			 (byte)(intValue >> 16),
			 (byte)(intValue >> 8),
			 (byte)intValue);

		/// <summary>
		/// Unpacks a 32-bit signed integer into a tuple of four signed bytes.
		/// </summary>
		/// <param name="intValue">The 32-bit signed integer value.</param>
		/// <returns>A tuple containing four sbytes (sb1, sb2, sb3, sb4) in big-endian order.</returns>
		/// <remarks>Bytes are interpreted as signed values (e.g., 255 becomes -1). Bits are [sb1:31-24, sb2:23-16, sb3:15-8, sb4:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte sb1, sbyte sb2, sbyte sb3, sbyte sb4) UnpackToSBytes(this int intValue) =>
			((sbyte)(intValue >> 24),
			 (sbyte)(intValue >> 16),
			 (sbyte)(intValue >> 8),
			 (sbyte)intValue);

		/// <summary>
		/// Unpacks a 32-bit signed integer into a tuple of two signed shorts.
		/// </summary>
		/// <param name="intValue">The 32-bit signed integer value.</param>
		/// <returns>A tuple containing two shorts (s1, s2) in big-endian order.</returns>
		/// <remarks>Shorts are interpreted as signed values (e.g., 65535 becomes -1). Bits are [s1:31-16, s2:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (short s1, short s2) UnpackToShorts(this int intValue) =>
			((short)(intValue >> 16),
			 (short)intValue);

		/// <summary>
		/// Unpacks a 32-bit signed integer into a tuple of two unsigned shorts.
		/// </summary>
		/// <param name="intValue">The 32-bit signed integer value.</param>
		/// <returns>A tuple containing two ushorts (us1, us2) in big-endian order.</returns>
		/// <remarks>Bits are extracted as [us1:31-16, us2:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (ushort us1, ushort us2) UnpackToUShorts(this int intValue) =>
			((ushort)(intValue >> 16),
			 (ushort)intValue);
	}
}