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
	/// Provides high-performance extension methods for packing tuples into and unpacking 64-bit signed integers.
	/// <para>All operations use big-endian byte order (most significant byte first).</para>
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds for minimal overhead.</para>
	/// </summary>
	public static class LongTupleExtensions
	{
		/// <summary>
		/// Packs a tuple of two signed integers into a 64-bit signed integer.
		/// </summary>
		/// <param name="ints">A tuple containing two ints (i1, i2).</param>
		/// <returns>A 64-bit signed integer with ints packed as i1:i2 (big-endian, treated as unsigned).</returns>
		/// <remarks>Each int is cast to a uint, preserving its bit pattern (e.g., -1 becomes 4294967295). Bits are [i1:63-32, i2:31-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long PackToLong(this (int i1, int i2) ints) =>
			(long)((ulong)(uint)ints.i1 << 32 | (uint)ints.i2);

		/// <summary>
		/// Packs a tuple of two unsigned integers into a 64-bit signed integer.
		/// </summary>
		/// <param name="uints">A tuple containing two uints (u1, u2).</param>
		/// <returns>A 64-bit signed integer with uints packed as u1:u2 (big-endian).</returns>
		/// <remarks>Bits are arranged as [u1:63-32, u2:31-0]. Useful for data packing or serialization.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long PackToLong(this (uint u1, uint u2) uints) =>
			(long)((ulong)uints.u1 << 32 | uints.u2);

		/// <summary>
		/// Packs a tuple of four signed shorts into a 64-bit signed integer.
		/// </summary>
		/// <param name="shorts">A tuple containing four shorts (s1, s2, s3, s4).</param>
		/// <returns>A 64-bit signed integer with shorts packed as s1:s2:s3:s4 (big-endian, treated as unsigned).</returns>
		/// <remarks>Each short is cast to a ushort, preserving its bit pattern (e.g., -1 becomes 65535). Bits are [s1:63-48, s2:47-32, s3:31-16, s4:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long PackToLong(this (short s1, short s2, short s3, short s4) shorts) =>
			(long)((ulong)(ushort)shorts.s1 << 48 |
				   (ulong)(ushort)shorts.s2 << 32 |
				   (ulong)(ushort)shorts.s3 << 16 |
				   (ushort)shorts.s4);

		/// <summary>
		/// Packs a tuple of four unsigned shorts into a 64-bit signed integer.
		/// </summary>
		/// <param name="ushorts">A tuple containing four ushorts (us1, us2, us3, us4).</param>
		/// <returns>A 64-bit signed integer with ushorts packed as us1:us2:us3:us4 (big-endian).</returns>
		/// <remarks>Bits are arranged as [us1:63-48, us2:47-32, us3:31-16, us4:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long PackToLong(this (ushort us1, ushort us2, ushort us3, ushort us4) ushorts) =>
			(long)((ulong)ushorts.us1 << 48 |
				   (ulong)ushorts.us2 << 32 |
				   (ulong)ushorts.us3 << 16 |
				   ushorts.us4);

		/// <summary>
		/// Packs a tuple of eight bytes into a 64-bit signed integer.
		/// </summary>
		/// <param name="bytes">A tuple containing eight bytes (b1, b2, b3, b4, b5, b6, b7, b8).</param>
		/// <returns>A 64-bit signed integer with bytes packed as b1:b2:b3:b4:b5:b6:b7:b8 (big-endian).</returns>
		/// <remarks>Bits are arranged as [b1:63-56, b2:55-48, b3:47-40, b4:39-32, b5:31-24, b6:23-16, b7:15-8, b8:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long PackToLong(this (byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, byte b8) bytes) =>
			(long)((ulong)bytes.b1 << 56 |
				   (ulong)bytes.b2 << 48 |
				   (ulong)bytes.b3 << 40 |
				   (ulong)bytes.b4 << 32 |
				   (ulong)bytes.b5 << 24 |
				   (ulong)bytes.b6 << 16 |
				   (ulong)bytes.b7 << 8 |
				   bytes.b8);

		/// <summary>
		/// Packs a tuple of eight signed bytes into a 64-bit signed integer.
		/// </summary>
		/// <param name="sbytes">A tuple containing eight sbytes (sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8).</param>
		/// <returns>A 64-bit signed integer with sbytes packed as sb1:sb2:sb3:sb4:sb5:sb6:sb7:sb8 (big-endian, treated as unsigned).</returns>
		/// <remarks>Each sbyte is cast to a byte, preserving its bit pattern (e.g., -1 becomes 255). Bits are [sb1:63-56, sb2:55-48, ..., sb8:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static long PackToLong(this (sbyte sb1, sbyte sb2, sbyte sb3, sbyte sb4, sbyte sb5, sbyte sb6, sbyte sb7, sbyte sb8) sbytes) =>
			(long)((ulong)(byte)sbytes.sb1 << 56 |
				   (ulong)(byte)sbytes.sb2 << 48 |
				   (ulong)(byte)sbytes.sb3 << 40 |
				   (ulong)(byte)sbytes.sb4 << 32 |
				   (ulong)(byte)sbytes.sb5 << 24 |
				   (ulong)(byte)sbytes.sb6 << 16 |
				   (ulong)(byte)sbytes.sb7 << 8 |
				   (byte)sbytes.sb8);

		/// <summary>
		/// Unpacks a 64-bit signed integer into a tuple of eight bytes.
		/// </summary>
		/// <param name="longValue">The 64-bit signed integer value.</param>
		/// <returns>A tuple containing eight bytes (b1, b2, b3, b4, b5, b6, b7, b8) in big-endian order.</returns>
		/// <remarks>Bytes are extracted as [b1:63-56, b2:55-48, b3:47-40, b4:39-32, b5:31-24, b6:23-16, b7:15-8, b8:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, byte b8) UnpackToBytes(this long longValue) =>
			(
				(byte)(longValue >> 56),
				(byte)(longValue >> 48),
				(byte)(longValue >> 40),
				(byte)(longValue >> 32),
				(byte)(longValue >> 24),
				(byte)(longValue >> 16),
				(byte)(longValue >> 8),
				(byte)longValue
			);

		/// <summary>
		/// Unpacks a 64-bit signed integer into a tuple of eight signed bytes.
		/// </summary>
		/// <param name="longValue">The 64-bit signed integer value.</param>
		/// <returns>A tuple containing eight sbytes (sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8) in big-endian order.</returns>
		/// <remarks>Bytes are interpreted as signed values (e.g., 255 becomes -1). Bits are [sb1:63-56, sb2:55-48, ..., sb8:7-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (sbyte sb1, sbyte sb2, sbyte sb3, sbyte sb4, sbyte sb5, sbyte sb6, sbyte sb7, sbyte sb8) UnpackToSBytes(this long longValue) =>
			(
				(sbyte)(longValue >> 56),
				(sbyte)(longValue >> 48),
				(sbyte)(longValue >> 40),
				(sbyte)(longValue >> 32),
				(sbyte)(longValue >> 24),
				(sbyte)(longValue >> 16),
				(sbyte)(longValue >> 8),
				(sbyte)longValue
			);

		/// <summary>
		/// Unpacks a 64-bit signed integer into a tuple of four signed shorts.
		/// </summary>
		/// <param name="longValue">The 64-bit signed integer value.</param>
		/// <returns>A tuple containing four shorts (s1, s2, s3, s4) in big-endian order.</returns>
		/// <remarks>Shorts are interpreted as signed values (e.g., 65535 becomes -1). Bits are [s1:63-48, s2:47-32, s3:31-16, s4:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (short s1, short s2, short s3, short s4) UnpackToShorts(this long longValue) =>
			(
				(short)(longValue >> 48),
				(short)(longValue >> 32),
				(short)(longValue >> 16),
				(short)longValue
			);

		/// <summary>
		/// Unpacks a 64-bit signed integer into a tuple of four unsigned shorts.
		/// </summary>
		/// <param name="longValue">The 64-bit signed integer value.</param>
		/// <returns>A tuple containing four ushorts (us1, us2, us3, us4) in big-endian order.</returns>
		/// <remarks>Bits are extracted as [us1:63-48, us2:47-32, us3:31-16, us4:15-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (ushort us1, ushort us2, ushort us3, ushort us4) UnpackToUShorts(this long longValue) =>
			(
				(ushort)(longValue >> 48),
				(ushort)(longValue >> 32),
				(ushort)(longValue >> 16),
				(ushort)longValue
			);

		/// <summary>
		/// Unpacks a 64-bit signed integer into a tuple of two signed integers.
		/// </summary>
		/// <param name="longValue">The 64-bit signed integer value.</param>
		/// <returns>A tuple containing two ints (i1, i2) in big-endian order.</returns>
		/// <remarks>Ints are interpreted as signed values (e.g., 4294967295 becomes -1). Bits are [i1:63-32, i2:31-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (int i1, int i2) UnpackToInts(this long longValue) =>
			(
				(int)(longValue >> 32),
				(int)longValue
			);

		/// <summary>
		/// Unpacks a 64-bit signed integer into a tuple of two unsigned integers.
		/// </summary>
		/// <param name="longValue">The 64-bit signed integer value.</param>
		/// <returns>A tuple containing two uints (u1, u2) in big-endian order.</returns>
		/// <remarks>Bits are extracted as [u1:63-32, u2:31-0].</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static (uint u1, uint u2) UnpackToUInts(this long longValue) =>
			(
				(uint)(longValue >> 32),
				(uint)longValue
			);
	}
}