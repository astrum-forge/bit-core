namespace BitCore
{
    /// <summary>
    /// Utility class for ZKey21 Morton key operations.
    /// Provides bit manipulation methods for encoding and decoding 3D coordinates with 21 bits per component.
    /// </summary>
    internal static class ZKey3D21Util
    {
        // Bit masks for isolating components (21 bits each, interleaved)
        public static readonly ulong X_MASK = 0x1249249249249249; // Bits for x: 1, 4, 7, ..., 61
        public static readonly ulong Y_MASK = 0x2492492492492492; // Bits for y: 2, 5, 8, ..., 62
        public static readonly ulong Z_MASK = 0x4924924924924924; // Bits for z: 3, 6, 9, ..., 63

        // Combined masks for component operations
        public static readonly ulong XY_MASK = X_MASK | Y_MASK; // 0x369B6DB6DB6DB6DB
        public static readonly ulong XZ_MASK = X_MASK | Z_MASK; // 0x5B6DB6DB6DB6DB6D
        public static readonly ulong YZ_MASK = Y_MASK | Z_MASK; // 0x6DB6DB6DB6DB6DB6

        /// <summary>
        /// Encodes 3D coordinates into a single Morton key.
        /// </summary>
        public static ulong Encode(uint x, uint y, uint z)
        {
            return (EncodePart(z) << 2) | (EncodePart(y) << 1) | EncodePart(x);
        }

        /// <summary>
        /// Decodes a Morton key into its x, y, z components.
        /// </summary>
        public static (uint x, uint y, uint z) Decode(ulong zKey)
        {
            return (DecodePart(zKey), DecodePart(zKey >> 1), DecodePart(zKey >> 2));
        }

        /// <summary>
        /// Encodes a single 21-bit component into its interleaved form.
        /// </summary>
        public static ulong EncodePart(uint n)
        {
            ulong n0 = n & 0x001FFFFF; // Mask to 21 bits

            n0 = (n0 | (n0 << 32)) & 0x001F00000000FFFF;
            n0 = (n0 | (n0 << 16)) & 0x001F0000FF0000FF;
            n0 = (n0 | (n0 << 8)) & 0x100F00F00F00F00F;
            n0 = (n0 | (n0 << 4)) & 0x10C30C30C30C30C3;
            n0 = (n0 | (n0 << 2)) & 0x9249249249249249;

            return n0;
        }

        /// <summary>
        /// Decodes a single interleaved component back to its 21-bit value.
        /// </summary>
        public static uint DecodePart(ulong n)
        {
            ulong n0 = n & 0x9249249249249249;

            n0 = (n0 | (n0 >> 2)) & 0x10C30C30C30C30C3;
            n0 = (n0 | (n0 >> 4)) & 0x100F00F00F00F00F;
            n0 = (n0 | (n0 >> 8)) & 0x001F0000FF0000FF;
            n0 = (n0 | (n0 >> 16)) & 0x001F00000000FFFF;
            n0 = (n0 | (n0 >> 32)) & 0x001FFFFF;

            return (uint)n0;
        }

        // Increment operations
        public static ulong IncX(ulong zKey) => ((zKey | YZ_MASK) + 1) & X_MASK | (zKey & YZ_MASK);
        public static ulong IncY(ulong zKey) => ((zKey | XZ_MASK) + 2) & Y_MASK | (zKey & XZ_MASK);
        public static ulong IncZ(ulong zKey) => ((zKey | XY_MASK) + 1) & Z_MASK | (zKey & XY_MASK);

        // Decrement operations
        public static ulong DecX(ulong zKey) => ((zKey & X_MASK) - 1) & X_MASK | (zKey & YZ_MASK);
        public static ulong DecY(ulong zKey) => ((zKey & Y_MASK) - 2) & Y_MASK | (zKey & XZ_MASK);
        public static ulong DecZ(ulong zKey) => ((zKey & Z_MASK) - 1) & Z_MASK | (zKey & XY_MASK);
    }
}