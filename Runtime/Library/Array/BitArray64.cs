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
    /// A ulong-backed array for efficient bit-level manipulation.
    /// Bits are stored in big-endian order within each 64-bit word (MSB at position 0).
    /// </summary>
    public class BitArray64 : IBitArray
    {
        /// <summary>
        /// Number of bits per element in the underlying ulong array (64 bits per word).
        /// </summary>
        public const int BitsPerElement = 64;

        private readonly ulong[] _array;

        /// <summary>
        /// Initializes a new BitArray64 with the specified number of bits.
        /// </summary>
        /// <param name="bitCount">Total number of bits to allocate (default is 64).</param>
        /// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if bitCount is negative.</exception>
        public BitArray64(int bitCount = 64)
        {
#if BITCORE_DEBUG
            if (bitCount < 0)
                throw new ArgumentOutOfRangeException(nameof(bitCount), "Bit count must be non-negative.");
#endif
            bitCount = Math.Max(bitCount, 1); // Ensure at least 1 word
            int ulongCount = (bitCount + BitsPerElement - 1) / BitsPerElement; // Round up
            _array = new ulong[ulongCount];
        }

        /// <summary>
        /// Gets the underlying ulong array storing the bits.
        /// </summary>
        public ulong[] Elements => _array;

        /// <summary>
        /// Gets the number of ulong words in the array.
        /// </summary>
        public int ElementLength => _array.Length;

        /// <summary>
        /// Gets the total number of bits in the array.
        /// </summary>
        public int BitLength => _array.Length * BitsPerElement;

        /// <summary>
        /// Gets the value of the bit at the specified position.
        /// </summary>
        /// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
        /// <returns>1 if the bit is set, 0 if cleared.</returns>
        /// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is out of range.</exception>
        /// <remarks>Bits within each ulong are big-endian (MSB at position 0).</remarks>
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
            return (int)_array[index].BitAt(bitOffset);
        }

        /// <summary>
        /// Gets the inverted value of the bit at the specified position.
        /// </summary>
        /// <param name="pos">The zero-based bit position (0 to BitLength - 1).</param>
        /// <returns>0 if the bit is set, 1 if cleared.</returns>
        /// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if pos is out of range.</exception>
        /// <remarks>Bits within each ulong are big-endian (MSB at position 0).</remarks>
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
            return (int)_array[index].BitInvAt(bitOffset);
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
        /// Counts the number of bits set to 1 in the BitArray64.
        /// </summary>
        /// <returns>The total number of set bits.</returns>
        /// <remarks>Optimized to process each ulong directly, leveraging ULongExtensions.PopCount.</remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int PopCount()
        {
            int count = 0;
            foreach (ulong value in _array)
            {
                count += value.PopCount();
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