namespace BitCore
{
    /// <summary>
    /// Defines a common interface for bit array implementations with varying backing types.
    /// </summary>
    public interface IBitArray
    {
        /// <summary>
        /// Gets the total number of bits in the array.
        /// </summary>
        int BitLength { get; }

        /// <summary>
        /// Gets the total number of bytes in the array.
        /// </summary>
        int ByteLength { get; }

        /// <summary>
        /// Gets the value of the bit at the specified position.
        /// </summary>
        /// <param name="pos">The zero-based bit position.</param>
        /// <returns>1 if the bit is set, 0 if cleared.</returns>
        int GetBitAt(int pos);

        /// <summary>
        /// Gets the inverted value of the bit at the specified position.
        /// </summary>
        /// <param name="pos">The zero-based bit position.</param>
        /// <returns>0 if the bit is set, 1 if cleared.</returns>
        int GetBitInvAt(int pos);

        /// <summary>
        /// Sets the bit at the specified position to 1.
        /// </summary>
        /// <param name="pos">The zero-based bit position.</param>
        void SetBitAt(int pos);

        /// <summary>
        /// Sets the bit at the specified position to the given value (0 or 1).
        /// </summary>
        /// <param name="pos">The zero-based bit position.</param>
        /// <param name="bitValue">The value to set (0 or 1).</param>
        void SetBitValueAt(int pos, int bitValue);

        /// <summary>
        /// Clears the bit at the specified position (sets it to 0).
        /// </summary>
        /// <param name="pos">The zero-based bit position.</param>
        void ClearBitAt(int pos);

        /// <summary>
        /// Toggles the bit at the specified position (0 to 1, or 1 to 0).
        /// </summary>
        /// <param name="pos">The zero-based bit position.</param>
        void ToggleBitAt(int pos);

        /// <summary>
        /// Counts the number of bits set to 1 in the array.
        /// </summary>
        /// <returns>The total number of set bits.</returns>
        int PopCount();
    }
}