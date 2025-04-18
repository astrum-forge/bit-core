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

namespace BitCore
{
	/// <summary>
	/// A byte-backed array for efficient bit-level manipulation.
	/// Bits are stored in big-endian order within each byte (MSB at position 0).
	/// </summary>
	public class BitArray8 : IBitArray
	{
		/// <summary>
		/// Number of bits per element in the underlying byte array (8 bits per byte).
		/// </summary>
		public const int BitsPerElement = 8;

		private readonly byte[] _array;
		private readonly int _bitLength;

		/// <summary>
		/// Initializes a new BitArray8 with the specified number of bits.
		/// </summary>
		/// <param name="bitCount">Total number of bits to allocate (default is 8).</param>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if bitCount is negative.</exception>
		public BitArray8(int bitCount = 8)
		{
#if BITCORE_DEBUG
			if (bitCount < 0)
				throw new ArgumentOutOfRangeException(nameof(bitCount), "Bit count must be non-negative.");
#endif
			bitCount = Math.Max(bitCount, 1); // Ensure at least 1 byte
			int byteCount = (bitCount + BitsPerElement - 1) / BitsPerElement; // Round up
			_array = new byte[byteCount];
			_bitLength = bitCount;
		}

		/// <summary>
		/// Gets the underlying byte array storing the bits.
		/// </summary>
		public byte[] Elements => _array;

		/// <summary>
		/// Gets the number of bytes in the array.
		/// </summary>
		public int ByteLength => BitLength / 8;

		/// <summary>
		/// Gets the total number of bits in the array.
		/// </summary>
		public int BitLength => _bitLength;

		/// <summary>
		/// Fills the array with all 1's (enables all bits)
		/// </summary>
		public void Fill()
		{
			int Length = _array.Length;

			// Count full bytes
			for (int i = 0; i < Length; i++)
			{
				_array[i] = 0xFF;
			}
		}

		/// <summary>
		/// Fills the array with all 0's (clears all bits)
		/// </summary>
		public void Clear()
		{
			int Length = _array.Length;

			// Count full bytes
			for (int i = 0; i < Length; i++)
			{
				_array[i] = 0;
			}
		}

		/// <summary>
		/// Gets the value of the bit at the specified position.
		/// </summary>
		/// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
		/// <returns>1 if the bit is set, 0 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is out of range.</exception>
		/// <remarks>Bits within each byte are big-endian (MSB at position 0).</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public int GetBitAt(int pos)
		{
#if BITCORE_DEBUG
			ValidatePosition(pos);
#endif
			int index = pos / BitsPerElement;
			int bitOffset = pos % BitsPerElement;

			return _array[index].BitAt(bitOffset);
		}

		/// <summary>
		/// Gets the inverted value of the bit at the specified position.
		/// </summary>
		/// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
		/// <returns>0 if the bit is set, 1 if cleared.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is out of range.</exception>
		/// <remarks>Bits within each byte are big-endian (MSB at position 0).</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public int GetBitInvAt(int pos)
		{
#if BITCORE_DEBUG
			ValidatePosition(pos);
#endif
			int index = pos / BitsPerElement;
			int bitOffset = pos % BitsPerElement;

			return _array[index].BitInvAt(bitOffset);
		}

		/// <summary>
		/// Sets the bit at the specified position to 1.
		/// </summary>
		/// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is out of range.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public void SetBitAt(int pos)
		{
#if BITCORE_DEBUG
			ValidatePosition(pos);
#endif
			int index = pos / BitsPerElement;
			int bitOffset = pos % BitsPerElement;

			_array[index] = _array[index].SetBitAt(bitOffset);
		}

		/// <summary>
		/// Sets the bit at the specified position to the given value (0 or 1).
		/// </summary>
		/// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
		/// <param name="bitValue">The value to set (0 or 1).</param>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos or bitValue is invalid.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public void SetBitValueAt(int pos, int bitValue)
		{
#if BITCORE_DEBUG
			ValidatePosition(pos);
			if (bitValue != 0 && bitValue != 1)
				throw new ArgumentOutOfRangeException(nameof(bitValue), "Bit value must be 0 or 1.");
#endif
			int index = pos / BitsPerElement;
			int bitOffset = pos % BitsPerElement;

			_array[index] = _array[index].SetBitValueAt(bitOffset, bitValue);
		}

		/// <summary>
		/// Clears the bit at the specified position (sets it to 0).
		/// </summary>
		/// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is out of range.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public void ClearBitAt(int pos)
		{
#if BITCORE_DEBUG
			ValidatePosition(pos);
#endif
			int index = pos / BitsPerElement;
			int bitOffset = pos % BitsPerElement;

			_array[index] = _array[index].ClearBitAt(bitOffset);
		}

		/// <summary>
		/// Toggles the bit at the specified position (0 to 1, or 1 to 0).
		/// </summary>
		/// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is out of range.</exception>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public void ToggleBitAt(int pos)
		{
#if BITCORE_DEBUG
			ValidatePosition(pos);
#endif
			int index = pos / BitsPerElement;
			int bitOffset = pos % BitsPerElement;

			_array[index] = _array[index].ToggleBitAt(bitOffset);
		}

		/// <summary>
		/// Counts the number of bits set to 1 in the BitArray8.
		/// </summary>
		/// <returns>The total number of set bits.</returns>
		/// <remarks>Optimized to process each byte directly, leveraging ByteExtensions.PopCount.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public int PopCount()
		{
			int count = 0;
			int fullBytes = _array.Length;

			// Count full bytes
			for (int i = 0; i < fullBytes - 1; i++)
			{
				count += _array[i].PopCount();
			}

			int excessBits = BitLength % BitsPerElement;

			// Handle excess bits in the last byte, if any
			if (excessBits > 0)
			{
				byte lastByte = _array[fullBytes - 1];
				// Mask to keep only the excessBits (left-aligned, big-endian)
				byte mask = (byte)(0xFF >> (BitsPerElement - excessBits));
				count += (lastByte & mask).PopCount();
			}
			else
			{
				count += _array[fullBytes - 1].PopCount();
			}

			return count;
		}

#if BITCORE_DEBUG
		private void ValidatePosition(int pos)
		{
			if (pos < 0 || pos >= BitLength)
				throw new ArgumentOutOfRangeException(nameof(pos), $"Position must be between 0 and {BitLength - 1}, was {pos}.");
		}
#endif
	}
}