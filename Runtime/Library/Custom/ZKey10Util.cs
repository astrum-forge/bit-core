namespace BitCore
{
    /// <summary>
    /// Utility class for ZKey10 Morton key operations.
    /// Provides bit manipulation methods for encoding and decoding 3D coordinates.
    /// </summary>
    internal static class ZKey10Util
    {
        // Bit masks for isolating components (10 bits each, interleaved)
        public static readonly uint X_MASK = 0x09249249; // Bits for x: 0, 3, 6, ..., 27
        public static readonly uint Y_MASK = 0x12492492; // Bits for y: 1, 4, 7, ..., 28
        public static readonly uint Z_MASK = 0x24924924; // Bits for z: 2, 5, 8, ..., 29

        // Combined masks for component operations
        public static readonly uint XY_MASK = X_MASK | Y_MASK;
        public static readonly uint XZ_MASK = X_MASK | Z_MASK;
        public static readonly uint YZ_MASK = Y_MASK | Z_MASK;

        /// <summary>
        /// Encodes 3D coordinates into a single Morton key.
        /// </summary>
        public static uint Encode(uint x, uint y, uint z)
        {
            return (EncodePart(z) << 2) | (EncodePart(y) << 1) | EncodePart(x);
        }

        /// <summary>
        /// Decodes a Morton key into its x, y, z components.
        /// </summary>
        public static (uint x, uint y, uint z) Decode(uint zKey)
        {
            return (DecodePart(zKey), DecodePart(zKey >> 1), DecodePart(zKey >> 2));
        }

        /// <summary>
        /// Encodes a single 10-bit component into its interleaved form.
        /// </summary>
        public static uint EncodePart(uint n)
        {
            uint n0 = n & 0x000003FF; // Mask to 10 bits

            uint n1 = (n0 | (n0 << 16)) & 0xFF0000FF;
            uint n2 = (n1 | (n1 << 8)) & 0x0300F00F;
            uint n3 = (n2 | (n2 << 4)) & 0x030C30C3;
            uint n4 = (n3 | (n3 << 2)) & 0x09249249;

            return n4;
        }

        /// <summary>
        /// Decodes a single interleaved component back to its 10-bit value.
        /// </summary>
        public static uint DecodePart(uint n)
        {
            uint n0 = n & 0x09249249;

            uint n1 = (n0 | (n0 >> 2)) & 0x030C30C3;
            uint n2 = (n1 | (n1 >> 4)) & 0x0300F00F;
            uint n3 = (n2 | (n2 >> 8)) & 0xFF0000FF;
            uint n4 = (n3 | (n3 >> 16)) & 0x000003FF;

            return n4;
        }

        // Increment operations
        public static uint IncX(uint zKey) => ((zKey | YZ_MASK) + 1) & X_MASK | (zKey & YZ_MASK);
        public static uint IncY(uint zKey) => ((zKey | XZ_MASK) + 2) & Y_MASK | (zKey & XZ_MASK);
        public static uint IncZ(uint zKey) => ((zKey | XY_MASK) + 1) & Z_MASK | (zKey & XY_MASK);

        // Decrement operations
        public static uint DecX(uint zKey) => ((zKey & X_MASK) - 1) & X_MASK | (zKey & YZ_MASK);
        public static uint DecY(uint zKey) => ((zKey & Y_MASK) - 2) & Y_MASK | (zKey & XZ_MASK);
        public static uint DecZ(uint zKey) => ((zKey & Z_MASK) - 1) & Z_MASK | (zKey & XY_MASK);
    }
}